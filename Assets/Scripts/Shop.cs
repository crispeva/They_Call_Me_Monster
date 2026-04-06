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
    // [SerializeField] List<WeaponData> weaponList = new List<WeaponData>();
    // [SerializeField] List<GameObject> ItemList = new List<GameObject>();
    [Header("Items")]
    [SerializeField] List<Items> allItems;
    [SerializeField] private GameObject _panelshop;
    [SerializeField] private TextMeshProUGUI _textpanelshop;
    [Header("Player")]
    [SerializeField] GameObject player;
    Inventory _playerInventory;
    int goldplayer;

    // Variables para controlar la interacción con el jugador y la UI
    bool isPlayerInShopRange = false;
    public Transform contentParent; // donde van los items (Grid / Vertical Layout)
    public GameObject itemPrefab;   // el prefab del slot
    Action <bool>shopping;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _playerInventory = player.GetComponent<Inventory>();

    }
    void Start()
    {

        GenerateShop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&& isPlayerInShopRange)
        {
            Debug.Log("Player entered the shop!");
            _panelshop.SetActive(true);

        }
        else if ( !isPlayerInShopRange || Input.GetKeyDown(KeyCode.E))
        {
            _panelshop.SetActive(false);
        }
    }
    #endregion



    #region Trigger Callbacks

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerHealth = other.gameObject.GetComponent<HealthSystem>();
            _playerInventory = other.gameObject.GetComponent<Inventory>();
            isPlayerInShopRange= true;
            AnimationTextcaldero();
          
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInShopRange= false;
            AnimationTextcalderoOUT();
            _panelshop.SetActive(false);
        }
    }
    #endregion

    #region Shop Animations and buttons
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
    public void GenerateShop()
    {
        foreach (var item in allItems)
        {
            GameObject slotitem = Instantiate(itemPrefab, contentParent);

            ShopItemSlot ui = slotitem.GetComponent<ShopItemSlot>();
            ui.Setup(item);
            ui.onBuy += BuyItem;
        }
    }
    public void BuyItem(Items item)
    {
        goldplayer = _playerInventory.GetAmount(RecolectableType.Coin);
        if (goldplayer < item.price)
        {
            Debug.Log("No tienes suficiente oro");
            return;
        }
        _playerInventory.UseResource(RecolectableType.Coin, item.price);
        Debug.Log(goldplayer);
        item.Use(player);
    }
    #endregion

}
