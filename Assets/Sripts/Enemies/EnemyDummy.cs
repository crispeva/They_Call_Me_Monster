using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour,IDamageable
{
    #region Properties
    public int health = 20;
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks

    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy hit! HP: " + health);

        if (health <= 0)
            Destroy(gameObject);
    }
    #endregion

}
