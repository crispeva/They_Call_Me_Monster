using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using Enemies;
using UnityEngine;
using UnityEngine.UI;
using static Enemies.WaveData;

namespace Waves
{
    public class WaveManager : MonoBehaviour
    {
        #region Properties

        #endregion

        #region Fields
        [SerializeField] private Enemies.WaveData[] waves;
        [SerializeField] private Transform[] spawnPoints;
        private float  timeBetweenWavesDefault=10f;
        private  float timeBetweenWaves ;
        private int EnemyRemaing = 0;
        public int currentWave = 0;
        private int bossWave = 2;
        public Action <int>OnWaveState;
        public Action<float> OnWaveCountdown;
        public Action<int> OnEnemiesCount;
        public Action OnWavesCompleted;
        //Music
        public Action OnBossWave;
        public Action OnMainWave;
        #endregion

        #region Unity Callbacks
        void Start()
        {
           
            StartCoroutine(RunWave());
        }

        #endregion

        #region Waves
        IEnumerator RunWave()
        {
            if (currentWave >= waves.Length)
                yield break;

            WaveData wave = waves[currentWave];
            //Se incrementa el contador de oleadas
            currentWave++;
            if (currentWave == bossWave)
            {
                OnBossWave?.Invoke(); 
                bossWave += 5; // Incrementar la siguiente oleada de jefe cada 5 oleadas
            }
            else
            {
                OnMainWave?.Invoke(); 
            }
            OnWaveState?.Invoke(currentWave);
            EnemyRemaing = GetTotalEnemiesInWave(wave);
            OnEnemiesCount?.Invoke(EnemyRemaing);
            foreach (var entry in wave.enemies)
            {
                PoolManager.Instance.WarmPool(entry.enemyPrefab, entry.count);
                // Preparar la pool para las recompensas de oro
                PoolManager.Instance.WarmPool(entry.goldRewardPrefab, (entry.count+entry.goldReward));


                for (int i = 0; i < entry.count; i++)
                {
                    Transform point = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
                    PoolManager.Instance.SpawnFromPool(entry.enemyPrefab, point.position, Quaternion.identity); // TODO: Refactorizar
                    yield return new WaitForSeconds(wave.timeBetweenSpawns);
                }

            }
           

        }
        public void OnWaveStarted()
        {
            
            if (currentWave < waves.Length)
            {
                
                StartCoroutine(WaitForNextWave(timeBetweenWavesDefault));
            }
            else
            {
                Debug.Log("¡Has completado todas las oleadas!");
            }

        }

        IEnumerator WaitForNextWave(float timeBetweenWavesDefault)
        {
            this.timeBetweenWaves = timeBetweenWavesDefault;

            while (timeBetweenWaves >= 0)
            {
                OnWaveCountdown?.Invoke(timeBetweenWaves);
                yield return new WaitForSeconds(1f);
                timeBetweenWaves--;
            }
            StartCoroutine(RunWave());
        }
#endregion

        #region Enemies
        public void OnEnemyDisabled()
        {
            EnemyRemaing--;
            
            if (EnemyRemaing <= 0)
            {
                OnWaveStarted();
               OnWavesCompleted?.Invoke(); 
            }
            OnEnemiesCount?.Invoke(EnemyRemaing);  // Actualizar UI después de decrementar
        }

        private int GetTotalEnemiesInWave(WaveData wave)
        {
            int total = 0;
            foreach (var enemyData in wave.enemies)
            {
                total += enemyData.count;
            }
            return total;
        }
        #endregion
    }
}
