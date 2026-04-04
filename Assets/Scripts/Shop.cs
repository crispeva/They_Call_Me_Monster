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
    [SerializeField] List<Items> allItems;
    [SerializeField] private GameObject _panelshop;
    [SerializeField] private TextMeshProUGUI _textpanelshop;
    HealthSystem playerHealth;
    Inventory playerInventory;
    float healthBuffAmount = 20f;
    bool isPlayerInShopRange = false;

    public Transform contentParent; // donde van los items (Grid / Vertical Layout)
    public GameObject itemPrefab;   // el prefab del slot
    #endregion

    #region Unity Callbacks
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

    public void GenerateShop()
    {
        foreach (var item in allItems)
        {
            GameObject slotitem = Instantiate(itemPrefab, contentParent);

            ShopItemSlot ui = slotitem.GetComponent<ShopItemSlot>();
            ui.Setup(item);
        }
    }
    #region Trigger Callbacks

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.gameObject.GetComponent<HealthSystem>();
            playerInventory = other.gameObject.GetComponent<Inventory>();
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

        Debug.Log("Hola!");
        // Fade suave (opcional)
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_textpanelshop.DOFade(0f, 0.5f))
            .AppendCallback(() => _textpanelshop.text = $"Press 'E' to open")
            .Append(_textpanelshop.DOFade(1f, 0.5f));
    }
    private void AnimationTextcalderoOUT()
    {

        Debug.Log("Hola!");
        // Fade suave (opcional)
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_textpanelshop.DOFade(0f, 0.5f))
            .AppendCallback(() => _textpanelshop.text = $"")
            .Append(_textpanelshop.DOFade(1f, 0.5f));
    }
    void WeaponButton()
    {

    }
    void BuffButton()
    {
        playerHealth.SetHealth(healthBuffAmount);
    }
    void NoMoney()
    {
        if (playerInventory.GetAmount(RecolectableType.Coin)>=2)
        {

        } 
            Debug.Log("Not enough money!");
    }
    #endregion

}
