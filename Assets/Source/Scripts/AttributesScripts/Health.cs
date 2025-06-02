using System;
using UnityEngine;

namespace Source.Scripts.AttributesScripts
{
    public class Health : MonoBehaviour
    {
        public event Action CurrentValueChanged;
        public event Action CurrentValueIsOver;

        public float MaxValue { get; private set; }
        public float CurrentValue { get; private set; }
        
        public void Initialize(float maxHealth)
        {
            MaxValue = maxHealth;
            CurrentValue = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (damage < 0f)
                return;

            ChangeCurrentValue(-damage);
        }

        public void Heal(float healValue)
        {
            if (healValue < 0f)
                return;

            ChangeCurrentValue(healValue);
        }
        
        private void ChangeCurrentValue(float delta)
        {
            if (Mathf.Approximately(delta, 0f))
                return;

            CurrentValue = Mathf.Clamp(CurrentValue + delta, 0f, MaxValue);
            
            CurrentValueChanged?.Invoke();
            
            if (CurrentValue <= 0)
                CurrentValueIsOver?.Invoke();
        }
    }
}