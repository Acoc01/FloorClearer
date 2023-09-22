using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01: Enemy
{

    public void Start(){
        health = remainingHealth;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
    }

    public override void Die()
    {
        Debug.Log("Base Enemy has died!");
        Destroy(this.gameObject);
    }
}
