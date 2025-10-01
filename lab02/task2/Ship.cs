using System;

namespace task2
{
    public class Ship : Vehicle
    {
        public int Passengers { get; set; }
        public string Port { get; set; }

        public Ship(double x, double y, decimal price, double speed, int year, int passengers, string port)
            : base(x, y, price, speed, year)
        {
            Passengers = passengers;
            Port = port;
        }

        public override void ShowInfo()
        {
            Console.WriteLine("Корабль:");
            base.ShowInfo();
            Console.WriteLine($"Количество пассажиров: {Passengers}");
            Console.WriteLine($"Порт приписки: {Port}");
            Console.WriteLine();
        }
    }
}