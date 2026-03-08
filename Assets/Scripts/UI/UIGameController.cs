using System.Collections;
using System.Collections.Generic;
using Controllers;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIGameController : MonoBehaviour
{
    #region Properties
    [Header("Live")]
    [SerializeField] private Slider _playerHealth; 
    [SerializeField] private TextMeshProUGUI _waveText;
    [SerializeField] private TextMeshProUGUI _waveCountText;
    
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
     void Awake()
    {
        
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
