using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{

    private Dictionary<int, int> inventory = new Dictionary<int, int>();
    public int Count = 0;
    [SerializeField] private TMP_Text Inventory;
    [SerializeField] private ItemListSO allItems;

    void Start()
    {
        countItems();
    }

    public void AddItem(ItemSO item)
    {
        if (inventory.ContainsKey(item.ID))
        {
            inventory[item.ID]++;
            Count ++;
        }

        else
        {
            inventory[item.ID] = 1;
            Count = 1;
        }

        countItems();
    }

    public void RemoveItem(ItemSO item)
    {
        if (!inventory.ContainsKey(item.ID))
        {
            return;
        }

        else
        {
            inventory[item.ID]--;
            if (inventory[item.ID] <= 0)
            {
                inventory.Remove(item.ID);
            }
        }

        Count --;

        countItems();
    }
    public bool HasItem(int id)
    {
        return inventory.ContainsKey(id) && inventory[id] > 0;
    }
    public void countItems()
    {
        string text = "Inventory:\n";

        for (int i = 0; i < inventory.Keys.Count; i++)
        {
            int itemId = new List<int>(inventory.Keys)[i];   //saco el ID en la posición i
            int cantidad = inventory[itemId];                //accedo a la cantidad con ese ID

            ItemSO item = allItems.items[itemId];
            if (item != null)
            {
                text += $"{item.ItemName} x{cantidad}\n";
            }
        }

        Inventory.text = text;

    }
}
