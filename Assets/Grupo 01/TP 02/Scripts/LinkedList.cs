using System;
using System.Text;

public class MyLinkedList<T> : ISimpleList<T>
{
    private MyNode<T> root;
    private MyNode<T> tail;

    public int Count { get; private set; }

    public MyLinkedList()
    {
        root = null;
        tail = null;
        Count = 0;
    }

    // ISimpleList<T>
    public T this[int index]
    {
        get
        {
            var node = GetNodeAt(index);
            if (node == null) throw new ArgumentOutOfRangeException(nameof(index), "Index fuera de rango.");
            return node.Data;
        }
        set
        {
            var node = GetNodeAt(index);
            if (node == null) throw new ArgumentOutOfRangeException(nameof(index), "Index fuera de rango.");
            node.Data = value;
        }
    }

    // ISimpleList<T>
    public void Add(T value)
    {
        var newNode = new MyNode<T>(value);
        if (IsEmpty())
        {
            root = tail = newNode;
        }
        else
        {
            tail.Next = newNode;
            newNode.Previous = tail;
            tail = newNode;
        }
        Count++;
    }

    // Consigna
    public void AddRange(MyLinkedList<T> values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));
        for (int i = 0; i < values.Count; i++)
            Add(values[i]);
    }

    // ISimpleList<T>
    public void AddRange(T[] values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));
        for (int i = 0; i < values.Length; i++)
            Add(values[i]);
    }

    // ISimpleList<T> — borra la última ocurrencia (para igualar tu SimpleList)
    public bool Remove(T value)
    {
        var current = tail;
        while (current != null)
        {
            if (current.IsEquals(value))
            {
                RemoveNode(current);
                return true;
            }
            current = current.Previous;
        }
        return false;
    }

    // Consigna
    public void RemoveAt(int index)
    {
        var node = GetNodeAt(index);
        if (node == null) throw new ArgumentOutOfRangeException(nameof(index), "Index fuera de rango.");
        RemoveNode(node);
    }

    // Consigna
    public void Insert(int index, T value)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Index fuera de rango.");

        if (index == Count) { Add(value); return; }

        var current = GetNodeAt(index);
        var newNode = new MyNode<T>(value)
        {
            Next = current,
            Previous = current?.Previous
        };

        if (current?.Previous != null)
            current.Previous.Next = newNode;
        else
            root = newNode;

        if (current != null)
            current.Previous = newNode;

        Count++;
    }

    // Consigna
    public bool IsEmpty()
    {
        return Count == 0;
    }

    // ISimpleList<T>
    public void Clear()
    {
        root = null;
        tail = null;
        Count = 0;
    }

    // Consigna — mismo formato que tu SimpleList.ToString()
    public override string ToString()
    {
        if (Count == 0) return "No hay empanadas";

        var sb = new StringBuilder();
        var current = root;
        int i = 0;
        while (current != null)
        {
            sb.Append(current.Data?.ToString());
            if (i < Count - 1) sb.Append(", ");
            current = current.Next;
            i++;
        }
        return sb.ToString();
    }

    // Helpers
    private MyNode<T> GetNodeAt(int index)
    {
        if (index < 0 || index >= Count) return null;

        // Optimización: avanzar desde el lado más cercano
        if (index <= Count / 2)
        {
            var current = root;
            for (int i = 0; i < index; i++) current = current.Next;
            return current;
        }
        else
        {
            var current = tail;
            for (int i = Count - 1; i > index; i--) current = current.Previous;
            return current;
        }
    }

    private void RemoveNode(MyNode<T> node)
    {
        if (node.Previous != null)
            node.Previous.Next = node.Next;
        else
            root = node.Next;

        if (node.Next != null)
            node.Next.Previous = node.Previous;
        else
            tail = node.Previous;

        Count--;
    }
}