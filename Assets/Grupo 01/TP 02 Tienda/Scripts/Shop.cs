using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
{
    //ID, Cantidad
    Dictionary<int, StoreItem> itemStock;
    [SerializeField] ItemListSO allItems;

    [SerializeField] private int playerMoney = 100;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private TMP_Text moneyText;


    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el diccionario
        itemStock = new Dictionary<int, StoreItem>();

        //Leemos la lista de todos los items, y cargamos al diccionario (con 99 de cantidad)
        foreach (ItemSO item in allItems.items)
        {
            itemStock.Add(item.ID, new StoreItem(item, 99));
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

            playerInventory.AddItem(storeItem.item); //Se agrega al inventario del player
            Debug.Log($"Compraste {storeItem.item.ItemName}");
            return true;
        }
        return false;
    }
    void UpdateMoneyUI()
    {
        moneyText.text = $"Dinero: ${playerMoney}";
    }
}