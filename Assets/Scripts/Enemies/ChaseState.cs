using UnityEngine;
public class ChaseState : IEnemyState
{
    public void EnterState(Enemy enemy)
    {
        enemy.GetComponentInChildren<IEnemyChase>().EnterState(enemy);
    }

    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Player.position) > enemy.LoseRange)
        {
            enemy.SetState(new PatrolState());
        }
        else
        {
            enemy.GetComponentInChildren<IEnemyChase>().ChasePlayer(enemy.Player);
        }
    }
}
