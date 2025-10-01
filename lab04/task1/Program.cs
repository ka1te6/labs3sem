using System;

class Program
{
    static void Main()
    {
        MyMatrix A = new MyMatrix(3, 3, 1, 5);
        MyMatrix B = new MyMatrix(3, 3, 1, 5);

        Console.WriteLine("Матрица A:");
        A.Print();
        Console.WriteLine("Матрица B:");
        B.Print();

        Console.WriteLine("A + B:");
        (A + B).Print();

        Console.WriteLine("A - B:");
        (A - B).Print();

        Console.WriteLine("A * 2:");
        (A * 2).Print();

        Console.WriteLine("A / 2:");
        (A / 2).Print();
    }
}