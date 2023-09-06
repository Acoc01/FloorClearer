using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcaneMissiles : Spell
{
    public GameObject missilePrefab;
    public int numProjectiles = 3;
    public float spreadAngle = 3f; // Angle in degrees for spread
    public float missileSpeed = 10f;
    public float missileLifetime = 3f; // Time in seconds to keep missile alive after impact
    public Rigidbody missileRigidbody;

    private float _xSpawnOffset = 0.2f;

    public ArcaneMissiles()
    {
        damage = 1;
        manaCost = 1;
        cooldown = 1;
        healing = 0;
        castSpeed = 1;
        spellSpeed = 1;
        statusEffects.Add("arcane");
    }

    public void Start(){
        missileRigidbody = GetComponent<Rigidbody>();
    }

    public override void CastSpell()
    {
        Debug.Log("pew");
        for (int i = 0; i < numProjectiles; i++)
        {
            // Calculate the direction based on the camera's forward vector with spread
            Quaternion spreadRotation = Quaternion.Euler(0, Random.Range(-spreadAngle, spreadAngle), 0);
            Vector3 missileDirection = spreadRotation * Camera.main.transform.forward;

            Vector3 spawnOffset = new Vector3(0,0,_xSpawnOffset*i);
            GameObject missile = Instantiate(missilePrefab, Camera.main.transform.position + spawnOffset, Quaternion.identity);
            

            // Apply force to the missile in the calculated direction
            missileRigidbody.velocity = missileDirection.normalized * missileSpeed;

            // Set the missile's lifetime before it's destroyed
            Destroy(missile, missileLifetime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("ArcaneMissile") && !collision.gameObject.CompareTag("Player")) // Check if the collided object is not another ArcaneMissile
        {
            missileRigidbody.velocity = Vector3.zero; // Stop the missile's movement upon collision
        }
    }

}
