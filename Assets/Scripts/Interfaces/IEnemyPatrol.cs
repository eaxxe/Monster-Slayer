using UnityEngine;

public interface IEnemyPatrol
{
    void Patrol(Transform[] patrolPoints);
    void SetPatrolSpeed(float speed);
}

