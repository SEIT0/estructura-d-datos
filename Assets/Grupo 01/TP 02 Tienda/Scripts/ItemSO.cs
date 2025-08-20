using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta clase va a ser un Wrapper, para separar el Asset de la class Item
//Podriamos hacer que el Item sea el ScriptableObject, pero asi es mas limpio
[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/ItemSO")]
public class ItemSO : ScriptableObject
{
    //Aca va el Item
    [field: SerializeField] private Item Item { get; set; }

    //Todas las propiedades llevan a la variable correspondiente del Item
    public int ID { get => Item.id; set { Item.id = value; } }
    public string ItemName { get => Item.name; set { Item.name = value; } }
    public Sprite Sprite { get => Item.sprite; set { Item.sprite = value; } }
    public int Price { get => Item.price; set { Item.price = value; } }

}