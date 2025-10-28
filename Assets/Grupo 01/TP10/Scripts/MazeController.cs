using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MazeController : MonoBehaviour
{
    public GridManager grid;
    public TMP_Text statusText;
    public GameObject playerPrefab;
    private GameObject player;
    private CellType currentPaint = CellType.Empty;

    public void CheckSolution()
    {
        Cell start = null, end = null;
        foreach (var c in grid.grid)
        {
            if (c.type == CellType.Start) start = c;
            if (c.type == CellType.End) end = c;
        }

        if (start == null || end == null)
        {
            statusText.text = "Falta entrada o salida";
            return;
        }

        var path = Pathfinding.FindPath(grid, start, end);
        if (path != null)
        {
            statusText.text = "Hay solución";

            // Pintar el camino en amarillo (sin tocar entrada/salida)
            foreach (var step in path)
            {
                if (step.type == CellType.Empty) // no sobreescribir entrada/salida
                    step.sr.color = Color.yellow;
            }

            StartCoroutine(MovePlayer(path));
        }
        else
        {
            statusText.text = "No hay camino";
        }

    }

    public void SetPaintMode(int typeIndex)
    {
        currentPaint = (CellType)typeIndex;
        statusText.text = "Modo: " + currentPaint.ToString();
    }
    public void ClearGrid()
    {
        foreach (var cell in grid.grid)
        {
            cell.SetType(CellType.Empty, Color.white);
        }
        statusText.text = "Mapa limpiado";
    }

    public void PaintCell(Cell cell)
    {
        Color color = Color.white;
        switch (currentPaint)
        {
            case CellType.Start: color = Color.blue; break;
            case CellType.End: color = Color.red; break;
            case CellType.Wall: color = Color.black; break;
            case CellType.Empty: color = Color.white; break;
        }
        cell.SetType(currentPaint, color);
    }

    IEnumerator MovePlayer(List<Cell> path)
    {
        if (player != null) Destroy(player);
        player = Instantiate(playerPrefab, path[0].transform.position, Quaternion.identity);

        foreach (var step in path)
        {
            player.transform.position = step.transform.position;
            yield return new WaitForSeconds(0.3f);
        }

        // Restaurar colores originales (opcional)
        foreach (var step in path)
        {
            if (step.type == CellType.Empty)
                step.sr.color = Color.white;
        }
    }

}