using Source.Scripts.HealthScripts;
using Source.Scripts.PlayerScripts;
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

        private Health _health;
        private EnemyAttack _enemyAttack;
        private TargetDetector _targetDetector;

        private void Awake()
        {
            _health = new Health(_maxHealth);
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
            if (_enemyAttack.IsAttacking == false || _targetTransform == null)
                _enemyMover.MoveBetweenWaypoints(transform);

            if (_targetTransform && _targetDetector.IsCloseToAttack())
            {
                _enemyAttack.SwitchOnAttacking();
                _enemyMover.MoveToTarget(transform, _targetTransform);
            }

            if (_health.CurrentHealth <= 0)
                Destroy(gameObject);
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0f)
                return;

            _health.TakeDamage(damage);
        }
    }
}