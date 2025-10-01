using System;

namespace task3
{
    class Program3
    {
        static void Main()
        {
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 3);
            Point p3 = new Point(4, 0);

            Figure triangle = new Figure("Треугольник", p1, p2, p3);

            Console.WriteLine($"Фигура: {triangle.Name}, Периметр = {triangle.PerimeterCalculator()}");
        }
    }
}
