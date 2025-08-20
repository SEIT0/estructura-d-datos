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

    //Indexador: acceso a elemento en index
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

    //Cantidad de elementos en la lista
    public int Count => count;

    //Añadir un elemento al final
    public void Add(T item)
    {
        if (count == array.Length)
            ResizeArray(array.Length * 2);

        array[count] = item;
        count++;
    }

    //Añadir varios elementos al final
    public void AddRange(T[] collection)
    {
        if (collection == null)
            throw new ArgumentNullException(nameof(collection));

        //Si no entra, se cambia el tamaño del array para que entre todo
        while (count + collection.Length > array.Length)
        {
            ResizeArray(array.Length * 2);
        }

        for (int i = 0; i < collection.Length; i++)
        {
            array[count] = collection[i];
            count++;
        }
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

        if (index == -1)
            return false;

        for (int i = index; i < count - 1; i++)
        {
            array[i] = array[i + 1];
        }

        count--;
        array[count] = default(T);

        return true;
    }

    public bool RemoveLast()
    {
        if (count == 0)
            return false;

        count--;
        array[count] = default(T);

        return true;
    }


    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            array[i] = default(T);
        }
        count = 0;
    }

    //Se cambia el tamaño del array
    private void ResizeArray(int newSize)
    {
        T[] newArray = new T[newSize];
        Array.Copy(array, newArray, count);
        array = newArray;
    }

    //Mostrar todos los elementos separados por coma
    public override string ToString()
    {
        if (count == 0)
            return "No hay empanadas";

        string result = "";

        for (int i = 0; i < count - 1; i++)
        {
            result += array[i]?.ToString() + ", ";
        }

        result += array[count - 1]?.ToString();

        return result;
    }

}
