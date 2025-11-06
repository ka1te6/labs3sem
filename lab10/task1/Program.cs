using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Запуск анализа акций...");

        string apiToken = "LWNwclM1ZEh4N2hobllJNE9EN2hVN0E0UkJnLXpaWG5lQnY1WXV5V3dRcz0";

        await CreateTickersFileIfNotExists();

        var analyzer = new StockPriceAnalyzer(apiToken);
        await analyzer.AnalyzeStocksAsync();

        Console.WriteLine("\nВведите тикер для проверки (например, AAPL):");
        string ticker = Console.ReadLine()?.ToUpper() ?? "";

        using (var db = new AppDbContext())
        {
            var last = db.Conditions
                .Where(c => c.Ticker == ticker)
                .OrderByDescending(c => c.DateChecked)
                .FirstOrDefault();

            if (last != null)
                Console.WriteLine($"Акция {ticker} {last.ChangeStatus}");
            else
                Console.WriteLine("Нет данных по этому тикеру.");
        }

        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }

    static async Task CreateTickersFileIfNotExists()
    {
        if (!System.IO.File.Exists("ticker.txt"))
        {
            string[] defaultTickers = { "AAPL", "MSFT", "GOOGL", "AMZN", "TSLA" };
            await System.IO.File.WriteAllLinesAsync("ticker.txt", defaultTickers);
            Console.WriteLine("Создан файл ticker.txt с примером тикеров.");
        }
    }
}