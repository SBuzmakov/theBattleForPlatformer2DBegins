using System;
using System.Collections.Generic;
using Source.Scripts.AttributesScripts;
using Source.Scripts.PlayerScripts;
using Source.Scripts.Services;
using Source.Scripts.UIScripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Source.Scripts.EnemyScripts
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _damage;
        [SerializeField] private Health _health;
        [SerializeField] private HealthBarSmoothViewer _healthBarSmoothViewer;
        [SerializeField] private TargetDetector _targetDetector;

        private List<IHealthViewable> _healthViewers;
        private HealthViewPresenter _healthViewPresenter;
        private EnemyAttack _enemyAttack;

        public Vector3 Position => transform.position;

        private void Awake()
        {
            _health.Initialize(_maxHealth);
            _healthViewers = new List<IHealthViewable>() { _healthBarSmoothViewer };
            _healthViewPresenter = new HealthViewPresenter(_health, _healthViewers);
            _healthViewPresenter.Initialize();
            _enemyAttack = new EnemyAttack(_damage);
        }

        private void OnEnable()
        {
            _health.CurrentValueIsOver += Destroy;
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
                player.TakeDamage(_enemyAttack.Damage);
        }

        private void Update()
        {
            if (_targetDetector.TargetTransform)
                _enemyMover.MoveToTarget(transform, _targetDetector.TargetTransform);
            else
                _enemyMover.MoveBetweenWaypoints(transform);

            if (_health.CurrentValue <= 0)
                Destroy(gameObject);
        }

        private void OnDisable()
        {
            _health.CurrentValueIsOver -= Destroy;
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