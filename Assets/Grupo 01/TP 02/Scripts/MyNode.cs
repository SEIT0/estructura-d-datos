using System.Collections.Generic;

public class MyNode<T>
{
    public T Data { get; set; }
    public MyNode<T> Previous { get; set; }
    public MyNode<T> Next { get; set; }

    public MyNode(T data)
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