using UnityEngine;

public class ChaseState : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        enemy.GetComponentInChildren<IEnemyChase>().EnterState(enemy);
    }

    public void UpdateState(Enemy enemy)
    {
        var enemyChase = enemy.GetComponentInChildren<IEnemyChase>();
        if (Vector3.Distance(enemy.transform.position, enemy.Player.position) > enemy.LoseRange)
        {
            enemyChase.ReturnToPatrol();
            if (Vector3.Distance(enemy.transform.position, enemyChase.OriginalPosition) < 0.1f)
            {
                enemy.SetState(new PatrolState());
            }
        }
        else
        {
            enemyChase.ChasePlayer(enemy.Player);
        }
    }
}
