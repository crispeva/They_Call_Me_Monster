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
    #endregion

    #region Unity Callbacks

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
            damageable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    public void Init(GameObject owner, Vector2 dir)
    {
        this.owner = owner;
        direction = dir.normalized;
    }
    #endregion

}
