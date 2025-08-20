using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button buyButton;

    private ItemSO currentItem;
    private Shop shop;

    public void Setup(ItemSO item, Shop shopRef)
    {
        currentItem = item;
        shop = shopRef;

        icon.sprite = item.Sprite;
        nameText.text = item.ItemName;
        priceText.text = $"${item.Price}";
        buyButton.onClick.AddListener(OnBuy);
    }

    void OnBuy()
    {
        shop.TryToBuy(currentItem.ID);
    }

}
