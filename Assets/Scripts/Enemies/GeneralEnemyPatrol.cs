using System.Collections.Generic;
using UnityEngine;

public class GeneralEnemyPatrol : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Transform _targetPoint;
    private List<Transform> listOfPoints;
    private int _pointCount = 1;
    private float _tolerance = 0.4f;

    [SerializeField, Range(1, 10)] private float _speed = 5.0f;
    [SerializeField] private Transform _firstPoint;
    [SerializeField] private Transform _lastPoint;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        listOfPoints = new List<Transform> { _firstPoint, _lastPoint };
    }

    public void FollowToSelectedPoint()
    {
        _targetPoint = listOfPoints[_pointCount];
        if (Vector2.Distance(transform.position, _targetPoint.position) < _tolerance)
        {
            _pointCount = (_pointCount + 1) % listOfPoints.Count;
        }

        Vector2 direction = new Vector2(_targetPoint.position.x - transform.position.x, 0).normalized;
        _rigidbody2D.velocity = new Vector2(direction.x * _speed, _rigidbody2D.velocity.y);
    }

    public void UpdateTargetPoint()
    {
        _pointCount = (_pointCount + 1) % listOfPoints.Count;
    }

    public void UpdateSpeedToNull(bool isChanged)
    {
        if (isChanged) _speed = 0;
        else _speed = 4.0f;
    }
}
