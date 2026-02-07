using System;
using Enemies;
using Unity.VisualScripting;
using UnityEngine;
using Weapons;
namespace Controllers
{
    public class GameController : Singleton<GameController>
    {
    #region Properties
    public WeaponController WeaponController => _weaponcontroller;
    public HealthSystem HealthSystem=> _healthsystem;
    public EnemyController EnemyController=> _enemyController;
    public UIGameController UIGameController=> _uiGameController;

    #endregion

    #region Fields
    [SerializeField] WeaponController _weaponcontroller;
    [SerializeField] protected HealthSystem _healthsystem;
    [SerializeField] protected UIGameController _uiGameController;
   [SerializeField] protected EnemyController _enemyController;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
    }
        private void Start()
        {
            //_healthsystem.OnDeath += OnPlayerDeath;
            //Vida de jugador y enemigo se actualiza en la UI
            _healthsystem.OnHealthChanged += _uiGameController.UpdatePlayerHealth;
            _enemyController.HealthSystem.OnHealthChanged += _uiGameController.UpdateEnemyHealth;
        }

        private void OnPlayerDeath()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        #endregion
    }
}