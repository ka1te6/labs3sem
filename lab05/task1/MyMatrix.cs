using System;

public class MyMatrix
{
    private double[,] matrix;
    private static Random random = new Random();
    private static int minValue;
    private static int maxValue;

    public int Rows { get; private set; }
    public int Columns { get; private set; }

    public MyMatrix(int rows, int columns)
    {
        if (rows <= 0 || columns <= 0)
            throw new ArgumentException("Количество строк и столбцов должно быть положительным");
        
        Rows = rows;
        Columns = columns;
        matrix = new double[rows, columns];
        
        Console.Write("Введите минимальное значение: ");
        minValue = int.Parse(Console.ReadLine());
        Console.Write("Введите максимальное значение: ");
        maxValue = int.Parse(Console.ReadLine());
        
        Fill();
    }

    public void Fill()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                matrix[i, j] = random.Next(minValue, maxValue + 1);
            }
        }
    }

    public void ChangeSize(int newRows, int newColumns)
    {
        if (newRows <= 0 || newColumns <= 0)
            throw new ArgumentException("Новый размер должен быть положительным");

        double[,] newMatrix = new double[newRows, newColumns];

        int rowsToCopy = Math.Min(Rows, newRows);
        int colsToCopy = Math.Min(Columns, newColumns);

        for (int i = 0; i < rowsToCopy; i++)
        {
            for (int j = 0; j < colsToCopy; j++)
            {
                newMatrix[i, j] = matrix[i, j];
            }
        }

        for (int i = 0; i < newRows; i++)
        {
            for (int j = 0; j < newColumns; j++)
            {
                if (i >= Rows || j >= Columns)
                {
                    newMatrix[i, j] = random.Next(minValue, maxValue + 1);
                }
            }
        }

        matrix = newMatrix;
        Rows = newRows;
        Columns = newColumns;
    }

    public void ShowPartialy(int startRow, int endRow, int startCol, int endCol)
    {
        if (startRow < 0 || endRow >= Rows || startCol < 0 || endCol >= Columns || 
            startRow > endRow || startCol > endCol)
            throw new ArgumentException("Некорректные границы отображения");

        Console.WriteLine($"Часть матрицы с [{startRow},{startCol}] по [{endRow},{endCol}]:");
        
        for (int i = startRow; i <= endRow; i++)
        {
            for (int j = startCol; j <= endCol; j++)
            {
                Console.Write($"{matrix[i, j],8:F2}");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public void Show()
    {
        Console.WriteLine("Полная матрица:");
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Console.Write($"{matrix[i, j],8:F2}");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public double this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
                throw new IndexOutOfRangeException("Индекс вне границ матрицы");
            return matrix[row, col];
        }
        set
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
                throw new IndexOutOfRangeException("Индекс вне границ матрицы");
            matrix[row, col] = value;
        }
    }
}