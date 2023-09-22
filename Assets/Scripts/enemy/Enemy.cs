using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] protected float _health;
    [SerializeField] protected float _remainingHealth;

    public float health
    {
        get => _health;
        set => _health = value;
    }

    public float remainingHealth
    {
        get => _remainingHealth;
        set => _remainingHealth = value;
    }

    public virtual void TakeDamage(float damage)
    {
        _remainingHealth -= damage;

        if (_remainingHealth <= 0)
        {
            Die();
        }
    }

    public abstract void Die();
}
