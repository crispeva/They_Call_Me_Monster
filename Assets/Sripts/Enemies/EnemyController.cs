using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    #region Properties
    [SerializeField] private EnemyData data;
    private HealthSystem _enemyHealth;
    private Transform target;
    private float currentHealth;

    SpriteRenderer sr;
    Color originalColor;
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        _enemyHealth.SetHealth(data.maxHealth);
        target = GameController.Instance.WeaponController.transform;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }
    private void Start()
    {
        
    }
    #endregion

    #region Public Methods
    public void TakeDamage(int amount)
    {
        _enemyHealth.TakeDamage(0);
        currentHealth -= amount;
        StartCoroutine(HitFlash());

        if (currentHealth <= 0)
            Die();
    }
    #endregion

    #region Private Methods

    IEnumerator HitFlash()
    {
        sr.color = Color.red;
        AudioManager.Instance?.PlaySFX(data.hitSFX);
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColor;
    }
    protected void EnemyMovement()
    {
        // Implement enemy movement logic here
        Vector2 dir = (target.position - transform.position).normalized;
        transform.position += (Vector3)dir * data.moveSpeed * Time.deltaTime;
    }
    void Die()
    {
        AudioManager.Instance?.PlaySFX(data.deathSFX);
        Destroy(gameObject);
    }
    #endregion

}
