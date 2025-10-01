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

        Array.Sort(cars, new CarComparer(CarComparer.CompareType.Name));
        Console.WriteLine("Сортировка по имени:");
        foreach (var car in cars) Console.WriteLine(car);

        Array.Sort(cars, new CarComparer(CarComparer.CompareType.Year));
        Console.WriteLine("\nСортировка по году выпуска:");
        foreach (var car in cars) Console.WriteLine(car);

        Array.Sort(cars, new CarComparer(CarComparer.CompareType.Speed));
        Console.WriteLine("\nСортировка по скорости:");
        foreach (var car in cars) Console.WriteLine(car);
    }
}