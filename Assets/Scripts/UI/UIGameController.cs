using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.UI;

public class UIGameController : MonoBehaviour
{
    #region Properties
    [Header("Live")]
    [SerializeField] private Slider _playerHealth;
    private Slider _enemyHealth;
    float _timeToNextWave=5;
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        // El slider del enemigo debe estar asignado en el Inspector
       
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
        if(waveNumber == 1)
        {
            Debug.Log("Ola actual: " + waveNumber);
        }
        else
        {
                Debug.Log("Oleada " + waveNumber);
        }
    }

    public void UpdateWaveCountdown(int secondsRemaining)
    {
        Debug.Log($"La siguiente oleada comienza en {secondsRemaining} segundos...");
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
