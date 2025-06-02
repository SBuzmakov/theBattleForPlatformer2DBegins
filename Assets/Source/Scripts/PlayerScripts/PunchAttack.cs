using System;
using Source.Scripts.EnemyScripts;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class PunchAttack : MonoBehaviour
    {
        [SerializeField] private ScanAttackedEnemies _scanAttackedEnemies;
        [SerializeField] private float _punchRadius;
        [SerializeField] private float _punchDamage;
        [SerializeField] private Transform _punchPoint;

        public event Action Punched;
        
        public void HandleAttackEvent()
        {
            Punched?.Invoke();
        }

        public void Perform()
        {
            Enemy enemy = _scanAttackedEnemies.GetClosest(_punchPoint.position, _punchRadius);
            enemy.TakeDamage(_punchDamage);
        }
    }
}