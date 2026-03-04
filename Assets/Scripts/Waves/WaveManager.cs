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
        public int EnemyRemaing = 0;
        private int currentWave = 0;
        public Action <int>OnWaveState;
        public Action<float> OnWaveCountdown;
        public Action<int> OnEnemiesCount;
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
            EnemyRemaing = GetTotalEnemiesInWave(wave);
            OnEnemiesCount?.Invoke(EnemyRemaing);
            foreach (var entry in wave.enemies)
            {
                for (int i = 0; i < entry.count; i++)
                {
                    Transform point = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
                    GameObject enemyInstance = Instantiate(entry.enemyPrefab, point.position, Quaternion.identity);
                    yield return new WaitForSeconds(wave.timeBetweenSpawns);
                }
            }

            currentWave++;
           
        }
        public void OnWaveStarted()
        {
           // Debug.Log("Wave " + currentWave);
            
            if (currentWave < waves.Length)
            {
                OnWaveState?.Invoke(currentWave);
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
            }
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
