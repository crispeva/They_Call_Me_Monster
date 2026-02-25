using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Weapons
{
    public class Dagger : Projectile
{
    #region Private Methods
         void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject == owner) return;

            _damageable = other.GetComponent<HealthSystem>();
            if (_damageable != null)
            {
                // Obtener el prefab original desde PooledObject (asignado por PoolManager)
                _damageable.TakeDamage(weaponData.damage);
                Debug.Log("Dagger hit: " + _damageable.CurrentHealth);
                ReturnSelfToPool();
            }
        }
    #endregion

}
}
