using System;
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
    [SerializeField] protected EnemyData _enemyData;
    protected HealthSystem _playerhealth;
    protected HealthSystem _enemyhealth;
    protected Slider _enemySlyderHealth;
    protected Transform _target;
        Animator _animator;
      protected  Vector2 dir;
     protected private float _attackCooldown ;
        //Referencia al objeto de la pool para devolverlo al morir
        PooledObject pooledObject;
        GameObject originPrefab;
        public static Action OnEnemyDeath;
        public static Action OnAttackEnemy;

        #endregion

        #region Fields
        public HealthSystem HealthSystem => _enemyhealth;
        #endregion

        #region Unity Callbacks
        protected void Awake()
    {
            //Para evitar problemas de referencia circular, el enemigo se registra a sí mismo en el GameController durante su Awake.

            _animator = GetComponent<Animator>();
            _target = GameController.Instance.WeaponController.transform;
            //Health Systems
            _playerhealth = _target.GetComponent<HealthSystem>();
            _enemyhealth = GetComponent<HealthSystem>();
            //UI
            _enemySlyderHealth = GetComponentInChildren<Slider>();
            _enemyhealth.OnHealthChanged += UpdateEnemyHealth;
            _enemyhealth.OnDeath += Die;

        }
        protected private void Start()
        {
           
            _attackCooldown = _enemyData.attackCooldown;
        }
        protected void FixedUpdate()
        {
            EnemyMovement();
        }
        protected void Update()
    {
        Flip();
        Attack();
       
     }
        #endregion

        #region Initialize
        public void Initialize(EnemyData enemyData)
    {
            Debug.Log("INICIALIZANDO");
            _enemyData = enemyData;
            _enemySlyderHealth.maxValue = enemyData.maxHealth;
            _enemyhealth.SetHealth(enemyData.maxHealth);
            _enemyhealth._isdeath = false;

        }
        #endregion

        #region Movement
        protected virtual void EnemyMovement()
        {
            // Implement enemy movement logic here
            dir = (_target.position - transform.position).normalized;
            transform.position += (Vector3)dir * _enemyData.moveSpeed * Time.deltaTime;
            Walk_anim(true);
        }

        #endregion

        #region Attack
        protected virtual void Attack()
    {

            if (Vector2.Distance(transform.position, _target.position) > _enemyData.attackRange)
            {
                _attackCooldown = _enemyData.attackCooldown;
                return;
            }
            else
            {
                UpdateAttackCooldown();
            }
               
            if (_attackCooldown <= 0)
            {
                _playerhealth.TakeDamage(_enemyData.damage);
                _attackCooldown = _enemyData.attackCooldown; // Ajusta según tu EnemyData
                Attack_anim();
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

        #endregion

        #region Life
        //Actualiza el slider de salud del enemigo en la UI (POSIBLE CAMBIO)
        internal void UpdateEnemyHealth(float value)
        {
            _enemySlyderHealth.value = value;
        }
        protected void Die()
        {
            //AudioManager.Instance?.PlaySFX(data.deathSFX);
            pooledObject = GetComponent<PooledObject>();
            if (pooledObject != null)
            {
                originPrefab = pooledObject.Prefab;
                Initialize(_enemyData);
                PoolManager.Instance?.ReturnToPool(originPrefab, gameObject);
                DropRewards();


            }
            OnEnemyDeath?.Invoke();
        }
        #endregion
        #region Rewards
        void DropRewards()
        {
            // Implement logic to drop rewards (e.g., gold, items) upon enemy death
            for (int i = 0; i < _enemyData.goldReward; i++)
            {
                PoolManager.Instance.SpawnFromPool(_enemyData.goldRewardPrefab, transform.position, Quaternion.identity);
            }
                
        }
        #endregion

        #region Animations
        void Flip()
        {
            if (_target.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        protected void Walk_anim(bool walk)
        {
            _animator.SetBool("Walk", walk);
        }
       protected void Attack_anim()
        {
            _animator.SetTrigger("Attack");
        }
        #endregion
    }
}
