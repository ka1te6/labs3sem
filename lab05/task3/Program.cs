using System;

class Program3
{
    static void Main()
    {
        MyDictionary<string, int> myDict = new MyDictionary<string, int>();
        
        myDict.Add("one", 1);
        myDict.Add("two", 2);
        myDict.Add("three", 3);
        
        Console.WriteLine($"Количество элементов: {myDict.Count}");
        
        Console.WriteLine($"myDict[\"one\"] = {myDict["one"]}");
        Console.WriteLine($"myDict[\"two\"] = {myDict["two"]}");
        
        myDict["two"] = 22;
        Console.WriteLine($"myDict[\"two\"] после изменения = {myDict["two"]}");
        
        Console.WriteLine("Элементы словаря:");
        foreach (var pair in myDict)
        {
            Console.WriteLine($"{pair.Key}: {pair.Value}");
        }
    }
}