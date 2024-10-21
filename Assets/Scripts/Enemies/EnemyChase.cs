using UnityEngine;

public class EnemyChase : MonoBehaviour, IEnemyChase
{
    [SerializeField] private float chaseSpeed = 4f;
    private EnemyAttack enemyAttack;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyAttack = enemy.GetComponentInChildren<EnemyAttack>(); // §µ§Ò§Ö§â§Ö§Þ §Ú§ß§ä§Ö§â§æ§Ö§Û§ã §Ú §Ú§ã§á§à§Ý§î§Ù§å§Ö§Þ §Ü§à§ß§Ü§â§Ö§ä§ß§í§Û §ä§Ú§á
        if (enemyAttack == null) Debug.LogError("Missing EnemyAttack component on Enemy");
    }

    public void ChasePlayer(Transform player)
    {
        // §¥§Ó§Ú§Ô§Ñ§Ö§Þ §â§à§Õ§Ú§ä§Ö§Ý§î§ã§Ü§Ú§Û §à§Ò§ì§Ö§Ü§ä §ã §á§à§ã§ä§à§ñ§ß§ß§à§Û §ã§Ü§à§â§à§ã§ä§î§ð
        Vector3 currentPosition = transform.parent.position;
        Vector3 targetPosition = new Vector3(player.position.x, currentPosition.y, currentPosition.z);

        float directionX = targetPosition.x - currentPosition.x;
        enemy.CheckAndFlip(directionX);

        transform.parent.position = Vector2.MoveTowards(currentPosition, targetPosition, chaseSpeed * Time.deltaTime);
    }

    public void EnterState(Enemy enemy)
    {
        chaseSpeed = enemy.ChaseSpeed;
    }

    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(transform.parent.position, enemy.Player.position) > enemy.LoseRange)
        {
            enemy.SetState(new PatrolState());
        }
        else
        {
            ChasePlayer(enemy.Player);
            enemyAttack.AttackPlayer();
        }
    }
}
