using System.Collections;
using System.Collections.Generic;
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
     Button buyButton;

    #endregion

    #region Fields
    private void Awake()
    {
       
    }
    #endregion

    #region Unity Callbacks
    public void Setup(Items newItem)
    {
        _icon.sprite = newItem.icon;
        _textprice.text = newItem.price.ToString();
       
        _textMeshPro.text = newItem.itemName;
        _textdescription.text = newItem.description;
        
       // buyButton.onClick.RemoveAllListeners();
        //buyButton.onClick.AddListener(() => onBuy(item));
    }
    #endregion
}
