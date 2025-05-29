using System.Collections.Generic;
using Source.Scripts.AttributesScripts;
using Source.Scripts.PlayerScripts;
using Source.Scripts.Services;
using Source.Scripts.UIScripts;
using UnityEngine;

namespace Source.Scripts.EnemyScripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] EnemyMover _enemyMover;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _damage;
        [SerializeField] private float _detectorRange;
        [SerializeField] private Transform _targetTransform;
        [SerializeField] private Health _health;
        [SerializeField] private HealthBarSmoothViewer _healthBarSmoothViewer;
        
        private List<IHealthViewable> _healthViewers;
        private HealthViewPresenter _healthViewPresenter;
        private EnemyAttack _enemyAttack;
        private TargetDetector _targetDetector;

        public Vector3 Position => transform.position;
        
        private void Awake()
        {
            _health.Initialize(_maxHealth);
            _healthViewers = new List<IHealthViewable>() {_healthBarSmoothViewer};
            _healthViewPresenter = new HealthViewPresenter(_health, _healthViewers);
            _healthViewPresenter.Initialize();
            _enemyAttack = new EnemyAttack(_damage);
            _targetDetector = new TargetDetector(transform, _targetTransform, _detectorRange);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
                player.TakeDamage(_enemyAttack.Damage);
        }

        private void Update()
        {
            if (_targetDetector.IsCloseToAttack() == false || _targetTransform == null)
                _enemyMover.MoveBetweenWaypoints(transform);

            if (_targetTransform && _targetDetector.IsCloseToAttack())
            {
                _enemyMover.MoveToTarget(transform, _targetTransform);
            }

            if (_health.CurrentValue <= 0)
                Destroy(gameObject);
        }

        private void OnDestroy()
        {
            _healthViewPresenter.Dispose();
        }
        
        public void TakeDamage(float damage)
        {
            if (damage < 0f)
                return;

            _health.TakeDamage(damage);
        }
    }
}