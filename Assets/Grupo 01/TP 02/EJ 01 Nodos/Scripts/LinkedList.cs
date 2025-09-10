using System;
using System.Text;

public class MyLinkedList<T> : ISimpleList<T>
{
    private MisNodos<T> root;
    private MisNodos<T> tail;

    public int Count { get; private set; }

    public MyLinkedList()
    {
        root = null;
        tail = null;
        Count = 0;
    }

    //ISimpleList<T>
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

    //ISimpleList<T>
    public void Add(T value)
    {
        var newNode = new MisNodos<T>(value);
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
    
    public void AddRange(MyLinkedList<T> values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));
        for (int i = 0; i < values.Count; i++)
            Add(values[i]);
    }
    
    public void AddRange(T[] values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));
        for (int i = 0; i < values.Length; i++)
            Add(values[i]);
    }
    
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
    
    public void RemoveAt(int index)
    {
        var node = GetNodeAt(index);
        if (node == null) throw new ArgumentOutOfRangeException(nameof(index), "Index fuera de rango.");
        RemoveNode(node);
    }
    
    public void Insert(int index, T value)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Index fuera de rango.");

        if (index == Count) { Add(value); return; }

        var current = GetNodeAt(index);
        var newNode = new MisNodos<T>(value)
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

    public bool IsEmpty()
    {
        return Count == 0;
    }

    public void Clear()
    {
        root = null;
        tail = null;
        Count = 0;
    }
   
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
    
    private MisNodos<T> GetNodeAt(int index)
    {
        if (index < 0 || index >= Count) return null;
        
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

    private void RemoveNode(MisNodos<T> node)
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

    public void BubbleSort(Comparison<T> comparison)
    {        
        bool swapped;

        do
        {
            swapped = false;
            var current = root;
            while (current != null && current.Next != null)
            {
                if (comparison(current.Data, current.Next.Data) > 0)
                {
                    //El intercambio de valores
                    T temp = current.Data;
                    current.Data = current.Next.Data;
                    current.Next.Data = temp;
                    swapped = true;
                }
                current = current.Next;
            }
        } while (swapped);
    }

    public void SelectionSort(Comparison<T> comparison)
    {        
        var current = root;
        while (current != null)
        {
            var minNode = current;
            var runner = current.Next;

            while (runner != null)
            {
                if (comparison(runner.Data, minNode.Data) < 0)
                    minNode = runner;

                runner = runner.Next;
            }

            //Se intercambian
            if (!ReferenceEquals(minNode, current))
            {
                T temp = current.Data;
                current.Data = minNode.Data;
                minNode.Data = temp;
            }

            current = current.Next;
        }
    }

    public void Bogosort(Comparison<T> comparison)
    {        
        Random rng = new Random();

        while (!IsSorted(comparison))
        {
            
            var current = root;
            int n = Count;
            while (current != null)
            {
                //Se elige un nodo al azar
                int randIndex = rng.Next(n);
                var randomNode = GetNodeAt(randIndex);

                //Se intercambian los datos
                T temp = current.Data;
                current.Data = randomNode.Data;
                randomNode.Data = temp;

                current = current.Next;
            }
        }
    }

    private bool IsSorted(Comparison<T> comparison)
    {        
        var current = root;
        while (current != null && current.Next != null)
        {
            if (comparison(current.Data, current.Next.Data) > 0)
                return false;
            current = current.Next;
        }
        return true;
    }
}