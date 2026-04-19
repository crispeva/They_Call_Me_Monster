using System;
using Enemies;
using Unity.VisualScripting;
using UnityEngine;
using Waves;
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
    public WaveManager WaveManager => _wavemanager;
    public Shop ShopManager => _shopmanager;
    public InputController InputController => _inputController;

     public Action onWaveStarted;
        #endregion

        #region Fields
    [SerializeField] WeaponController _weaponcontroller;
    [SerializeField] protected HealthSystem _healthsystem;
    [SerializeField] protected UIGameController _uiGameController;
    [SerializeField] protected EnemyController _enemyController;
    [SerializeField] protected WaveManager _wavemanager;
    [SerializeField] protected Shop _shopmanager;
    [SerializeField] protected InputController _inputController;
        #endregion

        #region Unity Callbacks
        void Awake()
        {
            _healthsystem.OnHealthChanged += _uiGameController.UpdatePlayerHealth;
            // Suscribirse al evento de muerte del jugador
            EnemyController.OnEnemyDeath += EnemiesDiabled;
            // Suscribirse al evento de cambio de oleada
            _wavemanager.OnEnemiesCount += UICountEnemies;
            _shopmanager.shopping += ShopState;
            _healthsystem.OnDeath += OnPlayerDeath;
        }

        private void Start()
        {
            _wavemanager.OnWaveState += UIActualWave;
            _wavemanager.OnWaveCountdown += OnCountDownWave;
            _wavemanager.OnVictory += OnVictory;

            // Iniciar la primera oleada
            UIActualWave(1);
        }

        #endregion

        #region Player
        private void OnPlayerDeath()
        {
            _uiGameController.ShowPanelDeath();
        }
        #endregion
        #region Waves Started
        public void ShopState(bool state)
        {
            if (state)
            {
                _uiGameController.OpenShop();
            }
            else {
                _uiGameController.CloseShop();
            }
        }
        #endregion
        #region Waves Started
        public void UIActualWave(int waveNumber)
        {
            _uiGameController.UpdateWaveNumber(waveNumber);

        }
        public void UICountEnemies(int EnemyRemaing)
        {
            _uiGameController.UpdateEnemiesNumber(EnemyRemaing);
        }
        public void OnCountDownWave(float secondsRemaining)
        {
            _uiGameController.UpdateWaveCountdown((int)secondsRemaining);
        }
        public void OnVictory()
        {
            Debug.Log("HAS GANADO!!");
            _uiGameController.ShowPanelVictory();
        }
        public void EnemiesDiabled()
        {
            _wavemanager.OnEnemyDisabled();
        }
        #endregion
    }
}