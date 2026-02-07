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
    [SerializeField] private Slider _enemyHealth;
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

    #region Public Methods

    #endregion

    #region Private Methods
    internal void UpdatePlayerHealth(float value)
    {
        _playerHealth.value = value;
    }
    internal void UpdateEnemyHealth(float value)
    {
        _enemyHealth.value = value;
    }
    #endregion

}
