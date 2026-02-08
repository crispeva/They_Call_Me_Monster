using System.Collections;
using System.Collections.Generic;
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
        private int currentWave = 0;
        #endregion

        #region Unity Callbacks
        void Start()
        {
            StartCoroutine(RunWave());
        }

        // Update is called once per frame
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
                    Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
                    GameObject enemyInstance = Instantiate(entry.enemyPrefab, point.position, Quaternion.identity);
                    yield return new WaitForSeconds(wave.timeBetweenSpawns);
                }
            }

            currentWave++;
            StartCoroutine(RunWave());

        }

    }
}
