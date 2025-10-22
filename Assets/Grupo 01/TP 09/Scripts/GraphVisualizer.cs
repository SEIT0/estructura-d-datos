using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GraphVisualizer : MonoBehaviour
{
    public GameManagerGraph gameManager;
    private Dictionary<string, PlanetNode> planetNodes;

    void Start()
    {
        planetNodes = new Dictionary<string, PlanetNode>();
        foreach (var node in Object.FindObjectsByType<PlanetNode>(FindObjectsSortMode.None))
        {
            planetNodes[node.planetName] = node;
        }

        DrawEdges();
    }

    void DrawEdges()
    {
        foreach (var from in gameManager.graph.Vertices)
        {
            foreach (var edge in gameManager.graph.GetEdges(from))
            {
                var fromNode = planetNodes[from];
                var toNode = planetNodes[edge.Item1];

                var lineObj = new GameObject($"Edge_{from}_{edge.Item1}");
                lineObj.transform.parent = this.transform;

                var lr = lineObj.AddComponent<LineRenderer>();
                lr.material = new Material(Shader.Find("Sprites/Default"));
                lr.startColor = lr.endColor = Color.yellow;
                lr.startWidth = lr.endWidth = 0.05f;
                lr.positionCount = 2;
                lr.SetPosition(0, fromNode.transform.position);
                lr.SetPosition(1, toNode.transform.position);
                lr.sortingOrder = 1;

                var textObj = new GameObject($"Weight_{from}_{edge.Item1}");
                textObj.transform.parent = this.transform;
                var tmp = textObj.AddComponent<TextMeshPro>();
                tmp.text = edge.Item2.ToString("F1"); 
                tmp.fontSize = 3;
                tmp.alignment = TextAlignmentOptions.Center;

                Vector2 midPoint = (fromNode.transform.position + toNode.transform.position) / 2;
                textObj.transform.position = midPoint;
            }
        }
    }
}