using System;
using UnityEngine;

namespace Source.Scripts.AttributesScripts
{
    public class Health : MonoBehaviour
    {
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        public event Action HealthChanged;
        
        public void Initialize(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0f)
                return;
            
            if (CurrentHealth - damage < 0f)
                CurrentHealth = 0f;
            else
                CurrentHealth -= damage;
            
            HealthChanged?.Invoke();
        }

        public void Heal(float healValue)
        {
            if (healValue < 0f)
                return;

            if (CurrentHealth + healValue > MaxHealth)
                CurrentHealth = MaxHealth;
            else
                CurrentHealth += healValue;
            
            HealthChanged?.Invoke();
        }
    }
}