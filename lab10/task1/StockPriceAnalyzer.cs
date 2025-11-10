using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public class StockPriceAnalyzer
{
    private readonly string _apiToken;
    private readonly string _tickersFile;
    private readonly HttpClient _httpClient;
    private readonly SemaphoreSlim _throttler;

    private const int MAX_CONCURRENT_REQUESTS = 5;
    private const int DELAY_BETWEEN_REQUESTS_MS = 1000; 

    public StockPriceAnalyzer(string apiToken, string tickersFile = "ticker.txt")
    {
        _apiToken = apiToken;
        _tickersFile = tickersFile;
        _httpClient = new HttpClient();
        _throttler = new SemaphoreSlim(MAX_CONCURRENT_REQUESTS, MAX_CONCURRENT_REQUESTS);
    }

    public async Task AnalyzeStocksAsync()
    {
        var tickers = await ReadTickersAsync();

        Console.WriteLine($"Найдено тикеров: {tickers.Count}");
        Console.WriteLine($"Ограничение: {MAX_CONCURRENT_REQUESTS} одновременных запросов");

        var tasks = new List<Task>();
        int delay = 0;

        foreach (var ticker in tickers)
        {
            if (delay > 0)
                await Task.Delay(DELAY_BETWEEN_REQUESTS_MS / MAX_CONCURRENT_REQUESTS);

            tasks.Add(ProcessTickerWithThrottlingAsync(ticker));
            delay++;
        }

        await Task.WhenAll(tasks);
        Console.WriteLine("Анализ завершен. Данные сохранены в базу данных.");
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

            if (candlesData == null)
            {
                Console.WriteLine($"Пропуск {ticker} — данных нет.");
                return;
            }

            double averagePrice = CalculateAveragePrice(candlesData);

            if (averagePrice == 0)
            {
                Console.WriteLine($"{ticker}: средняя цена = 0, данные не сохраняются.");
                return;
            }

            using var db = new AppDbContext();

            var stock = new Stock
            {
                Ticker = ticker,
                Date = DateTime.Now,
                ClosePrice = averagePrice
            };

            db.Stocks.Add(stock);
            await db.SaveChangesAsync();
            
            var price = new Price
            {
                TickerId = stock.Id,        
                PriceValue = averagePrice,
                Date = DateTime.Now
            };

            db.Prices.Add(price);
            await db.SaveChangesAsync();

            var lastTwo = db.Stocks
                .Where(s => s.Ticker == ticker)
                .OrderByDescending(s => s.Date)
                .Take(2)
                .ToList();

            string status = "нет данных";
            if (lastTwo.Count == 2)
                status = lastTwo[0].ClosePrice > lastTwo[1].ClosePrice ? "выросла" : "упала";

            db.Conditions.Add(new TodaysCondition
            {
                Ticker = ticker,
                ChangeStatus = status,
                DateChecked = DateTime.Now
            });
            await db.SaveChangesAsync();

            Console.WriteLine($"[{ticker}] Средняя цена: {averagePrice:F2} ({status})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке {ticker}: {ex.Message}");
        }
    }

    
    private async Task<CandlesResponse?> GetStockDataAsync(string ticker)
    {
        var fromDate = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
        var toDate = DateTime.Now.ToString("yyyy-MM-dd");

        string url = $"https://api.marketdata.app/v1/stocks/candles/D/{ticker}/?from={fromDate}&to={toDate}&token={_apiToken}";

        try
        {
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"{ticker}: ошибка HTTP {response.StatusCode}");
                return null;
            }

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<CandlesResponse>(json);

            if (data == null || data.Status != "ok" ||
                data.Close == null || data.Close.Count == 0 ||
                data.High == null || data.Low == null)
            {
                Console.WriteLine($"{ticker}: нет данных от API (Status={data?.Status})");
                return null;
            }

            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ticker}: ошибка при запросе данных — {ex.Message}");
            return null;
        }
    }


    private double CalculateAveragePrice(CandlesResponse candlesData)
    {
        if (candlesData.High == null || candlesData.Low == null || candlesData.High.Count == 0)
            return 0;

        double sum = 0;
        for (int i = 0; i < candlesData.High.Count; i++)
            sum += (candlesData.High[i] + candlesData.Low[i]) / 2;

        return sum / candlesData.High.Count;
    }
}
