using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{


    #region Properties
    [CreateAssetMenu(menuName = "Waves/Wave Data")]
    public class WaveData : ScriptableObject
    {
        public EnemyData[] enemies;
        public float timeBetweenSpawns;    
    }
}
#endregion




