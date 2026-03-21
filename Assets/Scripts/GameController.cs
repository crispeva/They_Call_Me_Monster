using System;
using Enemies;
using Unity.VisualScripting;
using UnityEngine;
using Waves;
using Weapons;
using static UnityEngine.EventSystems.EventTrigger;
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

     public Action onWaveStarted;
        #endregion

        #region Fields
    [SerializeField] WeaponController _weaponcontroller;
    [SerializeField] protected HealthSystem _healthsystem;
    [SerializeField] protected UIGameController _uiGameController;
    [SerializeField] protected EnemyController _enemyController;
    [SerializeField] protected WaveManager _wavemanager;

        // GameObject Coinprefab;
        #endregion

        #region Unity Callbacks
        void Awake()
        {
            _healthsystem.OnHealthChanged += _uiGameController.UpdatePlayerHealth;
            // Suscribirse al evento de muerte del jugador
            EnemyController.OnEnemyDeath += EnemiesDiabled;
            // Suscribirse al evento de cambio de oleada
            _wavemanager.OnEnemiesCount += UICountEnemies;

        }

     

        private void Start()
        {
            _wavemanager.OnWaveState += UIActualWave;
            _wavemanager.OnWaveCountdown += OnCountDownWave;
            
            // Iniciar la primera oleada
            UIActualWave(1);
        }
   

        #endregion

        #region Player
        private void OnPlayerDeath()
        {
            throw new NotImplementedException();
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
        public void EnemiesDiabled()
        {
            _wavemanager.OnEnemyDisabled();
        }
        #endregion


    }
}