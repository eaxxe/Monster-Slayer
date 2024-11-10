using UnityEngine;

public class FallDamage : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private PlayerHealth _playerHealth;
    private float _minFallDamage = 20;
    private float _minMiddleFallDamage = 35;
    private float _middleFallDamage = 50;
    private float _middleMaxFallDamage = 70;
    private float _maxFallDamage = 100;

    private float _minTimeFall = 1.2f;
    private float _minMiddleTimeFall = 1.4f;
    private float _middleTimeFall = 1.8f;
    private float _middleMaxTimeFall = 2.3f;
    private float _maxTimeFall = 2.8f;

    private float timeOfFalling = 0;

    private bool _isFall = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    public void DamageFall()
    {
        Vector3 direction = _rigidbody.velocity.normalized; 
        if(direction.y < -0.1)
        {
            timeOfFalling += Time.deltaTime;
            _isFall = true;
        }

        if (_isFall && !(direction.y < -0.1))
        {
            float damage;
            switch (timeOfFalling)
            {
                case float t when t > _minTimeFall && t < _minMiddleTimeFall:
                    damage = _minFallDamage;
                    break;
                case float t when t > _minMiddleTimeFall && t < _middleTimeFall:
                    damage = _minMiddleFallDamage;
                    break;
                case float t when t > _middleTimeFall && t < _middleMaxTimeFall:
                    damage = _middleFallDamage;
                    break;
                case float t when t > _middleMaxTimeFall && t < _maxTimeFall:
                    damage = _middleMaxFallDamage;
                    break;
                case float t when t > _maxTimeFall:
                    damage = _maxFallDamage;
                    break;
                default:
                    damage = 0;
                    break;
            }
            Debug.Log("Damage of fall: " + damage);
            Debug.Log("Time of fall: " + timeOfFalling);
            _isFall = false;
            timeOfFalling = 0;
            _playerHealth.TakeDamage((int)damage);
        }

    }

}
