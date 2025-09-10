using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum SortCriteria { ID, Nombre, Precio }
public enum SortAlgorithm { Bubble, Selection, Insertion }

public class Shop : MonoBehaviour
{
    Dictionary<int, StoreItem> itemStock;
    [SerializeField] ItemListSO allItems;
    [SerializeField] private int playerMoney = 100;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private Transform contentParent;
    [SerializeField] private ShopItemUI shopItemPrefab;

    private SimpleList<ItemSO> list;

    void Start()
    {
        LoadItems();
        UpdateMoneyUI();
    }

    void LoadItems()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        list = new SimpleList<ItemSO>();
        list.AddRange(allItems.items);

        SortItems(SortCriteria.Precio, SortAlgorithm.Bubble);

        // Crear stock y UI
        itemStock = new Dictionary<int, StoreItem>();
        for (int i = 0; i < list.Count; i++)
        {
            var item = list[i];
            itemStock.Add(item.ID, new StoreItem(item, 99));
            var ui = Instantiate(shopItemPrefab, contentParent);
            ui.Setup(item, this);
        }
    }

    public void SortItems(SortCriteria criteria, SortAlgorithm algorithm)
    {
        Comparison<ItemSO> comparison = null;

        switch (criteria)
        {
            case SortCriteria.ID:
                comparison = (a, b) => a.ID.CompareTo(b.ID);
                break;
            case SortCriteria.Nombre:
                comparison = (a, b) => a.ItemName.CompareTo(b.ItemName);
                break;
            case SortCriteria.Precio:
                comparison = (a, b) => a.Price.CompareTo(b.Price);
                break;
        }

        switch (algorithm)
        {
            case SortAlgorithm.Bubble:
                list.BubbleSort(comparison);
                break;
            case SortAlgorithm.Selection:
                list.SelectionSort(comparison);
                break;
            case SortAlgorithm.Insertion:
                list.InsertionSort(comparison);
                break;
        }

        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        for (int i = 0; i < list.Count; i++)
        {
            var item = list[i];
            var ui = Instantiate(shopItemPrefab, contentParent);
            ui.Setup(item, this);
        }
    }

    public bool TryToBuy(int itemId)
    {
        if (!itemStock.ContainsKey(itemId)) return false;
        var storeItem = itemStock[itemId];
        if (storeItem.quantity <= 0) return false;

        if (playerMoney >= storeItem.item.Price)
        {
            playerMoney -= storeItem.item.Price;
            storeItem.quantity--;
            playerInventory.AddItem(storeItem.item);
            UpdateMoneyUI();
            Debug.Log($"Compraste {storeItem.item.ItemName}");
            return true;
        }
        return false;
    }

    public bool TryToSellFor(int itemId, int priceToGive)
    {
        if (!playerInventory.HasItem(itemId)) return false;

        var storeItem = itemStock[itemId];
        playerInventory.RemoveItem(storeItem.item);
        playerMoney += priceToGive;
        storeItem.quantity++;
        UpdateMoneyUI();
        Debug.Log($"Vendiste {storeItem.item.ItemName} por ${priceToGive}");
        return true;
    }

    void UpdateMoneyUI()
    {
        moneyText.text = $"Dinero: ${playerMoney}";
    }
}