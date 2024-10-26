using UnityEngine;

public class EnemyChase : MonoBehaviour, IEnemyChase
{
    [SerializeField] private float chaseSpeed = 4f;
    private EnemyAttack enemyAttack;
    private Enemy enemy;
    private Vector3 originalPosition;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        enemyAttack = enemy.GetComponentInChildren<EnemyAttack>();
        if (enemyAttack == null) Debug.LogError("Missing EnemyAttack component on Enemy");
        originalPosition = transform.parent.position; // §³§à§ç§â§Ñ§ß§ñ§Ö§Þ §ß§Ñ§é§Ñ§Ý§î§ß§å§ð §á§à§Ù§Ú§è§Ú§ð
    }

    public Vector3 OriginalPosition => originalPosition; // §²§Ö§Ñ§Ý§Ú§Ù§å§Ö§Þ §ã§Ó§à§Û§ã§ä§Ó§à OriginalPosition

    public void ChasePlayer(Transform player)
    {
        Vector3 currentPosition = transform.parent.position;
        Vector3 targetPosition = new Vector3(player.position.x, currentPosition.y, currentPosition.z);
        float directionX = targetPosition.x - currentPosition.x;
        enemy.CheckAndFlip(directionX);
        transform.parent.position = Vector2.MoveTowards(currentPosition, targetPosition, chaseSpeed * Time.deltaTime);
    }

    public void ReturnToPatrol()
    {
        Vector3 currentPosition = transform.parent.position;
        float directionX = originalPosition.x - currentPosition.x;
        enemy.CheckAndFlip(directionX);
        transform.parent.position = Vector2.MoveTowards(currentPosition, originalPosition, chaseSpeed * Time.deltaTime);
    }

    public void EnterState(Enemy enemy)
    {
        chaseSpeed = enemy.ChaseSpeed;
    }

    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(transform.parent.position, enemy.Player.position) > enemy.LoseRange)
        {
            ReturnToPatrol();
            if (Vector3.Distance(transform.parent.position, originalPosition) < 0.1f)
            {
                enemy.SetState(new PatrolState());
            }
        }
        else
        {
            ChasePlayer(enemy.Player);
            enemyAttack.AttackPlayer();
        }
    }
}
