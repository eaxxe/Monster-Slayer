using UnityEngine;
public interface IEnemyChase
{
    void ChasePlayer(Transform player);
    void EnterState(Enemy enemy);
    void UpdateState(Enemy enemy);
}
