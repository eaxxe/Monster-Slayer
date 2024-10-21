using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private int damage = 20;
    [SerializeField] private float attackCooldown = 2f;
    private float lastAttackTime;

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
