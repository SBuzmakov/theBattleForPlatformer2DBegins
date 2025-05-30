using System;
using System.Collections;
using System.Linq;
using Source.Scripts.AttributesScripts;
using Source.Scripts.EnemyScripts;
using Source.Scripts.Extensions;
using Source.Scripts.UIScripts;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.PlayerScripts
{
    public class PlayerAttack : MonoBehaviour
    {
        private const int MaxEnemiesValue = 100;
        private const float VampyrismDurationTime = 6;
        private const float VampyrismCooldownTime = 4;
        private const float DelayTime = 1;

        [SerializeField] private float _punchRadius;
        [SerializeField] private float _vampyreSkillRadius;
        [SerializeField] private float _punchDamage;
        [SerializeField] private float _vampyrismDamage;

        public event Action Punched;
        private Coroutine _coroutine;
        private WaitForSeconds _wait;

        public float PunchRadius => _punchRadius;
        public float PunchDamage => _punchDamage;

        private void Awake()
        {
            _wait = new WaitForSeconds(DelayTime);
        }

        [Obsolete("Obsolete")]
        public Enemy GetClosestAttackedEnemy(Vector3 attackPoint, float attackingRadius)
        {
            Collider2D[] colliders = new Collider2D[MaxEnemiesValue];

            int size = Physics2D.OverlapCircleNonAlloc(attackPoint, attackingRadius, colliders);

            Enemy[] enemies = colliders.Take(size).Select(collider2D1 => collider2D1.GetComponent<Enemy>())
                .Where(enemy => enemy != null)
                .ToArray();

            if (enemies.Length == 0)
                return null;

            return enemies.Aggregate((closest, next) =>
                attackPoint.SqrDistance(next.Position) < attackPoint.SqrDistance(closest.Position)
                    ? next
                    : closest);
        }


        public void HandleAttackEvent()
        {
            Punched?.Invoke();
        }

        public void StartVampyrism(VampyreBarViewer vampyreBarViewer, Health health)
        {
            if (_coroutine == null)
                _coroutine = StartCoroutine(StartVampyrismCoroutine(vampyreBarViewer, health));
        }

        private IEnumerator StartVampyrismCoroutine(VampyreBarViewer vampyreBarViewer, Health health )
        {
            vampyreBarViewer.SetAreaEnable();

            float vampyrismDecreaseFillAmount = - 1 / VampyrismDurationTime;
            float vampyrismCooldownFill = 1 / VampyrismCooldownTime;

            for (int i = 0; i < VampyrismDurationTime; i++)
            {
                vampyreBarViewer.ChangeIndicatorFill(vampyrismDecreaseFillAmount);

                Enemy enemy = GetClosestAttackedEnemy(vampyreBarViewer.AreaTransform.position, _vampyreSkillRadius);

                if (enemy)
                {
                    enemy.TakeDamage(_vampyrismDamage);
                    health.Heal(_vampyrismDamage);
                }

                yield return _wait;
            }

            vampyreBarViewer.SetAreaDisable();

            for (int i = 0; i < VampyrismCooldownTime; i++)
            {
                vampyreBarViewer.ChangeIndicatorFill(vampyrismCooldownFill);

                yield return _wait;
            }

            _coroutine = null;
        }
    }
}