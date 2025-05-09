using System;
using Source.Scripts.LootScripts;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class Collector : MonoBehaviour
    {
        public event Action<HealLoot> PickedUpHealLoot;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out HealLoot healLoot))
            {
                PickedUpHealLoot?.Invoke(healLoot);
                
                healLoot.OnPickUp();
            }
        }
    }
}
