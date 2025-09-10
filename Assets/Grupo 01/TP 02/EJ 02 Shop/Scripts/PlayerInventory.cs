using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    //ID -> cantidad
    private Dictionary<int, int> inventory = new Dictionary<int, int>();
    public Text displayText;              // Mostrar la lista y mensajes
    public int Count = 0;                                     

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
        if (!inventory.ContainsKey(item.ID)) return;

        inventory[item.ID]--;
        if (inventory[item.ID] <= 0)
            inventory.Remove(item.ID);
            Count --;

        countItems();
    }
    public bool HasItem(int id)
    {
        return inventory.ContainsKey(id) && inventory[id] > 0;
    }
    public void countItems()
    {
        displayText.text = "Elementos: " + Count.ToString();
    }
}
