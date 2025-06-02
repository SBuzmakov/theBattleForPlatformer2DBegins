using System;
using System.Collections;
using Source.Scripts.AttributesScripts;
using Source.Scripts.EnemyScripts;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class VampyrismAttack : MonoBehaviour
    {
        private const float VampyrismDurationTime = 6;
        private const float VampyrismCooldownTime = 4;
        private const float DelayTime = 1;

        [SerializeField] private float _vampyreSkillRadius;
        [SerializeField] private float _vampyrismDamage;
        [SerializeField] private ScanAttackedEnemies _scanAttackedEnemies;

        private Coroutine _coroutine;
        private WaitForSeconds _wait;
        
        private float _decreaseFillAmount;
        private float _cooldownFillAmount;

        public event Action<float> ChangedValue;
        public event Action Enabled;
        public event Action Disabled;

        private void Awake()
        {
            _wait = new WaitForSeconds(DelayTime);

            _decreaseFillAmount = -1 / VampyrismDurationTime;
            _cooldownFillAmount = 1 / VampyrismCooldownTime;
        }

        public void Activate(Health health)
        {
            if (_coroutine == null)
                _coroutine = StartCoroutine(PerformRoutine(health));
        }

        private IEnumerator PerformRoutine(Health health)
        {
            Enabled?.Invoke();

            for (int i = 0; i < VampyrismDurationTime; i++)
            {
                ChangedValue?.Invoke(_decreaseFillAmount);

                Enemy enemy = _scanAttackedEnemies.GetClosest(gameObject.transform.position, _vampyreSkillRadius);

                if (enemy)
                {
                    enemy.TakeDamage(_vampyrismDamage);
                    health.Heal(_vampyrismDamage);
                }

                yield return _wait;
            }

            Disabled?.Invoke();

            for (int i = 0; i < VampyrismCooldownTime; i++)
            {
                ChangedValue?.Invoke(_cooldownFillAmount);

                yield return _wait;
            }

            _coroutine = null;
        }
    }
}