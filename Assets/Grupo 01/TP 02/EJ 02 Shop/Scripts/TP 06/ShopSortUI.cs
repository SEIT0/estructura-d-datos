using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopSortUI : MonoBehaviour
{
    [SerializeField] private Shop shop;
    [SerializeField] private TMP_Dropdown criteriaDropdown;
    [SerializeField] private TMP_Dropdown algorithmDropdown;

    public void OnSortButtonClicked()
    {
        SortCriteria criteria = SortCriteria.ID;
        SortAlgorithm algorithm = SortAlgorithm.Bubble;

        switch (criteriaDropdown.value)
        {
            case 0: criteria = SortCriteria.ID; break;
            case 1: criteria = SortCriteria.Nombre; break;
            case 2: criteria = SortCriteria.Precio; break;
        }

        switch (algorithmDropdown.value)
        {
            case 0: algorithm = SortAlgorithm.Bubble; break;
            case 1: algorithm = SortAlgorithm.Selection; break;
            case 2: algorithm = SortAlgorithm.Insertion; break;
        }

        shop.SortItems(criteria, algorithm);
    }
}