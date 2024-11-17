using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class EnemyState : MonoBehaviour
{
    [SerializeField] private Transform _player;
    Rigidbody2D _rigidbody;

    // Patrol
    private GeneralEnemyPatrol _generalEnemyPatrol;
    private GeneralEnemyChasePlayer _chasePlayer;
    private float _timeToNextPointChange = 15f;
    private bool _isWaitingForPointChange = false;

    // Chase Player
    private RaycastHit2D _hit;
    [SerializeField, Range(10, 20)] private float _maxDistanceRange;
    [SerializeField, Range(2, 5)] private float _minDistanceRange;
    [SerializeField] private LayerMask _layerMask;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _generalEnemyPatrol = GetComponent<GeneralEnemyPatrol>();
        _chasePlayer = GetComponent<GeneralEnemyChasePlayer>();
    }

    private void Update()
    {
        // _timeToNextPointChange = GetRandomTimeForChangedTargetPoint();
        // if (!_isWaitingForPointChange) StartCoroutine(ChangedSelectedPoint());
        //if(_rigidbody.velocity.x != )
        float distanceToPlayer;
        if (CheckChaseCondition())
        {
             _chasePlayer.FollowingPlayer();
        }
        else
        {
            _generalEnemyPatrol.FollowToSelectedPoint();
        }
        Vector3 direction = _rigidbody.velocity.normalized;
        transform.localScale = new Vector3(Mathf.Sign(direction.x), 1, 1);
    }

    private IEnumerator ChangedSelectedPoint()
    {
        _isWaitingForPointChange = true;
        yield return new WaitForSeconds(_timeToNextPointChange);
        StartCoroutine(StopMovement());
        Debug.Log("Point is changed");
        _generalEnemyPatrol.UpdateTargetPoint();
        _isWaitingForPointChange = false;
    }

    private IEnumerator StopMovement()
    {
        _generalEnemyPatrol.UpdateSpeedToNull(true);
        yield return new WaitForSeconds(4);
        _generalEnemyPatrol.UpdateSpeedToNull(false);
    }

    private int GetRandomTimeForChangedTargetPoint()
    {
        int randomValue = Random.Range(1, 101);
        if (randomValue <= 35) return 5;
        else if (randomValue <= 80) return 10;
        else return 17;
    }

    private bool CheckChaseCondition()
    {
        Vector3 playerPosition = new Vector2(_player.position.x, _player.position.y + 0.4f);
        Vector2 direction = playerPosition - transform.position;
        float distance = direction.magnitude;
        direction.Normalize();

        _hit = Physics2D.Raycast(transform.position, direction, distance, _layerMask);
        Debug.DrawRay(transform.position, direction * distance);
        //Debug.Log(_hit.collider.gameObject.name);

        if (distance < _maxDistanceRange /*&& distance > _minDistanceRange*/ && _hit.collider.gameObject.name == "Player")
        {
            return true;
        }
        return false;
    }
}
