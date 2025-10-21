using System;
using System.Collections.Generic;
using System.Linq;

public class MyALGraph<T>
{
    private Dictionary<T, List<(T, float)>> adjacencyList = new Dictionary<T, List<(T, float)>>();

    public IEnumerable<T> Vertices => adjacencyList.Keys;

    public IEnumerable<(T, float)> GetEdges(T from)
    {
        if (adjacencyList.ContainsKey(from))
            return adjacencyList[from];
        return Enumerable.Empty<(T, float)>();
    }

    public void AddVertex(T vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
            adjacencyList[vertex] = new List<(T, float)>();
    }

    public void RemoveVertex(T vertex)
    {
        if (adjacencyList.ContainsKey(vertex))
        {
            adjacencyList.Remove(vertex);
            foreach (var list in adjacencyList.Values)
                list.RemoveAll(edge => edge.Item1.Equals(vertex));
        }
    }

    public void AddEdge(T from, (T, float) edge)
    {
        if (!adjacencyList.ContainsKey(from))
            AddVertex(from);
        if (!adjacencyList.ContainsKey(edge.Item1))
            AddVertex(edge.Item1);

        adjacencyList[from].Add(edge);
    }

    public void RemoveEdge(T from, T to)
    {
        if (adjacencyList.ContainsKey(from))
            adjacencyList[from].RemoveAll(edge => edge.Item1.Equals(to));
    }

    public bool ContainsVertex(T vertex) => adjacencyList.ContainsKey(vertex);

    public bool ContainsEdge(T from, T to)
    {
        return adjacencyList.ContainsKey(from) &&
               adjacencyList[from].Any(edge => edge.Item1.Equals(to));
    }

    public float GetWeight(T from, T to)
    {
        if (adjacencyList.ContainsKey(from))
        {
            var edge = adjacencyList[from].FirstOrDefault(e => e.Item1.Equals(to));
            if (!edge.Equals(default((T, float))))
                return edge.Item2;
        }
        return float.NaN;
    }
}