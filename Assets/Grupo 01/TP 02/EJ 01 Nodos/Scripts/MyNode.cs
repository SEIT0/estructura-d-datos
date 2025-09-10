using System.Collections.Generic;

public class Nodos<T>
{
    public T Data { get; set; }
    public Nodos<T> Previous { get; set; }
    public Nodos<T> Next { get; set; }

    public Nodos(T data)
    {
        Data = data;
        Previous = null;
        Next = null;
    }

    public override string ToString()
    {
        return Data?.ToString() ?? "null";
    }

    public bool IsEquals(T value)
    {
        return EqualityComparer<T>.Default.Equals(Data, value);
    }
}