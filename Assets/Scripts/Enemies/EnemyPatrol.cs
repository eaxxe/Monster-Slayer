using UnityEngine;

public class EnemyPatrol : MonoBehaviour, IEnemyPatrol
{
    private int currentPatrolIndex = 0;
    private float patrolSpeed;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public int CurrentPatrolIndex => currentPatrolIndex;

    public void SetPatrolSpeed(float speed)
    {
        patrolSpeed = speed;
    }

    public void Patrol(Transform[] patrolPoints)
    {
        if (patrolPoints.Length == 0) return;

        Transform targetPatrolPoint = patrolPoints[currentPatrolIndex];
        Vector3 currentPosition = transform.parent.position;
        Vector3 targetPosition = targetPatrolPoint.position;

        float directionX = targetPosition.x - currentPosition.x;
        enemy.CheckAndFlip(directionX);

        transform.parent.position = Vector2.MoveTowards(currentPosition, targetPosition, patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(currentPosition, targetPosition) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }
}
