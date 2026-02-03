using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
namespace Enemies
{
public class EnemyController : MonoBehaviour
{
    #region Properties
    [SerializeField]private EnemyData _enemyData;
    private HealthSystem _playerhealth;
    private Transform _target;
    Vector2 dir;
    #endregion

        #region Fields
        #endregion

        #region Unity Callbacks
    private void Awake()
    {
            _target = GameController.Instance.WeaponController.transform;
            _playerhealth = _target.GetComponent<HealthSystem>();


        }
    protected void Start()
    {
       
    }

    protected void Update()
    {
        EnemyMovement();
        Attack();
     }
        #endregion

        #region Public Methods
        public void Initialize(EnemyData enemyData)
    {
        _enemyData = enemyData;
    }
    #endregion

    #region Private Methods

    private void Attack()
    {
            if (Vector2.Distance(transform.position, _target.position) > _enemyData.attackRange)
                return;
            _playerhealth.TakeDamage(_enemyData.damage);
    }


    protected virtual void EnemyMovement()
    {
        // Implement enemy movement logic here
        dir = (_target.position - transform.position).normalized;
        transform.position += (Vector3)dir * _enemyData.moveSpeed * Time.deltaTime;
    }
    void Die()
    {
        //AudioManager.Instance?.PlaySFX(data.deathSFX);
        Destroy(gameObject);
    }
    #endregion

}
}
