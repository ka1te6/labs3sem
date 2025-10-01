using System;

namespace task2
{
    public class Vehicle
    {
        public double X { get; set; }
        public double Y { get; set; }
        public decimal Price { get; set; }
        public double Speed { get; set; }
        public int Year { get; set; }

        public Vehicle(double x, double y, decimal price, double speed, int year)
        {
            X = x;
            Y = y;
            Price = price;
            Speed = speed;
            Year = year;
        }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Координаты: ({X}, {Y})");
            Console.WriteLine($"Цена: {Price:C}");
            Console.WriteLine($"Скорость: {Speed} км/ч");
            Console.WriteLine($"Год выпуска: {Year}");
        }
    }
}