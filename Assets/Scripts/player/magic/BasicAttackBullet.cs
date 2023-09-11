using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackBullet : Projectile
{
    public float lifetime = 30f;

    void Start()
    {
        // Destroy the bullet after a certain amount of time
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != "Player")
            Destroy(gameObject);
    }
}
