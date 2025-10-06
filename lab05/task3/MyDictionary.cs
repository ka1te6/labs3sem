using System;
using System.Collections;
using System.Collections.Generic;

public class MyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
{
    private struct Entry
    {
        public TKey Key;
        public TValue Value;
        public bool IsActive;
    }

    private Entry[] entries;
    private int count;
    private int capacity;

    public int Count => count;

    public MyDictionary()
    {
        capacity = 4;
        entries = new Entry[capacity];
        count = 0;
    }

    public void Add(TKey key, TValue value)
    {
        if (key == null)
            throw new ArgumentNullException(nameof(key));

        for (int i = 0; i < capacity; i++)
        {
            if (entries[i].IsActive && entries[i].Key.Equals(key))
                throw new ArgumentException("Элемент с таким ключом уже существует");
        }

        if (count >= capacity * 0.75)
        {
            capacity *= 2;
            Array.Resize(ref entries, capacity);
        }

        for (int i = 0; i < capacity; i++)
        {
            if (!entries[i].IsActive)
            {
                entries[i] = new Entry { Key = key, Value = value, IsActive = true };
                count++;
                return;
            }
        }
    }

    public TValue this[TKey key]
    {
        get
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            for (int i = 0; i < capacity; i++)
            {
                if (entries[i].IsActive && entries[i].Key.Equals(key))
                    return entries[i].Value;
            }

            throw new KeyNotFoundException("Ключ не найден в словаре");
        }
        set
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            for (int i = 0; i < capacity; i++)
            {
                if (entries[i].IsActive && entries[i].Key.Equals(key))
                {
                    entries[i].Value = value;
                    return;
                }
            }

            Add(key, value);
        }
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        for (int i = 0; i < capacity; i++)
        {
            if (entries[i].IsActive)
            {
                yield return new KeyValuePair<TKey, TValue>(entries[i].Key, entries[i].Value);
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}