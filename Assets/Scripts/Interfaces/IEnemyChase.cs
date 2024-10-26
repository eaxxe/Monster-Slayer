using UnityEngine;

public interface IEnemyChase
{
    void ChasePlayer(Transform player);
    void ReturnToPatrol(); // §¥§à§Ò§Ñ§Ó§Ý§Ö§ß §Þ§Ö§ä§à§Õ ReturnToPatrol
    void EnterState(Enemy enemy);
    void UpdateState(Enemy enemy);
    Vector3 OriginalPosition { get; } // §¥§à§Ò§Ñ§Ó§Ý§Ö§ß§à §ã§Ó§à§Û§ã§ä§Ó§à OriginalPosition
}
