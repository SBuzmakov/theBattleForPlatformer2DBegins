using System.Collections.Generic;
using System.Linq;
using Source.Scripts.EnemyScripts;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class ScanAttackedEnemies : MonoBehaviour
    {
        private const int MaxEnemiesValue = 100;
        
        private Collider2D[] _enemiesColliders;
        private List<Enemy> _enemies;

        private void Awake()
        {
            _enemiesColliders = new Collider2D[MaxEnemiesValue];
            _enemies = new List<Enemy>();
        }
        
        public Enemy GetClosest(Vector3 attackPoint, float attackingRadius)
        {
            int size = Physics2D.OverlapCircleNonAlloc(attackPoint, attackingRadius, _enemiesColliders);

            _enemies = _enemiesColliders.Take(size).Select(collider2D1 => collider2D1.GetComponent<Enemy>())
                .Where(enemy => enemy != null)
                .ToList();

            if (_enemies.Count == 0)
                return null;

            return _enemies.Aggregate((closest, next) =>
                attackPoint.SqrDistance(next.Position) < attackPoint.SqrDistance(closest.Position)
                    ? next
                    : closest);
        }
    }
}