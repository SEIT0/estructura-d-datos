using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSTNode<T> where T : IComparable<T>
{
    public T Value;
    public BSTNode<T> Left;
    public BSTNode<T> Right;

    public BSTNode(T value)
    {
        Value = value;
        Left = null;
        Right = null;
    }
}
