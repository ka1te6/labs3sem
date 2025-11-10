using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите режим: (1 — сервер, 2 — клиент)");
        var key = Console.ReadLine();

        if (key == "1")
        {
            Console.WriteLine("Запуск TCP сервера...");
            TcpStockServer.Main().Wait();
        }
        else if (key == "2")
        {
            Console.WriteLine("Запуск TCP клиента...");
            TcpStockClient.Main().Wait();
        }
        else
        {
            Console.WriteLine("Неверный выбор!");
        }
    }
}