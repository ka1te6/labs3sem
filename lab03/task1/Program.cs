using System;

namespace task1
{
    class Program
    {
        static void Main()
        {
            var v1 = new Vector(1, 2, 3);
            var v2 = new Vector(4, 5, 6);
            Console.WriteLine(v1 + v2);
            Console.WriteLine(v1 * v2);
            Console.WriteLine(v1 * 2);
            Console.WriteLine(v1 > v2);
        }
    }
}

