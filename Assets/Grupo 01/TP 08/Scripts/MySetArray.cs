using System;

public class MySetArray<T> : MySet<T>
{
    private T[] items;
    private int count;
    private const int defaultSize = 10;

    public MySetArray()
    {
        items = new T[defaultSize];
        count = 0;
    }

    public override int Count => count;
    public override bool IsEmpty => count == 0;

    public override void Add(T item)
    {
        if (Contains(item)) return;
        if (count == items.Length)
            Array.Resize(ref items, items.Length * 2);

        items[count++] = item;
    }

    public override bool Remove(T item)
    {
        for (int i = 0; i < count; i++)
        {
            if (Equals(items[i], item))
            {
                for (int j = i; j < count - 1; j++)
                    items[j] = items[j + 1];
                count--;
                return true;
            }
        }
        return false;
    }

    public override void Clear()
    {
        count = 0;
        items = new T[defaultSize];
    }

    public override bool Contains(T item)
    {
        for (int i = 0; i < count; i++)
            if (Equals(items[i], item)) return true;
        return false;
    }

    public override void Show()
    {
        Console.WriteLine(ToString());
    }

    public override string ToString()
    {
        if (count == 0) return "Set vacío";
        string result = "";
        for (int i = 0; i < count; i++)
            result += items[i] + (i < count - 1 ? ", " : "");
        return result;
    }
    public override SimpleList<T> GetElements()
    {
        var list = new SimpleList<T>();
        for (int i = 0; i < count; i++)
            list.Add(items[i]);
        return list;
    }
    public override MySet<T> UnionWith(MySet<T> other)
    {
        var result = new MySetArray<T>();
        for (int i = 0; i < count; i++) result.Add(items[i]);
        foreach (var elem in other.ToString().Split(", "))
            if (!string.IsNullOrWhiteSpace(elem)) result.Add((T)Convert.ChangeType(elem, typeof(T)));
        return result;
    }

    public override MySet<T> IntersectWith(MySet<T> other)
    {
        var result = new MySetArray<T>();
        for (int i = 0; i < count; i++)
            if (other.Contains(items[i])) result.Add(items[i]);
        return result;
    }

    public override MySet<T> DifferenceWith(MySet<T> other)
    {
        var result = new MySetArray<T>();
        for (int i = 0; i < count; i++)
            if (!other.Contains(items[i])) result.Add(items[i]);
        return result;
    }
    
}