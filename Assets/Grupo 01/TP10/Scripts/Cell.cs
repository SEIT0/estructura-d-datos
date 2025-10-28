using UnityEngine;
public enum CellType { Empty, Wall, Start, End }
public class Cell : MonoBehaviour
{
    public Vector2Int gridPos;
    public CellType type = CellType.Empty;
    public SpriteRenderer sr;

    void OnMouseDown()
    {
        // Llama al controlador para pintar esta celda
        Object.FindFirstObjectByType<MazeController>().PaintCell(this);
    }

    public void SetType(CellType newType, Color color)
    {
        type = newType;
        sr.color = color;
    }

    public bool IsWalkable => type != CellType.Wall;
}