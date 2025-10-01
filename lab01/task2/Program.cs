using System;

namespace task2
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите длину стороны A: ");
            double a = double.Parse(Console.ReadLine());
            Console.Write("Введите длину стороны B: ");
            double b = double.Parse(Console.ReadLine());

            Rectangle rect = new Rectangle(a, b);

            Console.WriteLine($"Площадь: {rect.Area}");
            Console.WriteLine($"Периметр: {rect.Perimeter}");
        }
    }
}

