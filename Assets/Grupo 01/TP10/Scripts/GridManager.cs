using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public int width = 17;
    [SerializeField] public int height = 7;
    public float cellScale = 1f;     // escala de cada celda
    public float cellSpacing = 1.05f; // factor de separación entre celdas
    public GameObject cellPrefab;
    public Cell[,] grid;

    void Start()
    {
        GenerateGrid();
    }

    public void Regenerate()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {

        grid = new Cell[width, height];

        // Centra la grilla
        Vector3 offset = new Vector3(-width / 1.91f + 0.5f, -height / 2f + 0.5f, 0);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Aplicamos el espaciado multiplicando por cellSpacing
                var pos = new Vector3(x * cellSpacing, y * cellSpacing, 0) + offset;
                var obj = Instantiate(cellPrefab, pos, Quaternion.identity, transform);
                obj.transform.localScale = Vector3.one * cellScale;

                var cell = obj.GetComponent<Cell>();
                cell.gridPos = new Vector2Int(x, y);
                cell.sr = obj.GetComponent<SpriteRenderer>();
                grid[x, y] = cell;
            }
        }

    }

    public Cell GetCell(Vector2Int pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= width || pos.y >= height) return null;
        return grid[pos.x, pos.y];
    }
}