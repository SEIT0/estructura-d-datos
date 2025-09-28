using UnityEngine;
using TMPro;

public class TreeVisualizer : MonoBehaviour
{
    public GameObject nodePrefab; // Prefab con un círculo/esfera + TextMeshPro
    public float xSpacing = 2f;
    public float ySpacing = 2f;

    public void DrawTree<T>(BSTNode<T> root, Vector2 position, float spread) where T : System.IComparable<T>
    {
        if (root == null) return;

        // Instanciar nodo
        GameObject nodeObj = Instantiate(nodePrefab, position, Quaternion.identity, transform);
        var textMesh = nodeObj.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
            textMesh.text = root.Value.ToString();

        // Hijo izquierdo
        if (root.Left != null)
        {
            Vector2 leftPos = position + new Vector2(-spread, -ySpacing);
            DrawLine(position, leftPos);
            DrawTree(root.Left, leftPos, spread / 2f); // se va reduciendo
        }

        // Hijo derecho
        if (root.Right != null)
        {
            Vector2 rightPos = position + new Vector2(spread, -ySpacing);
            DrawLine(position, rightPos);
            DrawTree(root.Right, rightPos, spread / 2f);
        }
    }

    void DrawLine(Vector3 start, Vector3 end)
    {
        var lineObj = new GameObject("Line");
        lineObj.transform.parent = transform; // que quede organizado
        var lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startWidth = 0.05f;
        lr.endWidth = 0.05f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.endColor = Color.white;
        lr.sortingOrder = -1; // para que no tape los números
    }

}
