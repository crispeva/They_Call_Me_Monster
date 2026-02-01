using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour,IDamageable
{
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;
    public event Action OnDestroy;

    [SerializeField]
    private float _maxHealth = 100f;
    private bool _isdeath;
    public float CurrentHealth { get; set; }
    [SerializeField] private float delayDeath = 1f;
    //Sprite Renderer for flash effect
    SpriteRenderer _sprite;
    Color originalColor;
    private void Awake()
    {
        CurrentHealth = _maxHealth;
        _isdeath = false;
    }
    private void Start()
    {
        _sprite = this.GetComponent<SpriteRenderer>();
        originalColor = _sprite.color;
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        OnHealthChanged?.Invoke(CurrentHealth); // Notify listeners about health change
        Debug.Log($"Took {damage} damage, current health: {CurrentHealth}");
        StartCoroutine(HitFlash());
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Min(CurrentHealth, _maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth);
    }

    public void SetHealth(float value)
    {
        CurrentHealth = value;
        CurrentHealth = Mathf.Min(CurrentHealth, _maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth);
    }
    IEnumerator HitFlash()
    {
        _sprite.color = Color.red;
        //AudioManager.Instance?.PlaySFX(data.hitSFX);
        yield return new WaitForSeconds(0.1f);
        _sprite.color = originalColor;
    }
    private void Die()
    {

            OnDestroy?.Invoke();
            //return;

    }

}
