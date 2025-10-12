using System;

public abstract class MySet<T>
{
    public abstract void Add(T item);
    public abstract bool Remove(T item);
    public abstract void Clear();
    public abstract bool Contains(T item);
    public abstract int Count { get; }
    public abstract bool IsEmpty { get; }
    public abstract void Show();
    public abstract override string ToString();
    public abstract SimpleList<T> GetElements();
    public abstract MySet<T> UnionWith(MySet<T> other);
    public abstract MySet<T> IntersectWith(MySet<T> other);
    public abstract MySet<T> DifferenceWith(MySet<T> other);
    
}