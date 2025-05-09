using System;
using System.Collections.Generic;
using Source.Scripts.EnemyScripts;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private float _punchRadius;
        [SerializeField] private int _maxPunchedColliders;
        [SerializeField] private float _punchDamage;

        public event Action Punched;
        
        public float PunchDamage => _punchDamage;

        public List<Enemy> GetPunchedEnemies(Vector2 point)
        {
            Collider2D[] results = Physics2D.OverlapCircleAll(point, _punchRadius);

            List<Enemy> punchedEnemies = new List<Enemy>();

            for (int i = 0; i < results.Length; i++)
            {
                if (results[i].TryGetComponent(out Enemy enemy))
                    punchedEnemies.Add(enemy);
            }

            return punchedEnemies;
        }

        public void HandleAttackEvent()
        {
            Punched?.Invoke();
        }
    }
}