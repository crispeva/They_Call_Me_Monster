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

     public Action onWaveStarted;
        #endregion

        #region Fields
    [SerializeField] WeaponController _weaponcontroller;
    [SerializeField] protected HealthSystem _healthsystem;
    [SerializeField] protected UIGameController _uiGameController;
    [SerializeField] protected EnemyController _enemyController;
    [SerializeField] protected WaveManager _wavemanager;
    public bool waveTransitioning = false;
        #endregion

        #region Unity Callbacks
        void Awake()
        {
        }
        private void Start()
        {
            _healthsystem.OnHealthChanged += _uiGameController.UpdatePlayerHealth;
            Enemies.EnemyController.OnEnemyDeath += EnemiesDiabled;
            _wavemanager.OnWaveState += () => UINextWave(_wavemanager.currentWave);
            UINextWave(0);
        }
        private void Update()
        {
            
        }

        #endregion

        #region Player
        private void OnPlayerDeath()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Waves Started
            public void UINextWave(int waveNumber)
            {
                _uiGameController.UpdateWaveNumber(waveNumber+1); 
                
            }

        public void ResetWaveTransition()
        {
            waveTransitioning = false;
        }
        public void EnemiesDiabled()
        {
            _wavemanager.enemyCount--;
            Debug.Log("Enemigos restantes: " + _wavemanager.enemyCount);
            if (_wavemanager.enemyCount == 0)
            {
                _wavemanager.OnWaveStarted();
            }
        }
        #endregion


    }
}