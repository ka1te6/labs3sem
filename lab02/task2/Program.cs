using System;

namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Vehicle plane = new Plane(100, 200, 50000000m, 900, 2020, 10000, 300);
            Vehicle car = new Car(50, 60, 2500000m, 180, 2022);
            Vehicle ship = new Ship(500, 300, 200000000m, 50, 2018, 2000, "Москва");
            
            plane.ShowInfo();
            car.ShowInfo();
            ship.ShowInfo();
          
        }
    }
}