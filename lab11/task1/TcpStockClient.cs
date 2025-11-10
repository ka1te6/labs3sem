using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

public class TcpStockClient
{
    private const string ServerIp = "127.0.0.1";
    private const int ServerPort = 5000;

    public static async Task Main()
    {
        Console.WriteLine("🔹 TCP клиент запущен.");
        Console.Write("Введите тикер акции: ");
        string ticker = Console.ReadLine()?.Trim().ToUpper() ?? "";

        if (string.IsNullOrWhiteSpace(ticker))
        {
            Console.WriteLine("Ошибка: тикер не введён.");
            return;
        }

        try
        {
            using var client = new TcpClient();
            await client.ConnectAsync(ServerIp, ServerPort);

            using var stream = client.GetStream();
            using var writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            using var reader = new StreamReader(stream, Encoding.UTF8);

            await writer.WriteLineAsync(ticker);

            string? response = await reader.ReadLineAsync();
            Console.WriteLine($"Ответ сервера: {response}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка подключения: {ex.Message}");
        }
    }
}