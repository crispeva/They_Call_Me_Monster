using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IDamageable
{
    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    [SerializeField]
    private float _maxHealth = 100f;
    public bool _isdeath;
    public float CurrentHealth { get; set; }
    [SerializeField] private float delayDeath = 1f;

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        _isdeath = false;
    }

    private void Start()
    {
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        OnHealthChanged?.Invoke(CurrentHealth); // Notify listeners about health change

        if (CurrentHealth <= 0 && !_isdeath)
        {
            Die();
            _isdeath= true;
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

    private void Die()
    {
        Debug.Log("Invoke Died");
        OnDeath?.Invoke();
        return;
    }
}
