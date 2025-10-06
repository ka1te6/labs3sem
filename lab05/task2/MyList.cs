using System;
using System.Collections;
using System.Collections.Generic;

public class MyList<T> : IEnumerable<T>
{
    private T[] items;
    private int count;
    private int capacity;

    public int Count => count;
    public int Capacity => capacity;

    public MyList()
    {
        capacity = 4;
        items = new T[capacity];
        count = 0;
    }

    public MyList(int initialCapacity)
    {
        if (initialCapacity <= 0)
            throw new ArgumentException("Емкость должна быть положительной");
        
        capacity = initialCapacity;
        items = new T[capacity];
        count = 0;
    }

    public void Add(T item)
    {
        if (count == capacity)
        {
            capacity *= 2;
            Array.Resize(ref items, capacity);
        }
        items[count] = item;
        count++;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Индекс вне границ списка");
            return items[index];
        }
        set
        {
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException("Индекс вне границ списка");
            items[index] = value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < count; i++)
        {
            yield return items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}