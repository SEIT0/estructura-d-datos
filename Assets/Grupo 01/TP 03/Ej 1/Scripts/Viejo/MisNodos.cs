using System.Collections.Generic;

public class MisNodos<T>
{
    public T Data { get; set; }
    public MisNodos<T> Previous { get; set; }
    public MisNodos<T> Next { get; set; }

    public MisNodos(T data)
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