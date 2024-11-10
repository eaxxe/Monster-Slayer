using UnityEngine;

public class GeneralEnemyChasePlayer : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _player;
    [SerializeField, Range(2, 13)] private float _speed;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void FollowingPlayer()
    {
        Vector2 direction = new Vector2(_player.position.x - transform.position.x, 0).normalized;
        _rigidbody2D.velocity = new Vector2(direction.x * _speed, _rigidbody2D.velocity.y);
    }
}
