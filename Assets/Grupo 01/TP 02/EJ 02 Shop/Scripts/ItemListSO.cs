using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemListSO", menuName = "ScriptableObjects/ItemList")]
public class ItemListSO : ScriptableObject
{

    public ItemSO[] items;

    private void OnValidate()
    {
        //Se asignan los IDs a todos los items
        if (items == null) return;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                items[i].ID = i;
            }
        }
    }
}