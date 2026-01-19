using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDummy : MonoBehaviour,IDamageable
{
    #region Properties
    [SerializeField] int maxHealth = 30;
    int currentHealth;

    SpriteRenderer sr;
    Color originalColor;
    #endregion

    #region Fields
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }
    #endregion

    #region Public Methods
    public void TakeDamage(int amount)
    {
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
        yield return new WaitForSeconds(0.1f);
        sr.color = originalColor;
    }

    void Die()
    {
        Destroy(gameObject);
    }
    #endregion

}
