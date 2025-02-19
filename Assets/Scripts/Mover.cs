using System;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform[] _wayPoints;

    [SerializeField] private float _speed = 5;

    private Transform _currentWayPoint;

    private int _currentWayPointIndex = 0;

    private float _minDistanceToWayPoint = 0.1f;

    private void Update()
    {
        transform.LookAt(_currentWayPoint);
        ChooseWayPoint();
        Move();
    }

    private void Move()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _currentWayPoint.position, _speed * Time.deltaTime);
    }

    private void ChooseWayPoint()
    {
        if (_wayPoints.Length < 1)
        {
            throw new InvalidOperationException();
        }
        else
        {
            if (transform.position.IsEnoughClose(_wayPoints[_currentWayPointIndex].position, _minDistanceToWayPoint))
            {
                _currentWayPointIndex = ++_currentWayPointIndex % _wayPoints.Length;
            }

            _currentWayPoint = _wayPoints[_currentWayPointIndex];
        }
    }
}