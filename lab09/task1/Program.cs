using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Запуск анализа цен акций...");
        
        string apiToken = "eDgyQXRXYmlQZld0NTZ4VG9WWTN4RjYtbjRsdHd2Q1Y5QmRJazlOQ290ND0";
        
        await CreateTickersFileIfNotExists();
        
        var analyzer = new StockPriceAnalyzer(apiToken);
        
        try
        {
            await analyzer.AnalyzeStocksAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Произошла ошибка: {ex.Message}");
        }
        
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
    
    static async Task CreateTickersFileIfNotExists()
    {
        if (!System.IO.File.Exists("ticker.txt"))
        {
            string[] defaultTickers = {
                "AAPL",    
                "MSFT",    
                "GOOGL",   
                "AMZN",    
                "TSLA",    
                "META",    
                "NVDA",   
                "JPM",     
                "JNJ",     
                "V"       
            };
            
            await System.IO.File.WriteAllLinesAsync("ticker.txt", defaultTickers);
            Console.WriteLine("Создан файл ticker.txt с примером тикеров");
        }
    }
}