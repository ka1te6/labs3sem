using System;

class task1
{
    static void Main()
    {
        MyMatrix matrix = new MyMatrix(3, 4);
        
        Console.WriteLine("Исходная матрица:");
        matrix.Show();
        
        Console.WriteLine("Частичное отображение:");
        matrix.ShowPartialy(0, 1, 1, 3);
        
        Console.WriteLine("Изменение размера матрицы:");
        matrix.ChangeSize(4, 5);
        matrix.Show();
        
        Console.WriteLine("Использование индексатора:");
        matrix[0, 0] = 999;
        Console.WriteLine($"matrix[0,0] = {matrix[0,0]}");
    }
}