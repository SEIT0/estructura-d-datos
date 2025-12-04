using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] public int width = 17;
    [SerializeField] public int height = 7;
    public float cellScale = 1f;     // escala de cada celda
    // Espaciado entre centros de celdas. Por defecto igual a la escala para que no queden huecos.
    public float cellSpacing = 1f;
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

        // Centra la grilla: calcular offset de tal forma que la celda (0,0) y la (width-1,height-1)
        // queden simétricas respecto al origen. Usamos cellSpacing para posicionar los centros.
        Vector3 offset = new Vector3(-((width - 1) * cellSpacing) / 2f, -((height - 1) * cellSpacing) / 2f, 0);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Posición basada en el espaciado entre centros de celda
                var pos = new Vector3(x * cellSpacing, y * cellSpacing, 0f) + offset;
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