using System;

public class MyBST<T> where T : IComparable<T>
{
    public BSTNode<T> Root { get; protected set; }

    public void Insert(T value)
    {
        Root = Insert(Root, value);
    }

    private BSTNode<T> Insert(BSTNode<T> node, T value)
    {
        if (node == null)
            return new BSTNode<T>(value);

        if (value.CompareTo(node.Value) < 0)
            node.Left = Insert(node.Left, value);
        else if (value.CompareTo(node.Value) > 0)
            node.Right = Insert(node.Right, value);

        return node;
    }

    public int GetHeight()
    {
        return GetHeight(Root);
    }

    private int GetHeight(BSTNode<T> node)
    {
        if (node == null) return 0;
        return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    }

    public int GetBalanceFactor(BSTNode<T> node)
    {
        if (node == null) return 0;
        return GetHeight(node.Left) - GetHeight(node.Right);
    }

    public SimpleList<T> InOrder()
    {
        var result = new SimpleList<T>();
        InOrder(Root, result);
        return result;
    }

    private void InOrder(BSTNode<T> node, SimpleList<T> result)
    {
        if (node == null) return;
        InOrder(node.Left, result);
        result.Add(node.Value);
        InOrder(node.Right, result);
    }

    public SimpleList<T> PreOrder()
    {
        var result = new SimpleList<T>();
        PreOrder(Root, result);
        return result;
    }

    private void PreOrder(BSTNode<T> node, SimpleList<T> result)
    {
        if (node == null) return;
        result.Add(node.Value);
        PreOrder(node.Left, result);
        PreOrder(node.Right, result);
    }

    public SimpleList<T> PostOrder()
    {
        var result = new SimpleList<T>();
        PostOrder(Root, result);
        return result;
    }

    private void PostOrder(BSTNode<T> node, SimpleList<T> result)
    {
        if (node == null) return;
        PostOrder(node.Left, result);
        PostOrder(node.Right, result);
        result.Add(node.Value);
    }

    public SimpleList<T> LevelOrder()
    {
        var result = new SimpleList<T>();
        if (Root == null) return result;

        MyQueue<BSTNode<T>> queue = new MyQueue<BSTNode<T>>();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            result.Add(node.Value);
            if (node.Left != null) queue.Enqueue(node.Left);
            if (node.Right != null) queue.Enqueue(node.Right);
        }
        return result;
    }
}