using UnityEngine;
public class PatrolState : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        enemy.GetComponentInChildren<IEnemyPatrol>().SetPatrolSpeed(enemy.PatrolSpeed);
    }

    public void UpdateState(Enemy enemy)
    {
        Transform[] patrolPoints = enemy.PatrolPoints;
        if (patrolPoints.Length == 0) return;

        Transform targetPatrolPoint = patrolPoints[enemy.GetComponentInChildren<EnemyPatrol>().CurrentPatrolIndex];

        // §²§Ñ§Ù§Ó§à§â§Ñ§é§Ú§Ó§Ñ§Ö§Þ §Ó§â§Ñ§Ô§Ñ §é§Ö§â§Ö§Ù `Enemy`
        float directionX = targetPatrolPoint.position.x - enemy.transform.position.x;
        enemy.CheckAndFlip(directionX);

        enemy.GetComponentInChildren<IEnemyPatrol>().Patrol(patrolPoints);

        if (Vector3.Distance(enemy.transform.position, enemy.Player.position) <= enemy.DetectionRange)
        {
            enemy.SetState(new ChaseState());
        }
    }
}
