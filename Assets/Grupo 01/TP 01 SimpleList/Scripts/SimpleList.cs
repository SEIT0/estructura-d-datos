using System;
using UnityEngine;

public class SimpleList<T> : ISimpleList<T>
{
    private T[] array;
    private int count;
    private const int defaultSize = 12;

    public SimpleList()
    {
        array = new T[defaultSize];
        count = 0;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index fuera de rango.");
            return array[index];
        }
        set
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException(nameof(index), "Index fuera de rango.");
            array[index] = value;
        }
    }

    public int Count => count;

    public void Add(T item)
    {
        if (count == array.Length)
            ResizeArray(array.Length * 2);

        array[count] = item;
        count++;
    }

    public void AddRange(T[] collection)
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        while (count + collection.Length > array.Length)
            ResizeArray(array.Length * 2);

        for (int i = 0; i < collection.Length; i++)
            Add(collection[i]);
    }

    public bool Remove(T item)
    {
        int index = -1;
        for (int i = count - 1; i >= 0; i--)
        {
            if (Equals(array[i], item))
            {
                index = i;
                break;
            }
        }
        if (index == -1) return false;

        for (int i = index; i < count - 1; i++)
            array[i] = array[i + 1];

        count--;
        array[count] = default(T);
        return true;
    }

    public bool RemoveLast()
    {
        if (count == 0) return false;
        count--;
        array[count] = default(T);
        return true;
    }

    public void Clear()
    {
        for (int i = 0; i < count; i++)
            array[i] = default(T);
        count = 0;
    }

    private void ResizeArray(int newSize)
    {
        T[] newArray = new T[newSize];
        Array.Copy(array, newArray, count);
        array = newArray;
    }

    public override string ToString()
    {
        if (count == 0) return "No hay elementos";
        string result = "";
        for (int i = 0; i < count - 1; i++)
            result += array[i]?.ToString() + ", ";
        result += array[count - 1]?.ToString();
        return result;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= count)
            throw new ArgumentOutOfRangeException(nameof(index), "Índice fuera de rango.");

        for (int i = index; i < count - 1; i++)
            array[i] = array[i + 1];

        count--;
        array[count] = default(T);
    }
    public void BubbleSort(Comparison<T> comparison)
    {
        for (int i = 0; i < count - 1; i++)
        {
            for (int j = 0; j < count - i - 1; j++)
            {
                if (comparison(array[j], array[j + 1]) > 0)
                {
                    T temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;
                }
            }
        }
    }

    public void SelectionSort(Comparison<T> comparison)
    {
        for (int i = 0; i < count - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < count; j++)
            {
                if (comparison(array[j], array[minIndex]) < 0)
                    minIndex = j;
            }
            if (minIndex != i)
            {
                T temp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = temp;
            }
        }
    }

    public void InsertionSort(Comparison<T> comparison)
    {
        for (int i = 1; i < count; i++)
        {
            T key = array[i];
            int j = i - 1;
            while (j >= 0 && comparison(array[j], key) > 0)
            {
                array[j + 1] = array[j];
                j--;
            }
            array[j + 1] = key;
        }
    }
}