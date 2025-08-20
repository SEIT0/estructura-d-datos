using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //ID -> cantidad
    private Dictionary<int, int> inventory = new Dictionary<int, int>();

    public void AddItem(ItemSO item)
    {
        if (inventory.ContainsKey(item.ID))
        {
            inventory[item.ID]++;
        }

        else
        {
            inventory[item.ID] = 1;
        }

    }

    public void RemoveItem(ItemSO item)
    {
        if (!inventory.ContainsKey(item.ID)) return;

        inventory[item.ID]--;
        if (inventory[item.ID] <= 0)
            inventory.Remove(item.ID);
    }
}
