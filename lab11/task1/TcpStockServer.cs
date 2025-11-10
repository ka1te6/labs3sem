using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

public class TcpStockServer
{
    private const int Port = 5000;

    public static async Task Main()
    {
        Console.WriteLine("TCP сервер запущен. Ожидание клиентов...");

        var listener = new TcpListener(IPAddress.Any, Port);
        listener.Start();

        while (true)
        {
            var client = await listener.AcceptTcpClientAsync();
            _ = HandleClientAsync(client); 
        }
    }

    private static async Task HandleClientAsync(TcpClient client)
    {
        Console.WriteLine($"Клиент подключен: {client.Client.RemoteEndPoint}");

        using var stream = client.GetStream();
        using var reader = new StreamReader(stream, Encoding.UTF8);
        using var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

        string? ticker = await reader.ReadLineAsync();

        if (string.IsNullOrWhiteSpace(ticker))
        {
            await writer.WriteLineAsync("Ошибка: пустой тикер");
            client.Close();
            return;
        }

        ticker = ticker.Trim().ToUpper();

        Console.WriteLine($"Получен запрос на тикер: {ticker}");

        string response = await GetLastPriceAsync(ticker);
        await writer.WriteLineAsync(response);

        Console.WriteLine($"Отправлен ответ: {response}");
        client.Close();
    }

    private static async Task<string> GetLastPriceAsync(string ticker)
    {
        try
        {
            using var db = new AppDbContext();
            var lastStock = db.Stocks
                .Where(s => s.Ticker == ticker)
                .OrderByDescending(s => s.Date)
                .FirstOrDefault();

            if (lastStock == null)
                return "нет данных по тикеру " + ticker;

            return $"Последняя цена {ticker}: {lastStock.ClosePrice:F2}";
        }
        catch (Exception ex)
        {
            return $"Ошибка при доступе к БД: {ex.Message}";
        }
    }
}
