using UnityEngine;

public class Attack : MonoBehaviour, IAttack
{
    [SerializeField] private int damage = 40;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyLayers;
    [SerializeField] private float attackCooldown = 1f; // §£§â§Ö§Þ§ñ §Ù§Ñ§Õ§Ö§â§Ø§Ü§Ú §Þ§Ö§Ø§Õ§å §Ñ§ä§Ñ§Ü§Ñ§Þ§Ú
    private float nextAttackTime = 0f;

    void Start()
    {
        attackPoint = transform.parent.Find("PlayerScripts/AttackPoint");
        if (attackPoint == null)
        {
            Debug.LogError("AttackPoint not found in " + transform.parent.name);
        }
    }

    public void HandleAttack()
    {
        if (Time.time >= nextAttackTime && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("attack");
            AttackEnemy();
            nextAttackTime = Time.time + attackCooldown; // §µ§ã§ä§Ñ§ß§Ñ§Ó§Ý§Ú§Ó§Ñ§Ö§Þ §Ó§â§Ö§Þ§ñ §Õ§Ý§ñ §ã§Ý§Ö§Õ§å§ð§ë§Ö§Û §Ñ§ä§Ñ§Ü§Ú
        }
    }

    private void AttackEnemy()
    {
        if (attackPoint == null)
        {
            Debug.LogError("AttackPoint is not assigned.");
            return;
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            // §±§à§Ú§ã§Ü Enemy §Ü§à§Þ§á§à§ß§Ö§ß§ä§Ñ §ß§Ñ §Ü§à§ß§Ü§â§Ö§ä§ß§à§Þ §Õ§à§é§Ö§â§ß§Ö§Þ §à§Ò§ì§Ö§Ü§ä§Ö
            Transform enemyScripts = enemy.transform.Find("EnemyScripts");
            if (enemyScripts != null)
            {
                Enemy enemyComponent = enemyScripts.GetComponent<Enemy>();
                if (enemyComponent != null)
                {
                    enemyComponent.TakeDamage(damage);
                }
                else
                {
                    Debug.LogError("Enemy component not found on " + enemyScripts.name);
                }
            }
            else
            {
                Debug.LogError("EnemyScripts object not found on " + enemy.name);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
