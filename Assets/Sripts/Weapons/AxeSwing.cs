using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AxeSwing : MonoBehaviour
{
    #region Properties
    public float duration = 0.2f;
    public float radius = 1.5f;
    public int damage;

    float timer;
    Transform owner;
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
    public void Init(Transform owner, int damage, float radius)
    {
        this.owner = owner;
        this.damage = damage;
        this.radius = radius;
    }
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // Sigue al jugador
        transform.position = owner.position;
        OnDrawGizmosSelected();

        // Giro visual
        float angle = Mathf.Lerp(-90f, 90f, timer / duration);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (timer >= duration)
            Destroy(gameObject);
    }
    #endregion

    #region Public Methods

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform == owner) return;

        IDamageable dmg = col.GetComponent<IDamageable>();
        if (dmg != null)
        {
            dmg.TakeDamage(damage);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    #endregion

    #region Private Methods
    #endregion

}
