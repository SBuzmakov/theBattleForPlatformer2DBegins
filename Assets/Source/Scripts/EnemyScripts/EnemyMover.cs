using System.Collections.Generic;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.EnemyScripts
{
    public class EnemyMover : MonoBehaviour
    {
        private const float ChangingWaypointDistance = 0.1f;

        [SerializeField] float _speed = 1.0f;
        [SerializeField] private List<Transform> _waypoints;
        [SerializeField] private Transform _modelTransform;

        private int _currentWaypoint;
        private bool _isFacingRight;
        private float _previousPositionX;

        public void MoveBetweenWaypoints(Transform objectTransform)
        {
            if (objectTransform == null)
                return;

            _previousPositionX = objectTransform.position.x;

            if (_waypoints.Count == 0 || _waypoints == null)
                return;

            objectTransform.position =
                Vector2.MoveTowards(objectTransform.position, _waypoints[_currentWaypoint].position,
                    (_speed * Time.deltaTime));

            if (objectTransform.position.IsEnoughClose(_waypoints[_currentWaypoint].position, ChangingWaypointDistance))
                _currentWaypoint = ++_currentWaypoint % _waypoints.Count;

            ChangeFacing(objectTransform);
        }

        public void MoveToTarget(Transform objectTransform, Transform target)
        {
            if (objectTransform == null || target == null)
                return;

            _previousPositionX = objectTransform.position.x;

            objectTransform.position = Vector2.MoveTowards(objectTransform.position, target.position,
                (_speed * Time.deltaTime));

            ChangeFacing(objectTransform);
        }

        private void ChangeFacing(Transform objectTransform)
        {
            if (objectTransform == null)
                return;

            if (objectTransform.position.x < _previousPositionX && _isFacingRight ||
                objectTransform.position.x > _previousPositionX && _isFacingRight == false)
            {
                _modelTransform.FlipByAxisY();

                _isFacingRight = !_isFacingRight;
            }
        }
    }
}