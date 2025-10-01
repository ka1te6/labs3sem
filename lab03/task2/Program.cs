using System;

namespace task2
{
    class Program
    {
        static void Main()
        {
            var catalog = new CarsCatalog();
            catalog.Add(new Car("BMW", "V6", 250));
            catalog.Add(new Car("Audi", "V8", 280));
            Console.WriteLine(catalog[0]);
            Console.WriteLine(catalog[1]);
        }
    }
}