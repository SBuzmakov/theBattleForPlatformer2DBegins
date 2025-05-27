using System.Collections.Generic;
using Source.Scripts.AttributesScripts;
using Source.Scripts.EnemyScripts;
using Source.Scripts.LootScripts;
using Source.Scripts.Services;
using Source.Scripts.UIScripts;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Collector _collector;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private FootGroundDetector _footGroundDetector;
        [SerializeField] private InputService _inputService;
        [SerializeField] private PlayerAnimator _playerAnimator;
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private float _maxHealth;
        [SerializeField] private HealthBarSmoothViewer _healthBarSmoothViewer;
        [SerializeField] private Health _health;
        [SerializeField] private Transform _punchPoint;
        
        private HealthViewPresenter _healthViewPresenter;
        private List<IHealthViewable> _healthViewers;

        private void Awake()
        {
            _health.Initialize(_maxHealth);
            _healthViewers = new List<IHealthViewable> {_healthBarSmoothViewer};
            _healthViewPresenter = new HealthViewPresenter(_health, _healthViewers);
            _healthViewPresenter.Initialize();
        }

        private void OnEnable()
        {
            _inputService.PressedAttackKey += Attack;
            _inputService.PressedJumpKey += Jump;
            _playerAttack.Punched += GiveDamage;
            _collector.PickedUpHealLoot += UseHealLoot;
        }

        private void Update()
        {
            if (_health.CurrentHealth <= 0)
                Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnDisable()
        {
            _inputService.PressedJumpKey -= Jump;
            _inputService.PressedAttackKey -= Attack;
            _playerAttack.Punched += GiveDamage;
            _collector.PickedUpHealLoot += UseHealLoot;
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
            Debug.Log($"takeDamage: {damage}, health: {_health.CurrentHealth}/{_health.MaxHealth}");
        }

        private void UseHealLoot(HealLoot healLoot)
        {
            if (healLoot == null)
                return;

            _health.Heal(healLoot.HealthIncreaseValue);
            Debug.Log(
                $"Health increased: +{healLoot.HealthIncreaseValue}. health: {_health.CurrentHealth}/{_health.MaxHealth}");
        }

        private void Jump()
        {
            if (_footGroundDetector.IsGrounded)
                _playerMover.Jump(_rigidbody);
        }

        private void Move()
        {
            _playerMover.Move(transform, _inputService.Direction);

            _playerAnimator.SetWalkSpeed(_inputService.Direction);
        }

        private void Attack()
        {
            _playerAnimator.PlayAttackClip();
        }

        private void GiveDamage()
        {
            List<Enemy> punchedEnemies = _playerAttack.GetPunchedEnemies(_punchPoint.position);

            foreach (Enemy enemy in punchedEnemies)
            {
                enemy.TakeDamage(_playerAttack.PunchDamage);
            }
        }
    }
}