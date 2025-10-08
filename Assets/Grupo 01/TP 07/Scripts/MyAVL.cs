using System;
using System.Collections.Generic;

// Árbol AVL genérico que hereda de MyBST
public class MyAVL<T> : MyBST<T> where T : IComparable<T>
{
    // Sobrescribimos Insert para que balancee
    public new void Insert(T value)
    {
        Root = Insert(Root, value);
    }

    private BSTNode<T> Insert(BSTNode<T> node, T value)
    {
        // Inserción normal de BST
        if (node == null)
            return new AVLNode<T>(value);

        if (value.CompareTo(node.Value) < 0)
            node.Left = Insert(node.Left, value);
        else if (value.CompareTo(node.Value) > 0)
            node.Right = Insert(node.Right, value);
        else
            return node; // no se permiten duplicados

        // Actualizar altura
        UpdateHeight(node);

        // Balancear
        return Balance(node, value);
    }

    private void UpdateHeight(BSTNode<T> node)
    {
        var avlNode = node as AVLNode<T>;
        if (avlNode != null)
        {
            avlNode.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
        }
    }

    private int GetHeight(BSTNode<T> node)
    {
        if (node == null) return 0;
        return (node as AVLNode<T>)?.Height ?? 1;
    }

    private int GetBalance(BSTNode<T> node)
    {
        if (node == null) return 0;
        return GetHeight(node.Left) - GetHeight(node.Right);
    }

    private BSTNode<T> Balance(BSTNode<T> node, T value)
    {
        int balance = GetBalance(node);

        // Caso Izquierda - Izquierda
        if (balance > 1 && value.CompareTo(node.Left.Value) < 0)
            return RotateRight(node);

        // Caso Derecha - Derecha
        if (balance < -1 && value.CompareTo(node.Right.Value) > 0)
            return RotateLeft(node);

        // Caso Izquierda - Derecha
        if (balance > 1 && value.CompareTo(node.Left.Value) > 0)
        {
            node.Left = RotateLeft(node.Left);
            return RotateRight(node);
        }

        // Caso Derecha - Izquierda
        if (balance < -1 && value.CompareTo(node.Right.Value) < 0)
        {
            node.Right = RotateRight(node.Right);
            return RotateLeft(node);
        }

        return node;
    }

    private BSTNode<T> RotateRight(BSTNode<T> y)
    {
        BSTNode<T> x = y.Left;
        BSTNode<T> T2 = x.Right;

        // Rotación
        x.Right = y;
        y.Left = T2;

        // Actualizar alturas
        UpdateHeight(y);
        UpdateHeight(x);

        return x;
    }

    private BSTNode<T> RotateLeft(BSTNode<T> x)
    {
        BSTNode<T> y = x.Right;
        BSTNode<T> T2 = y.Left;

        // Rotación
        y.Left = x;
        x.Right = T2;

        // Actualizar alturas
        UpdateHeight(x);
        UpdateHeight(y);

        return y;
    }
}

// Nodo especializado para AVL
public class AVLNode<T> : BSTNode<T> where T : IComparable<T>
{
    public int Height { get; set; }

    public AVLNode(T value) : base(value)
    {
        Height = 1;
    }
}