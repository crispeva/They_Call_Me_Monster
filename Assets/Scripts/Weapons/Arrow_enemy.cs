using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Weapons
{
    public class Arrow_enemy : Projectile
{
    #region Private Methods
    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.tag!="Player" || other.gameObject==owner) return;
           // Debug.Log("Toco" + other);
        _damageable = other.GetComponent<HealthSystem>();
        if (_damageable != null)
        {
            // Obtener el prefab original desde PooledObject (asignado por PoolManager)
            _damageable.TakeDamage(weaponData.damage);
            ReturnSelfToPool();
        }    
    }
    #endregion

}
}
