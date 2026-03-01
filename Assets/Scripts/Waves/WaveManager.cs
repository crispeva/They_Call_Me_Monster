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
        public int enemyCount = 0;
        public int currentWave = 0;
        public Action OnWaveState;
        #endregion

        #region Unity Callbacks
        private void Awake()
        {
      
        }
        void Start()
        {
           
            StartCoroutine(RunWave());
        }

        void Update()
        {

        }
        #endregion

        IEnumerator RunWave()
        {
            if (currentWave >= waves.Length)
                yield break;

            WaveData wave = waves[currentWave];
            
            foreach (var entry in wave.enemies)
            {
                for (int i = 0; i < entry.count; i++)
                {
                    Transform point = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
                    GameObject enemyInstance = Instantiate(entry.enemyPrefab, point.position, Quaternion.identity);
                    yield return new WaitForSeconds(wave.timeBetweenSpawns);
                    enemyCount++;
                }
            }

            Debug.Log("Total enemigos " + enemyCount);
            currentWave++;
           
        }
        public void OnWaveStarted()
        {
           // Debug.Log("Wave " + currentWave);
            
            if (currentWave < waves.Length)
            {
                OnWaveState?.Invoke();
                StartCoroutine(WaitForNextWave(5f));
            }
            else
            {
                Debug.Log("¡Has completado todas las oleadas!");
            }

        }

        IEnumerator WaitForNextWave(float waitTime)
        {
            float timeRemaining = waitTime;

            while (timeRemaining >= 0)
            {
                GameController.Instance.UIGameController.UpdateWaveCountdown((int)timeRemaining);
                yield return new WaitForSeconds(1f);
                timeRemaining--;
            }
            StartCoroutine(RunWave());
        }
    }
}
