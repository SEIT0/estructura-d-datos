using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MyStack<T>
{
    private T[] _items;
    private int _count;
    private const int DefaultCapacity = 4;

    public int Count => _count;

    public void Push(T item)
    {
        if (_items == null) _items = new T[DefaultCapacity];
        if (_count == _items.Length) Array.Resize(ref _items, _items.Length * 2);
        _items[_count++] = item;
    }

    public void Clear()
    {
        if (_items != null) Array.Clear(_items, 0, _count);
        _count = 0;
    }

    public bool TryPop(out T item)
    {
        if (_count == 0)
        {
            item = default;
            return false;
        }
        var idx = --_count;
        item = _items[idx];
        _items[idx] = default;
        return true;
    }

    public bool TryPeek(out T item)
    {
        if (_count == 0)
        {
            item = default;
            return false;
        }
        item = _items[_count - 1];
        return true;
    }
}