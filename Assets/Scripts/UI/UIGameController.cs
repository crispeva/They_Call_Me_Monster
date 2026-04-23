using System;
using System.Collections;
using Controllers;
using DG.Tweening;
using Recolectables;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class UIGameController : MonoBehaviour
{
    #region Properties
    [Header("Live")]
    [SerializeField] private Slider _playerHealth; 
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _waveCountText;



    [Header("Event_Shop")]
    [SerializeField] private TextMeshProUGUI _coinText;
    [SerializeField] private TextMeshProUGUI _textcaldero;
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private GameObject _panelshop;
    [Header("Panels")]
    [SerializeField] private GameObject _panelvictory;
    [SerializeField] private GameObject _panelDeath;
    [SerializeField] private GameObject _panelPause;

    [Header("Canvas Groups")]
    public CanvasGroup canvasGroupEndDemo;
    public CanvasGroup canvasGroupDeath;
    float Duration = 2f;


    #endregion

    #region Unity Callbacks
    void Start()
    {
        _playerInventory.OnInventoryUpdated += UpdateCoinUI;
        GameController.Instance.InputController.OnActiveMenu += ShowPausePanel;


    }

    private void UpdateCoinUI()
    {
        _coinText.text = _playerInventory.GetAmount(RecolectableType.Coin).ToString();
    }

    // Update is called once per
    #endregion
    #region Shop State
    internal void OpenShop()
    {
        _panelshop.SetActive(true);
    }

    internal void CloseShop()
    {
        _panelshop.SetActive(false);
    }
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

    #region UIPlayer
    internal void UpdatePlayerHealth(float value)
    {
        _playerHealth.value = value;
    }
    #endregion

    #region Panels
    internal void ShowPanelVictory()
    {
        StartCoroutine(FadeIn(canvasGroupEndDemo, Duration));
    }
    internal void ShowPanelDeath()
    {
        Debug.Log("ShowPanelDeath");
        StartCoroutine(FadeIn(canvasGroupDeath, Duration));
    }
    public void ShowPausePanel()
    {
        if(Time.timeScale > 0)
        {
            _panelPause.SetActive(true);
            Time.timeScale = 0f; // Pausa el juego
        }
        else
        {
            _panelPause.SetActive(false);
            Time.timeScale = 1f; //Continua el juego
        }

    }

    #endregion

    #region Animations
    public IEnumerator FadeIn(CanvasGroup group, float duration)
    {
        float t = 0f;
        group.interactable = true;
        group.blocksRaycasts = true;

        while (t < duration)
        {
            group.alpha = Mathf.Lerp(0f, 1f, t / duration);
            t += Time.deltaTime;
            yield return null;
        }

        group.alpha = 1f;
    }
    public IEnumerator FadeOut(CanvasGroup group, float duration)
    {
        float t = 0f;
        group.interactable = false;
        group.blocksRaycasts = false;

        while (t < duration)
        {
            group.alpha = Mathf.Lerp(1f, 0f, t / duration);
            t += Time.deltaTime;
            yield return null;
        }
        group.alpha = 0f;
    }
    #endregion


}
