using System;

class Program
{
    static void Main()
    {
        Car[] cars =
        {
            new Car("BMW", 2015, 250),
            new Car("Audi", 2018, 240),
            new Car("Lada", 2010, 160),
            new Car("Tesla", 2020, 300)
        };

        CarCatalog catalog = new CarCatalog(cars);

        Console.WriteLine("Прямой проход:");
        foreach (var car in catalog.Forward())
            Console.WriteLine(car);

        Console.WriteLine("\nОбратный проход:");
        foreach (var car in catalog.Backward())
            Console.WriteLine(car);

        Console.WriteLine("\nФильтр по году >= 2015:");
        foreach (var car in catalog.FilterByYear(2015))
            Console.WriteLine(car);

        Console.WriteLine("\nФильтр по скорости >= 200:");
        foreach (var car in catalog.FilterBySpeed(200))
            Console.WriteLine(car);
    }
}