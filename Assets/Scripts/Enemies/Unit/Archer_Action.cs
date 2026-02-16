using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
namespace Enemies
{
    public class Archer_Action : EnemyController
{
        #region Properties
        [SerializeField] GameObject _arrow;
        private GameObject Arrow_instance;
        private float arrowSpeed=5f;
        Animator _animator;
        #endregion

        #region Fields
        #endregion

        #region Unity Callbacks
        void Awake()
    {
        base.Awake();
            _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
            base.Update();
            Flip();
     }
        #endregion

        #region Public Methods
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
        protected override void Attack()
        {
            if (Vector2.Distance(transform.position, _target.position) > _enemyData.attackRange)
                return;
            Debug.Log("Ataco");
            
            if (_attackCooldown <= 0)
            {
                Attack_anim();
                //Disparo de flecha isntanciada
                 Arrow_instance = Instantiate(_arrow, transform.position, Quaternion.identity);
                Vector2 direction = (_target.position - transform.position).normalized;
                Arrow_instance.GetComponent<Rigidbody2D>().linearVelocity = direction * arrowSpeed;

               // _playerhealth.TakeDamage(_enemyData.damage);
                _attackCooldown = _enemyData.attackCooldown;
            }
        }
        #endregion
        #region Private Methods
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

        #endregion

        #region Animations
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
