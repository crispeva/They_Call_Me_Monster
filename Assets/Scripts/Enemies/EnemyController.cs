using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.UI;
namespace Enemies
{
public class EnemyController : MonoBehaviour
{
    #region Properties
    [SerializeField]private EnemyData _enemyData;
    protected HealthSystem _playerhealth;
    protected HealthSystem _enemyhealth;
    protected Slider _enemySlyderHealth;
    protected Transform _target;
    private Slider _healthSlider;
        Vector2 dir;
    private float _attackCooldown = 5f;
        #endregion

        #region Fields
        public HealthSystem HealthSystem => _enemyhealth;
        #endregion

        #region Unity Callbacks
        protected void Awake()
    {
            //Para evitar problemas de referencia circular, el enemigo se registra a sí mismo en el GameController durante su Awake.
          

            _target = GameController.Instance.WeaponController.transform;
            _playerhealth = _target.GetComponent<HealthSystem>();
            _enemyhealth = GetComponent<HealthSystem>();
            _enemySlyderHealth= GetComponentInChildren<Slider>();
            GameController.Instance.RegisterEnemy(this);
            _enemyhealth.OnDestroy += Die;

        }
    protected void Start()
    {
    }

    protected void Update()
    {
        EnemyMovement();
        Attack();
        UpdateAttackCooldown();
     }
        #endregion

        #region Public Methods
        public void Initialize(EnemyData enemyData)
    {
        _enemyData = enemyData;
    }
        #endregion

        #region Private Methods

        protected void Attack()
    {
            if (Vector2.Distance(transform.position, _target.position) > _enemyData.attackRange)
                return;

            if (_attackCooldown <= 0)
            {
                _playerhealth.TakeDamage(_enemyData.damage);
                _attackCooldown = _enemyData.attackCooldown; // Ajusta según tu EnemyData
            }
    }

        protected void UpdateAttackCooldown()
    {
            
            if (_attackCooldown > 0)
        {
               // Debug.Log("Attack Cooldown: " + _attackCooldown);
                _attackCooldown -= Time.deltaTime;
        }
    }

    protected virtual void EnemyMovement()
    {
        // Implement enemy movement logic here
        dir = (_target.position - transform.position).normalized;
        transform.position += (Vector3)dir * _enemyData.moveSpeed * Time.deltaTime;
    }
        internal void UpdateEnemyHealth(float value)
        {
            _enemySlyderHealth.value = value;
        }
        protected void Die()
    {
        //AudioManager.Instance?.PlaySFX(data.deathSFX);
        Debug.Log("Enemy died"+gameObject.name);
            Destroy(gameObject);
    }
    #endregion

}
}
