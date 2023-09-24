using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour, IHealth
{
    public float health { get; set; }
    public float remainingHealth { get; set; }

    public void Start()
    {
        remainingHealth = health;
    }

    public void TakeDamage(float damage)
    {
        remainingHealth -= damage;

        if (remainingHealth <= 0)
        {
            Die();
        }
    }
    public void Die()
    {

    }
}
