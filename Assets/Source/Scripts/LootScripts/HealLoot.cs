using UnityEngine;

namespace Source.Scripts.LootScripts
{
    public class HealLoot : MonoBehaviour
    {
        [SerializeField] private float _healthIncreaseValue;

        public float HealthIncreaseValue => _healthIncreaseValue;

        public void OnPickUp()
        {
            Destroy(gameObject);
        }
    }
}