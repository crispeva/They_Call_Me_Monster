using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerProjectile : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    public float speed = 10f;
    public int damage = 10;
    Vector2 direction;
    GameObject owner;
   GameObject originPrefab;
    #endregion

    #region Unity Callbacks
    void Awake()
    {
        // Obtener el prefab original desde PooledObject (asignado por PoolManager)
       
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == owner) return;

        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            var pooled = GetComponent<PooledObject>();
            if (pooled != null)
            {
                originPrefab = pooled.Prefab;
            }
            damageable.TakeDamage(damage);
            PoolManager.Instance?.ReturnToPool(originPrefab, gameObject);
        }
    }
    public void Init(GameObject owner, Vector2 dir)
    {
        this.owner = owner;
        direction = dir.normalized;
    }
    public void ReturnSelfToPool()
    {
        PoolManager.Instance?.ReturnToPool(originPrefab, gameObject);
    }
    #endregion

}
