using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public class StockPriceAnalyzer
{
    private readonly string _apiToken;
    private readonly string _tickersFile;
    private readonly string _outputFile;
    private readonly HttpClient _httpClient;
    private readonly SemaphoreSlim _throttler;
    private readonly object _fileLock = new object(); 
    
    private const int MAX_CONCURRENT_REQUESTS = 5;
    private const int DELAY_BETWEEN_REQUESTS_MS = 1000; 
    
    public StockPriceAnalyzer(string apiToken, string tickersFile = "ticker.txt", string outputFile = "results.txt")
    {
        _apiToken = apiToken;
        _tickersFile = tickersFile;
        _outputFile = outputFile;
        _httpClient = new HttpClient();
        _throttler = new SemaphoreSlim(MAX_CONCURRENT_REQUESTS, MAX_CONCURRENT_REQUESTS);
        
        File.WriteAllText(_outputFile, string.Empty);
    }
    
    public async Task AnalyzeStocksAsync()
    {
        var tickers = await ReadTickersAsync();
        
        Console.WriteLine($"Найдено тикеров: {tickers.Count}");
        Console.WriteLine($"Ограничение: {MAX_CONCURRENT_REQUESTS} одновременных запросов");
        
        var tasks = new List<Task>();
        var delay = 0;
        
        foreach (var ticker in tickers)
        {
            if (delay > 0)
                await Task.Delay(DELAY_BETWEEN_REQUESTS_MS / MAX_CONCURRENT_REQUESTS);

            tasks.Add(ProcessTickerWithThrottlingAsync(ticker));
            delay++;
        }
        
        await Task.WhenAll(tasks);
        
        Console.WriteLine("Анализ завершен. Результаты сохранены в файл.");
    }
    
    private async Task ProcessTickerWithThrottlingAsync(string ticker)
    {
        await _throttler.WaitAsync();
        
        try
        {
            await ProcessTickerAsync(ticker);
            await Task.Delay(DELAY_BETWEEN_REQUESTS_MS);
        }
        finally
        {
            _throttler.Release();
        }
    }
    
    private async Task<List<string>> ReadTickersAsync()
    {
        var tickers = new List<string>();
        
        if (!File.Exists(_tickersFile))
            throw new FileNotFoundException($"Файл с тикерами {_tickersFile} не найден");
        
        using var reader = new StreamReader(_tickersFile);
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (!string.IsNullOrWhiteSpace(line))
                tickers.Add(line.Trim());
        }
        
        return tickers;
    }
    
    private async Task ProcessTickerAsync(string ticker)
    {
        try
        {
            var candlesData = await GetStockDataAsync(ticker);
            
            if (candlesData != null && candlesData.Status == "ok")
            {
                var averagePrice = CalculateAveragePrice(candlesData);
                
                var stockData = new StockData
                {
                    Ticker = ticker,
                    AveragePrice = averagePrice
                };
                
                SaveResult(stockData); // 🔒 теперь запись под lock
                
                Console.WriteLine($"Обработан тикер: {ticker}, Средняя цена: {averagePrice:F2}");
            }
            else
            {
                Console.WriteLine($"Ошибка при получении данных для тикера: {ticker}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке тикера {ticker}: {ex.Message}");
        }
    }
    
    private async Task<CandlesResponse?> GetStockDataAsync(string ticker)
    {
        var toDate = DateTime.Now.ToString("yyyy-MM-dd");
        var fromDate = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
        
        var url = $"https://api.marketdata.app/v1/stocks/candles/D/{ticker}/?from={fromDate}&to={toDate}&token={_apiToken}";
        
        try
        {
            var response = await _httpClient.GetAsync(url);
            
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<CandlesResponse>(json);
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Тикер {ticker} не найден");
                return null;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
            {
                Console.WriteLine($"Превышен лимит запросов для {ticker}. Ожидание...");
                await Task.Delay(5000);
                return null;
            }
            else
            {
                Console.WriteLine($"HTTP ошибка для {ticker}: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка API для {ticker}: {ex.Message}");
            return null;
        }
    }
    
    private double CalculateAveragePrice(CandlesResponse candlesData)
    {
        if (candlesData.High == null || candlesData.Low == null || candlesData.High.Count == 0)
            return 0;
        
        double sum = 0;
        int count = 0;
        
        for (int i = 0; i < candlesData.High.Count; i++)
        {
            var dailyAverage = (candlesData.High[i] + candlesData.Low[i]) / 2;
            sum += dailyAverage;
            count++;
        }
        
        return count > 0 ? sum / count : 0;
    }

    private void SaveResult(StockData stockData)
    {
        lock (_fileLock)
        {
            File.AppendAllText(_outputFile, $"{stockData.Ticker}:{stockData.AveragePrice:F2}{Environment.NewLine}", Encoding.UTF8);
        }
    }
}
