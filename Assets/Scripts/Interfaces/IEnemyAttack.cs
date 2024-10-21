using UnityEngine;
public interface IEnemyAttack
{
    void AttackPlayer();
    Transform AttackPoint { get; }
}
