using System;

namespace task2
{
    public class Car : Vehicle
    {
        public Car(double x, double y, decimal price, double speed, int year)
            : base(x, y, price, speed, year)
        {
        }

        public override void ShowInfo()
        {
            Console.WriteLine("Автомобиль:");
            base.ShowInfo();
            Console.WriteLine();
        }
    }
}