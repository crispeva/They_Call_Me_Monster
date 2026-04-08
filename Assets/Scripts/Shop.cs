using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Recolectables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class Shop : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [Header("Items")]
    [SerializeField] List<Items> allItems;
    [SerializeField] private TextMeshProUGUI _textpanelshop;
    [Header("Player")]
    GameObject player;
    Inventory _playerInventory;
    int goldplayer;
    [SerializeField] List<GameObject> allItemsPrefabs;
    GameObject slotitem;
    // Variables para controlar la interacción con el jugador y la UI
    bool isPlayerInShopRange = false;
    public Transform contentParent; // donde van los items (Grid / Vertical Layout)
    public GameObject itemPrefab;   // el prefab del slot
   public Action<bool> shopping;
   public Action Onbougth;
    private bool isShopOpen = false;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        GenerateShop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isPlayerInShopRange)
            {
                isShopOpen = !isShopOpen; // Cambiamos el estado local
                shopping?.Invoke(isShopOpen); // Avisamos al resto del juego
                CantBuyItem();
            }
        }
        // Si el jugador se va, cerramos la tienda forzosamente
        if (!isPlayerInShopRange && isShopOpen)
        {
            isShopOpen = false;
            shopping?.Invoke(false);
        }
    }
    #endregion



    #region Trigger Callbacks

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInventory = other.gameObject.GetComponent<Inventory>();
            player= other.gameObject;
            isPlayerInShopRange = true;
            AnimationTextcaldero();
          
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInShopRange= false;
            AnimationTextcalderoOUT();
            shopping?.Invoke(false);
        }
    }
    #endregion
    #region Shop text Animations
    private void AnimationTextcaldero()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_textpanelshop.DOFade(0f, 0.5f))
            .AppendCallback(() => _textpanelshop.text = $"Press 'E' to open")
            .Append(_textpanelshop.DOFade(1f, 0.5f));
    }
    private void AnimationTextcalderoOUT()
    {

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_textpanelshop.DOFade(0f, 0.5f))
            .AppendCallback(() => _textpanelshop.text = $"")
            .Append(_textpanelshop.DOFade(1f, 0.5f));
    }
    #endregion

    #region Shop Animations and buttons
    private void CantBuyItem()
    {
        goldplayer = _playerInventory.GetAmount(RecolectableType.Coin);

        for (int i = 0; i < allItems.Count; i++)
        {
            Items item = allItems[i];
            GameObject slot = allItemsPrefabs[i];


            ShopItemSlot ui = slot.GetComponent<ShopItemSlot>();
            ui.SetAffordable(goldplayer >= item.price);
           
            

        }
    }
    public void GenerateShop()
    {
        foreach (var item in allItems)
        {
             slotitem = Instantiate(itemPrefab, contentParent);
            Debug.Log(slotitem);
            allItemsPrefabs.Add(slotitem);
            ShopItemSlot ui = slotitem.GetComponent<ShopItemSlot>();
            
            ui.Setup(item);
            
            ui.onBuy += BuyItem;
        }
    }

    public void BuyItem(Items item)
    {
        goldplayer = _playerInventory.GetAmount(RecolectableType.Coin);

        _playerInventory.UseResource(RecolectableType.Coin, item.price);
        Debug.Log(goldplayer);
        
        if (goldplayer >= item.price)
        {
            item.Use(player);
            Onbougth?.Invoke();
        }
        CantBuyItem();
    }
    #endregion

}
