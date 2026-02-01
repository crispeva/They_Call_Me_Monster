using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Enemies/Enemy Data")]
public class EnemyData : ScriptableObject
{
    #region Properties
    [Header("Identity")]
    public string enemyName;
    public int count;
    public GameObject enemyPrefab;

    [Header("Stats")]
    public float maxHealth;
    public float moveSpeed;
    public float damage;

    [Header("Combat")]
    public float attackRate;
    public float attackRange;

    [Header("Rewards")]
    public int expReward;
    public int goldReward;

   // [Header("Audio")]
   // public AudioClip hitSFX;
   // public AudioClip deathSFX;
    #endregion


}
