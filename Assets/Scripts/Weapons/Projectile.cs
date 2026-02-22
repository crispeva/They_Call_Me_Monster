using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Weapons
{
    public class Projectile : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    //Propiedades del proyectil
    [SerializeField] WeaponData weaponData;
    // Dirección del proyectil
    Vector2 direction;
    // Dueño del proyectil para evitar colisiones consigo mismo
    GameObject owner;
    // Prefab original del proyectil
    GameObject originPrefab;
    HealthSystem _damageable;
    PooledObject pooledObject;
    // Distancia recorrida del proyectil
    Vector3 startPos;
    private float Max_distance = 20f;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        startPos = transform.position;
    }
    private void Start()
    {
    }

    void Update()
    {
        transform.position += (Vector3)direction * weaponData.projectileSpeed * Time.deltaTime;
        if(Vector3.Distance(startPos, transform.position)> Max_distance)
        {
            ReturnSelfToPool();
        }
    }
    #endregion


    #region Private Methods
    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.gameObject == owner) return;

        _damageable = other.GetComponent<HealthSystem>();
        if (_damageable != null)
        {
            // Obtener el prefab original desde PooledObject (asignado por PoolManager)
            _damageable.TakeDamage(weaponData.damage);
            ReturnSelfToPool();
        }    
    }
    public void Init(GameObject owner, Vector2 dir)
    {
        this.owner = owner;
        direction = dir.normalized;
    }
    public void ReturnSelfToPool()
    {
        pooledObject = GetComponent<PooledObject>();
        if (pooledObject != null)
        {
            originPrefab = pooledObject.Prefab;
            PoolManager.Instance?.ReturnToPool(originPrefab, gameObject);
        }
       
    }
    #endregion

}
}
