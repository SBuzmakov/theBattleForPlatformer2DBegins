using System;
using System.Collections;
using System.Linq;
using Source.Scripts.EnemyScripts;
using Source.Scripts.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.PlayerScripts
{
    public class PlayerAttack : MonoBehaviour
    {
        private const float VampyrismDurationTime = 6;
        private const float VampyrismCooldownTime = 4;
        private const float DelayTime = 1;

        [SerializeField] private float _punchRadius;
        [SerializeField] private float _vampyreSkillRadius;
        [SerializeField] private float _punchDamage;
        [SerializeField] private float _vampyrismDamage;
        [SerializeField] private SpriteRenderer _vampyrismSprite;
        [SerializeField] private Image _vampyrismIndicator;

        public event Action Punched;
        private Coroutine _coroutine;
        private WaitForSeconds _wait;

        public float PunchRadius => _punchRadius;
        public float PunchDamage => _punchDamage;

        private void Awake()
        {
            _wait = new WaitForSeconds(DelayTime);

            _vampyrismSprite.gameObject.SetActive(false);
        }

        public Enemy GetClosestAttackedEnemy(Vector3 attackPoint, float attackingRadius)
        {
            Collider2D[] results = Physics2D.OverlapCircleAll(attackPoint, attackingRadius);

            var enemies = results.Select(collider2D1 => collider2D1.GetComponent<Enemy>()).Where(enemy => enemy != null)
                .ToArray();

            if (enemies.Length == 0)
                return null;

            return enemies.Aggregate((closest, next) =>
                attackPoint.SqrDistance(next.Position) < attackPoint.SqrDistance(closest.Position)
                    ? next
                    : closest);
            ;
        }

        public void HandleAttackEvent()
        {
            Punched?.Invoke();
        }

        public void StartVampyrism()
        {
            if (_coroutine == null)
                _coroutine = StartCoroutine(VampyrismCountUp());
        }

        private IEnumerator VampyrismCountUp()
        {
            _vampyrismSprite.gameObject.SetActive(true);
            
            float vampyrismDecreaseFillAmount = 1 / VampyrismDurationTime;
            float vampyrismCooldownFill = 1 / VampyrismCooldownTime;
            
            for (int i = 0; i < VampyrismDurationTime; i++)
            {
                _vampyrismIndicator.fillAmount -= vampyrismDecreaseFillAmount;

                Enemy enemy = GetClosestAttackedEnemy(_vampyrismSprite.transform.position, _vampyreSkillRadius);

                if (enemy)
                    enemy.TakeDamage(_vampyrismDamage);

                yield return _wait;
            }

            _vampyrismSprite.gameObject.SetActive(false);

            for (int i = 0; i < VampyrismCooldownTime; i++)
            {
                _vampyrismIndicator.fillAmount += vampyrismCooldownFill;

                yield return _wait;
            }

            _coroutine = null;
        }
    }
}