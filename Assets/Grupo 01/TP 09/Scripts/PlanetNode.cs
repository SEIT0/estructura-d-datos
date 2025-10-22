using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class PlanetNode : MonoBehaviour
{
    public string planetName;
    private SpriteRenderer sr;
    private bool isSelected;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (string.IsNullOrEmpty(planetName))
            planetName = gameObject.name; 
    }

    void OnMouseDown()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            GameManagerGraph.Instance.SelectPlanet(planetName);
            sr.color = Color.yellow;
        }
        else
        {
            GameManagerGraph.Instance.DeselectPlanet(planetName);
            sr.color = Color.white;
        }
    }

    public void ResetSelection()
    {
        isSelected = false;
        sr.color = Color.white;
    }
}