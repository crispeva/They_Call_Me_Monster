using DG.Tweening;
using Recolectables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIGameController : MonoBehaviour
{
    #region Properties
    [Header("Live")]
    [SerializeField] private Slider _playerHealth; 
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _waveCountText;
    [SerializeField] private TextMeshProUGUI _coinText;


    [Header("Event_Shop")]
    [SerializeField] private TextMeshProUGUI _textcaldero;
    [SerializeField] private Inventory _playerInventory;
    #endregion

    #region Fields
    #endregion
    void Update()
    {
        //AnimationTextcaldero();
    }
    #region Unity Callbacks
    void Awake()
    {

    }
    void Start()
    {
        _playerInventory.OnInventoryUpdated += UpdateCoinUI;
    }

    private void UpdateCoinUI()
    {
        _coinText.text = _playerInventory.GetAmount(RecolectableType.Coin).ToString();
    }
   
    // Update is called once per 
    #endregion

    #region UI Waves
    public void UpdateWaveNumber(int waveNumber)
    {
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(_waveCountText.DOFade(0f, 0.5f))
            .AppendCallback(() => _waveCountText.text = $"wave: {waveNumber}")
            .Append(_waveCountText.DOFade(1f, 0.5f));
    }

    public void UpdateEnemiesNumber(int EnemiesNumber)
    {
        if (EnemiesNumber <= 0)
            return;
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(_waveText.DOFade(0f, 0.8f))
            .AppendCallback(() => _waveText.text = $"enemies remain: {EnemiesNumber}")
            .Append(_waveText.DOFade(1f, 0.8f));
    }

    public void UpdateWaveCountdown(int secondsRemaining)
    {
        DG.Tweening.Sequence sequence = DOTween.Sequence();
        sequence.Append(_waveText.DOFade(0f, 0.3f))
            .AppendCallback(() => _waveText.text = $"next wave in: {secondsRemaining} seg")
            .Append(_waveText.DOFade(1f, 0.3f));
    }
    #endregion
    private void OnEnable()
    {
        // Suscribirse a eventos o inicializar datos aquí si es necesario
    }
    #region UIPlayer
    internal void UpdatePlayerHealth(float value)
    {
        _playerHealth.value = value;
    }

    #endregion

}
