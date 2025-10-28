using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    public static List<Cell> FindPath(GridManager grid, Cell start, Cell end)
    {
        var open = new List<Cell> { start };
        var cameFrom = new Dictionary<Cell, Cell>();
        var gScore = new Dictionary<Cell, int> { [start] = 0 };

        while (open.Count > 0)
        {
            // Elegir el nodo con menor costo (heurística Manhattan)
            Cell current = open[0];
            foreach (var c in open)
            {
                if (gScore[c] + Heuristic(c, end) < gScore[current] + Heuristic(current, end))
                    current = c;
            }

            if (current == end)
                return ReconstructPath(cameFrom, current);

            open.Remove(current);

            foreach (var neighbor in GetNeighbors(grid, current))
            {
                if (!neighbor.IsWalkable) continue;

                int tentative = gScore[current] + 1;
                if (!gScore.ContainsKey(neighbor) || tentative < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative;
                    if (!open.Contains(neighbor))
                        open.Add(neighbor);
                }
            }
        }
        return null; // No hay camino
    }

    static int Heuristic(Cell a, Cell b) =>
        Mathf.Abs(a.gridPos.x - b.gridPos.x) + Mathf.Abs(a.gridPos.y - b.gridPos.y);

    static List<Cell> GetNeighbors(GridManager grid, Cell cell)
    {
        var dirs = new Vector2Int[] {
            Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
        };
        var result = new List<Cell>();
        foreach (var d in dirs)
        {
            var n = grid.GetCell(cell.gridPos + d);
            if (n != null) result.Add(n);
        }
        return result;
    }

    static List<Cell> ReconstructPath(Dictionary<Cell, Cell> cameFrom, Cell current)
    {
        var path = new List<Cell> { current };
        while (cameFrom.ContainsKey(current))
        {
            current = cameFrom[current];
            path.Insert(0, current);
        }
        return path;
    }
}