using System.Collections.Generic;
using Source.Scripts.AttributesScripts;
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
        [SerializeField] private float _maxHealth;
        [SerializeField] private Health _health;
        [SerializeField] private VampyrismAttack _vampyrismAttack;
        [SerializeField] private PunchAttack _punchAttack;
        [SerializeField] private HealthBarSmoothViewer _healthBarSmoothViewer;

        private HealthViewPresenter _healthViewPresenter;
        private List<IHealthViewable> _healthViewers;

        public Transform Position => transform;

        private void Awake()
        {
            _health.Initialize(_maxHealth);
            _healthViewers = new List<IHealthViewable> { _healthBarSmoothViewer };
            _healthViewPresenter = new HealthViewPresenter(_health, _healthViewers);
            _healthViewPresenter.Initialize();
        }

        private void OnEnable()
        {
            _inputService.PressedAttackKey += PlayPunchAttack;
            _inputService.PressedVampyreSkillKey += ActivateVampyreSkill;
            _inputService.PressedJumpKey += Jump;
            _punchAttack.Punched += GivePunchDamage;
            _collector.PickedUpHealLoot += UseHealLoot;
            _health.CurrentValueIsOver += Destroy;
        }

        private void Update()
        {
            if (_health.CurrentValue <= 0)
                Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            _playerMover.Move(transform, _inputService.Direction);

            _playerAnimator.SetWalkSpeed(_inputService.Direction);
        }

        private void OnDisable()
        {
            _inputService.PressedJumpKey -= Jump;
            _inputService.PressedAttackKey -= PlayPunchAttack;
            _inputService.PressedVampyreSkillKey -= ActivateVampyreSkill;
            _punchAttack.Punched -= GivePunchDamage;
            _collector.PickedUpHealLoot -= UseHealLoot;
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

        private void Destroy()
        {
            Destroy(gameObject);
        }

        private void UseHealLoot(HealLoot healLoot)
        {
            if (healLoot == null)
                return;

            _health.Heal(healLoot.HealthIncreaseValue);
        }

        private void Jump()
        {
            if (_footGroundDetector.IsGrounded)
                _playerMover.Jump(_rigidbody);
        }

        private void PlayPunchAttack()
        {
            _playerAnimator.PlayPunchClip();
        }

        private void ActivateVampyreSkill()
        {
            _vampyrismAttack.Activate(_health);
        }

        private void GivePunchDamage()
        {
            _punchAttack.Perform();
        }
    }
}