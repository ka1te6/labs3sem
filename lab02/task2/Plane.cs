using System;

namespace task2
{
    public class Plane : Vehicle
    {
        public double Height { get; set; }
        public int Passengers { get; set; }

        public Plane(double x, double y, decimal price, double speed, int year, double height, int passengers)
            : base(x, y, price, speed, year)
        {
            Height = height;
            Passengers = passengers;
        }

        public override void ShowInfo()
        {
            Console.WriteLine("Самолет:");
            base.ShowInfo();
            Console.WriteLine($"Высота полета: {Height} м");
            Console.WriteLine($"Количество пассажиров: {Passengers}");
            Console.WriteLine();
        }
    }
}