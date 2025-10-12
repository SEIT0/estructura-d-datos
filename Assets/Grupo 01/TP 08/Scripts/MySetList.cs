using System;

public class MySetList<T> : MySet<T>
{
    private SimpleList<T> items = new SimpleList<T>();

    public override int Count => items.Count;
    public override bool IsEmpty => items.Count == 0;

    public override void Add(T item)
    {
        if (!Contains(item))
            items.Add(item);
    }

    public override bool Remove(T item) => items.Remove(item);

    public override void Clear() => items.Clear();

    public override bool Contains(T item)
    {
        for (int i = 0; i < items.Count; i++)
            if (Equals(items[i], item)) return true;
        return false;
    }

    public override void Show() => Console.WriteLine(ToString());

    public override string ToString() => IsEmpty ? "Set vacío" : items.ToString();
    public override SimpleList<T> GetElements()
    {
        return items;
    }

    public override MySet<T> UnionWith(MySet<T> other)
    {
        var result = new MySetList<T>();
        for (int i = 0; i < items.Count; i++) result.Add(items[i]);
        var otherItems = other.GetElements();
        for (int i = 0; i < otherItems.Count; i++) result.Add(otherItems[i]);
        return result;
    }

    public override MySet<T> IntersectWith(MySet<T> other)
    {
        var result = new MySetList<T>();
        for (int i = 0; i < items.Count; i++)
            if (other.Contains(items[i])) result.Add(items[i]);
        return result;
    }

    public override MySet<T> DifferenceWith(MySet<T> other)
    {
        var result = new MySetList<T>();
        for (int i = 0; i < items.Count; i++)
            if (!other.Contains(items[i])) result.Add(items[i]);
        return result;
    }
   
}