using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerGraph : MonoBehaviour
{
    public static GameManagerGraph Instance { get; private set; }

    public MyALGraph<string> graph = new MyALGraph<string>();
    public TextMeshProUGUI uiText;

    private List<string> selectedPath = new List<string>();
    private Dictionary<string, PlanetNode> planetNodes;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        // Detectar planetas en la escena
        planetNodes = new Dictionary<string, PlanetNode>();
        foreach (var node in Object.FindObjectsByType<PlanetNode>(FindObjectsSortMode.None))
        {
            planetNodes[node.planetName] = node;
            graph.AddVertex(node.planetName);
        }

        // Conexiones manuales 
        AddConnection("Earth", "Moon");
        AddConnection("Earth", "Mars");
        AddConnection("Earth", "Venus");
        AddConnection("Venus", "Mercury");
        AddConnection("Jupiter", "Mercury");
        AddConnection("Jupiter", "Saturn");
        AddConnection("Pluto", "Uranus");
        AddConnection("Uranus", "Neptune");
        // Podés agregar más conexiones según tu mapa
    }

    private void AddConnection(string from, string to)
    {
        if (planetNodes.ContainsKey(from) && planetNodes.ContainsKey(to))
        {
            float distance = Vector2.Distance(
                planetNodes[from].transform.position,
                planetNodes[to].transform.position
            );

            graph.AddEdge(from, (to, distance));
            // Si querés que sea bidireccional:
            graph.AddEdge(to, (from, distance));
        }
    }

    public void SelectPlanet(string planetName)
    {
        selectedPath.Add(planetName);
        if (planetName == "Sun")
        {
            uiText.text = $"You really wanted to die";
        }
        else 
        { 
            uiText.text = $"Seleccionado: {string.Join(" -> ", selectedPath)}"; 
        }
    }

    public void DeselectPlanet(string planetName)
    {
        selectedPath.Remove(planetName);
        uiText.text = $"Seleccionado: {string.Join(" -> ", selectedPath)}";
    }

    public void ClearSelection()
    {
        selectedPath.Clear();
        uiText.text = "Selección reiniciada";

        foreach (var node in planetNodes.Values)
            node.ResetSelection();
    }

    public void ValidatePath()
    {
        if (selectedPath.Count == 0)
        {
            uiText.text = "No has seleccionado ningún planeta.";
            return;
        }

        if (selectedPath.Count == 1)
        {
            uiText.text = "No has seleccionado planetas suficientes para hacer un camino.";
            return;
        }

        float total = 0f;
        for (int i = 0; i < selectedPath.Count - 1; i++)
        {
            if (!graph.ContainsEdge(selectedPath[i], selectedPath[i + 1]))
            {
                uiText.text = "Camino inválido";
                return;
            }
            total += graph.GetWeight(selectedPath[i], selectedPath[i + 1]);
        }

        uiText.text = $"Camino válido. Distancia total: {total:F2}";
    }
}