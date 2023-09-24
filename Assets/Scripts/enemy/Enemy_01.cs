using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_01 : Enemy
{
    public float followRange = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 3f;
    public float attackCooldown = 2f;

    private Transform player;
    private float timeSinceLastAttack = 0f; 

    public EnemyAttack attackScript;
    private Animator animator;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        remainingHealth = health;
    }

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && timeSinceLastAttack >= attackCooldown && !IsAttacking())
        {
            Attack();
            animator.SetBool("isWalking", false);
        }
        else if (distanceToPlayer <= followRange && distanceToPlayer > attackRange && !IsAttacking())
        {
            FollowPlayer();
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
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

    void Attack()
    {
        timeSinceLastAttack = 0f;
        Debug.Log("Attacking the player...");
        animator.SetTrigger("attack1");

        EnemyAttack attackScript = GetComponent<EnemyAttack>();
        if (attackScript)
        {
            attackScript.ActivateAttack(2f); 
        }
    }

    void FollowPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    bool IsAttacking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("attack1");
    }

    public void EndAttack()
    {
        if (attackScript)
        {
            attackScript.DeactivateAttack();
        }
    }
}
