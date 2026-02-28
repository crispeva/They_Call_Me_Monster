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
        private bool isWaitingForNextWave = false;
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
            if (isWaitingForNextWave)
                return;
  
           
            if (currentWave < waves.Length)
            {
                Debug.Log("Wave " + currentWave);
                isWaitingForNextWave = true;
                StartCoroutine(WaitForNextWave(5f));
            }
            else
            {
                GameController.Instance.ResetWaveTransition();
            }
            OnWaveState?.Invoke();
        }

        IEnumerator WaitForNextWave(float waitTime)
        {
            float timeRemaining = waitTime;

            while (timeRemaining > 0)
            {
                GameController.Instance.UIGameController.UpdateWaveCountdown((int)timeRemaining);
                yield return new WaitForSeconds(1f);
                timeRemaining--;
            }

            isWaitingForNextWave = false;
            GameController.Instance.ResetWaveTransition(); // Reset aquí
            StartCoroutine(RunWave());
        }
    }
}
