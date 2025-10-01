using System;

public class MyMatrix
{
    private int[,] data;
    private int rows, cols;

    public MyMatrix(int m, int n, int minValue, int maxValue)
    {
        rows = m;
        cols = n;
        data = new int[m, n];
        Random rnd = new Random();
        for (int i = 0; i < m; i++)
            for (int j = 0; j < n; j++)
                data[i, j] = rnd.Next(minValue, maxValue + 1);
    }

    public int this[int i, int j]
    {
        get => data[i, j];
        set => data[i, j] = value;
    }

    public static MyMatrix operator +(MyMatrix a, MyMatrix b)
    {
        if (a.rows != b.rows || a.cols != b.cols)
            throw new Exception("Размеры матриц не совпадают");
        MyMatrix result = new MyMatrix(a.rows, a.cols, 0, 0);
        for (int i = 0; i < a.rows; i++)
            for (int j = 0; j < a.cols; j++)
                result[i, j] = a[i, j] + b[i, j];
        return result;
    }

    public static MyMatrix operator -(MyMatrix a, MyMatrix b)
    {
        if (a.rows != b.rows || a.cols != b.cols)
            throw new Exception("Размеры матриц не совпадают");
        MyMatrix result = new MyMatrix(a.rows, a.cols, 0, 0);
        for (int i = 0; i < a.rows; i++)
            for (int j = 0; j < a.cols; j++)
                result[i, j] = a[i, j] - b[i, j];
        return result;
    }

    public static MyMatrix operator *(MyMatrix a, MyMatrix b)
    {
        if (a.cols != b.rows)
            throw new Exception("Умножение невозможно");
        MyMatrix result = new MyMatrix(a.rows, b.cols, 0, 0);
        for (int i = 0; i < a.rows; i++)
            for (int j = 0; j < b.cols; j++)
            {
                int sum = 0;
                for (int k = 0; k < a.cols; k++)
                    sum += a[i, k] * b[k, j];
                result[i, j] = sum;
            }
        return result;
    }

    public static MyMatrix operator *(MyMatrix a, int number)
    {
        MyMatrix result = new MyMatrix(a.rows, a.cols, 0, 0);
        for (int i = 0; i < a.rows; i++)
            for (int j = 0; j < a.cols; j++)
                result[i, j] = a[i, j] * number;
        return result;
    }

    public static MyMatrix operator /(MyMatrix a, int number)
    {
        if (number == 0) throw new DivideByZeroException();
        MyMatrix result = new MyMatrix(a.rows, a.cols, 0, 0);
        for (int i = 0; i < a.rows; i++)
            for (int j = 0; j < a.cols; j++)
                result[i, j] = a[i, j] / number;
        return result;
    }

    public void Print()
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
                Console.Write($"{data[i, j],4}");
            Console.WriteLine();
        }
    }
}
