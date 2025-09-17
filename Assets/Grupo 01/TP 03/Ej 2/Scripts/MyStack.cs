using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MyStack<T>
{
    private T[] items;
    private int count;
    private const int DefaultCapacity = 4;

    public int Count => count;

    public void Push(T item)
    {
        if (items == null)
        {
            items = new T[DefaultCapacity];
        }

        if (count == items.Length)
        {
            Array.Resize(ref items, items.Length * 2);
        }

        items[count++] = item;
    }

    public void Clear()
    {
        if (items != null)
        {
            Array.Clear(items, 0, count);
        }

        count = 0;
    }

    public bool TryPop(out T item)
    {
        if (count == 0)
        {
            item = default;
            return false;
        }

        else
        {
            var idx = --count;
            item = items[idx];
            items[idx] = default;
            return true;
        }
    }

    public bool TryPeek(out T item)
    {
        if (count == 0)
        {
            item = default;
            return false;
        }

        else
        {
            item = items[count - 1];
            return true;
        }
    }
}