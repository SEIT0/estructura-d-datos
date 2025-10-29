using UnityEngine;
using TMPro;

public class TreeVisualizer : MonoBehaviour
{
    public GameObject nodePrefab;
    public float xSpacing = 2f;
    public float ySpacing = 2f;

    public void DrawTree<T>(BSTNode<T> root, Vector2 position, float spread) where T : System.IComparable<T>
    {
        if (root == null) return;

        //Instanciar nodo
        GameObject nodeObj = Instantiate(nodePrefab, position, Quaternion.identity, transform);
        var textMesh = nodeObj.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
            textMesh.text = root.Value.ToString();
        textMesh.fontSize = 2;

        //Hijo izquierdo
        if (root.Left != null)
        {
            Vector2 leftPos = position + new Vector2(-spread, -ySpacing);
            DrawLine(position, leftPos);
            DrawTree(root.Left, leftPos, spread / 2f); //se va reduciendo
        }

        //Hijo derecho
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
        lineObj.transform.parent = transform; //que quede organizado
        var lr = lineObj.AddComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.startWidth = 0.5f;
        lr.endWidth = 0.5f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        Color brown = new Color(0.55f, 0.27f, 0.07f);
        lr.startColor = brown;
        lr.endColor = brown;
        lr.sortingOrder = -1; //para que no tape los números
    }

}
