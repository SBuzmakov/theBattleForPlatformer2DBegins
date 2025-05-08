using System.Collections.Generic;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.EnemyScripts
{
    public class WaypointsMover : MonoBehaviour
    {
        private const float ChangingWaypointDistance = 0.1f;

        [SerializeField] float _speed = 1.0f;
        [SerializeField] private List<Transform> _waypoints;

        private Transform _target;
        private int _currentWaypoint;
        private bool _isFacingRight;
        private float _previousPositionX;

        private void Start()
        {
            if (_target != null)
                _waypoints.Add(_target);
        }

        private void Update()
        {
            Move();
        }
        
        private void ChangeFacing()
        {
            if (transform.position.x < _previousPositionX && _isFacingRight ||
                transform.position.x > _previousPositionX && _isFacingRight == false)
            {
                transform.FlipByAxisY();

                _isFacingRight = !_isFacingRight;
            }
        }
        
        private void Move()
        {
            _previousPositionX = transform.position.x;
            
            if (_waypoints.Count == 0 || _waypoints == null)
                return;

            transform.position =
                Vector2.MoveTowards(transform.position, _waypoints[_currentWaypoint].position,
                    (_speed * Time.deltaTime));

            if (transform.position.IsEnoughClose(_waypoints[_currentWaypoint].position, ChangingWaypointDistance))
                _currentWaypoint = ++_currentWaypoint % _waypoints.Count;
            
            ChangeFacing();
        }
    }
}
