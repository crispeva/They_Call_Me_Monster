using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerProjectile : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    //Propiedades del proyectil
    public float speed = 10f;
    public int damage = 10;
    // Dirección del proyectil
    Vector2 direction;
    // Dueño del proyectil para evitar colisiones consigo mismo
    GameObject owner;
    // Prefab original del proyectil
    GameObject originPrefab;

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

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        if(Vector3.Distance(startPos, transform.position)> Max_distance)
        {
            ReturnSelfToPool();
        }
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
            // Obtener el prefab original desde PooledObject (asignado por PoolManager)
            damageable.TakeDamage(damage);
            ReturnSelfToPool();
        }    
    }
    public void Init(GameObject owner, Vector2 dir)
    {
        this.owner = owner;
        direction = dir.normalized;
    }
    //Refactorizar para usar PoolManager
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
