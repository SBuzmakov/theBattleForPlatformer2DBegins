using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.EnemyScripts
{
    public class TargetDetector
    {
        private readonly Transform _transform;
        private readonly float _range;
        private readonly Transform _targetTransform;

        public TargetDetector(Transform transform, Transform targetTransform, float range)
        {
            if (transform == null || targetTransform == null)
                return;

            _transform = transform;
            _range = range;
            _targetTransform = targetTransform;
        }

        public bool IsCloseToAttack()
        {
            return _transform.position.IsEnoughClose(_targetTransform.position, _range);
        }
    }
}