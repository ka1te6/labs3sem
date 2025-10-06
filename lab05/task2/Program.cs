using System;

class Program2
{
    static void Main()
    {
        MyList<int> myList = new MyList<int>();
        
        myList.Add(10);
        myList.Add(20);
        myList.Add(30);
        
        Console.WriteLine($"Количество элементов: {myList.Count}");
        
        Console.WriteLine($"myList[0] = {myList[0]}");
        Console.WriteLine($"myList[1] = {myList[1]}");
        
        Console.WriteLine("Элементы списка:");
        foreach (var item in myList)
        {
            Console.WriteLine(item);
        }
    }
}