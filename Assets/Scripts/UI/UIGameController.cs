using System.Collections;
using System.Collections.Generic;
using Controllers;
using TMPro;
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
    private void Awake()
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
        // Aquí puedes actualizar el texto o cualquier elemento de la UI que muestre el número de la ola actual

            _waveCountText.text = $"wave: {waveNumber}";
        
    }
    public void UpdateEnemiesNumber(int EnemiesNumber)
    {
        // Aquí puedes actualizar el texto o cualquier elemento de la UI que muestre el número de la ola actual


            _waveText.text = $"enemies remain: {EnemiesNumber}";
    }
    public void UpdateWaveCountdown(int secondsRemaining)
    {
        _waveText.text = $"next wave in: {secondsRemaining} seg";
        // Aquí actualizar texto de UI con el tiempo restante
    }
    #endregion

    #region UIPlayer
    internal void UpdatePlayerHealth(float value)
    {
        _playerHealth.value = value;
    }
    #endregion

}
