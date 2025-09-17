using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Aca tenemos otro asset que contiene todos los Items posibles
[CreateAssetMenu(fileName = "ItemListSO", menuName = "ScriptableObjects/ItemList")]
public class ItemListSO : ScriptableObject
{
    //Usamos un array porque no vamos a modificarlo en runtime
    public ItemSO[] items;

    //OnValidate se llama al cambiar un valor en Inspector
    //En este caso, al agregar, remover o cambiar items
    private void OnValidate()
    {
        //Asignamos los IDs de todos los items
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