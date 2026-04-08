using System;
using System.Collections;
using System.Collections.Generic;
using Recolectables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    #region Properties
    [SerializeField]Image _icon;
    [SerializeField] TextMeshProUGUI _textMeshPro;
    [SerializeField] TextMeshProUGUI _textprice;
    [SerializeField] TextMeshProUGUI _textdescription;
    [SerializeField] Button buyButton;

   public  Action<Items> onBuy;
    #endregion

    #region Unity Callbacks
    public void Setup(Items newItem)
    {
        _icon.sprite = newItem.icon;
        _textprice.text = newItem.price.ToString()+"$";
       
        _textMeshPro.text = newItem.itemName;
        _textdescription.text = newItem.description;

        // Limpiamos eventos previos para evitar múltiples llamadas al hacer click
        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(()=>onBuy.Invoke(newItem));
    }
    public void SetAffordable(bool canBuy)
    {
        _textprice.color = canBuy ? Color.white : Color.red;
    }
    #endregion
}
