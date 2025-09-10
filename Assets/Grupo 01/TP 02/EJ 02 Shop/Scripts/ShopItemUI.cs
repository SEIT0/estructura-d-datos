using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button buyButton;
    [SerializeField] private Button sellButton; // Nuevo botón para vender
    [SerializeField] private TMP_Text sellButtonText; // Texto del botón vender

    private int itemId;
    private Shop shop;
    private int sellPrice;

    public void Setup(ItemSO item, Shop shopRef)
    {
        shop = shopRef;
        itemId = item.ID;

        icon.sprite = item.Sprite;
        nameText.text = item.ItemName;
        priceText.text = $"Comprar: ${item.Price}";

        // Calcular precio de venta: 60% del original, redondeado
        sellPrice = Mathf.RoundToInt(item.Price * 0.6f);
        sellButtonText.text = $"Vender: ${sellPrice}";

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => shop.TryToBuy(itemId));

        sellButton.onClick.RemoveAllListeners();
        sellButton.onClick.AddListener(() => shop.TryToSellFor(itemId, sellPrice));
    }
}