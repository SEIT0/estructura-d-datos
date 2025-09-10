using System;
using UnityEngine;

public class MyQueue<T>
{
    private SimpleList<T> elements = new SimpleList<T>();

    public int Count => elements.Count;

    public void Enqueue(T item)
    {
        elements.Add(item);
    }

    public T Dequeue()
    {
        if (elements.Count == 0)
            throw new InvalidOperationException("La cola está vacía");

        T item = elements[0];
        elements.RemoveAt(0);
        return item;
    }

    public T Peek()
    {
        if (elements.Count == 0)
            throw new InvalidOperationException("La cola está vacía");

        return elements[0];
    }

    public void Clear()
    {
        elements.Clear();
    }

    public T[] ToArray()
    {
        T[] result = new T[elements.Count];
        for (int i = 0; i < elements.Count; i++)
        {
            result[i] = elements[i];
        }
        return result;
    }

    public override string ToString()
    {
        return elements.ToString();
    }

    public bool TryDequeue(out T item)
    {
        if (elements.Count == 0)
        {
            item = default(T);
            return false;
        }

        item = elements[0];
        elements.RemoveAt(0);
        return true;
    }

    public bool TryPeek(out T item)
    {
        if (elements.Count == 0)
        {
            item = default(T);
            return false;
        }

        item = elements[0];
        return true;
    }
}
