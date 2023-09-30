using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, IHealth
{
    public float health { get; set; }
    public float remainingHealth { get; set; }
    private HealthBar healthBar;

    public void Start()
    {
        healthBar = PlayerUI.Instance.healthBar;
        health = 20;
        remainingHealth = health;
    }

    public void TakeDamage(float damage)
    {
        remainingHealth -= damage;
        healthBar.UpdateHealthBar(remainingHealth, health);
        if (remainingHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {

    }
}
