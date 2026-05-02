using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEditor.EditorTools;
using UnityEngine;

public class Clerigo_Actions : EnemyController
{
    #region Properties

    #endregion

    #region Fields

    #endregion

    #region Unity Callbacks
    void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        base.Start();

    }
    // Update is called once per frame
    void Update()
    {
        base.Update();

    }
    #endregion

    #region Attack
    protected override void Attack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, _enemyData.attackRange);
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
            Attack_anim();
            OnAttackEnemy?.Invoke();
            Debug.Log("Clerigo ataca");
            foreach (var hit in hits)
            {
                EnemyController enemy = hit.GetComponent<EnemyController>();

                if (enemy != null && enemy != this)
                {
                    Debug.Log("Clerigo cura a " + enemy.name);
                    enemy.HealthSystem.SetHealth(_enemyData.damage + enemy.HealthSystem.CurrentHealth);
                }
            }
            _attackCooldown = _enemyData.attackCooldown; // Ajusta según tu EnemyData
        }
    }
    protected override void EnemyMovement()
    {
        if (Vector2.Distance(transform.position, _target.position) <= _enemyData.attackRange)
        {
            Walk_anim(false);
            return;
        }
        else
        {
            // Implement enemy movement logic here
            dir = (_target.position - transform.position).normalized;
            transform.position += (Vector3)dir * _enemyData.moveSpeed * Time.deltaTime;
            Walk_anim(true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _enemyData.attackRange);
    }

    #endregion

    #region Private Methods
    #endregion

}
