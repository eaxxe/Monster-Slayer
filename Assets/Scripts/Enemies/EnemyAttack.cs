using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private float attackCooldown = 2f;
    private float lastAttackTime;
    private System.Random _random;
    public event Action<int> OnAttack;
    private PlayerHealth playerHealth;

    private void Start()
    {
        // §µ§Ò§Ö§Õ§Ú§Þ§ã§ñ, §é§ä§à BoxCollider2D §ß§Ñ§ã§ä§â§à§Ö§ß §Ü§Ñ§Ü §ä§â§Ú§Ô§Ô§Ö§â
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            Debug.LogError("Missing BoxCollider2D component on EnemyAttack");
        }
        else
        {
            collider.isTrigger = true;
        }
        _random = new System.Random();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = other.GetComponentInChildren<PlayerHealth>();
            if (playerHealth == null)
            {
                Debug.LogError("Missing PlayerHealth component on Player");
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && playerHealth != null)
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                OnAttack?.Invoke(_random.Next(1, 3));
                AttackPlayer();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth = null;
        }
    }

    public void AttackPlayer()
    {
        playerHealth.TakeDamage(damage);
        lastAttackTime = Time.time;
    }
}
