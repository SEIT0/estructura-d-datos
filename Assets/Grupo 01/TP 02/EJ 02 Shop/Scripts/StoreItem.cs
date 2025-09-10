using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItem 
{
    public ItemSO item;
    public int quantity;

    public StoreItem(ItemSO item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}