using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Weapons;
using static UnityEngine.EventSystems.EventTrigger;
namespace Enemies
{
    public class Archer_Action : EnemyController
{
        #region Properties
       [SerializeField] WeaponData Arrow_data;
       int rotationOffset = 90;
        GameObject proj;
        Transform owner;
        Animator _animator;
        #endregion

        #region Fields
        #endregion

        #region Unity Callbacks
        void Awake()
    {
        base.Awake();
            _animator = GetComponent<Animator>();
            owner = transform;
    }
        private void Start()
        {
            if (PoolManager.Instance != null)
            {
                PoolManager.Instance.WarmPool(Arrow_data.weaponPrefab, 5);
            }



        }
        // Update is called once per frame
        void Update()
    {
            base.Update();
            Flip();
     }
        #endregion

        #region Movement
        protected override void EnemyMovement()
        {
            if (Vector2.Distance(transform.position, _target.position) > _enemyData.attackRange)
            {
                // Movimiento hacia el jugador
                base.EnemyMovement();

                //Animations
                Walk_anim(true);

            }
            else
            {
                Walk_anim(false);
            }
        }
        #endregion
        #region Attack
        protected override void Attack()
        {
            if (Vector2.Distance(transform.position, _target.position) > _enemyData.attackRange)
                return;
            
            if (_attackCooldown <= 0)
            {
                Attack_anim();
                //Disparo de flecha isntanciada
                FireArrow();
                _attackCooldown = _enemyData.attackCooldown;
            }
        }
        void FireArrow()
        {
            Vector2 direction = (_target.position - transform.position).normalized;
          float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
           float angle = baseAngle + rotationOffset;
            
            // Capturar el objeto devuelto
            if (PoolManager.Instance != null)
            {
                Debug.Log("Spawneo flecha");
                proj = PoolManager.Instance.SpawnFromPool(Arrow_data.weaponPrefab, owner.position, Quaternion.Euler(0f, 0f, angle));
            }
            else
            {
                Debug.Log("Instancio otro objeto de flecha");
                proj = Object.Instantiate(Arrow_data.weaponPrefab, owner.position, Quaternion.Euler(0f, 0f, angle));
            }

            if (proj != null)
            {
                Projectile Arrow = proj.GetComponent<Projectile>();
                Arrow.GetComponent<Rigidbody2D>().linearVelocity = direction * Arrow_data.projectileSpeed;
                Arrow.Init(gameObject, direction);
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
        void Walk_anim(bool walk)
        {
            _animator.SetBool("Walk", walk);
        }
        void Attack_anim()
        {
            _animator.SetTrigger("Attack");
        }
        #endregion

    }
}
