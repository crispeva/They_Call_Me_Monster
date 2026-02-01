using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Properties
    //Mover EnemyData a un controlador
    public EnemyData EnemyData => _enemyData;
    private EnemyData _enemyData;
    private Transform _target;
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
    private void Awake()
    {

        
    }
    protected void Start()
    {
        _target = GameController.Instance.WeaponController.transform;
    }
    #endregion

    #region Public Methods
    public void Initialize(EnemyData enemyData)
    {
        _enemyData = enemyData;
    }
    #endregion

    #region Private Methods


    protected virtual void EnemyMovement()
    {
        // Implement enemy movement logic here
        Vector2 dir = (_target.position - transform.position).normalized;
        transform.position += (Vector3)dir * _enemyData.moveSpeed * Time.deltaTime;
    }
    void Die()
    {
        //AudioManager.Instance?.PlaySFX(data.deathSFX);
        Destroy(gameObject);
    }
    #endregion

}
