using UnityEngine;

namespace Source.Scripts.LootScripts
{
    public class LootFactory
    {
        private readonly Loot _lootPrefab;

        public LootFactory(Loot lootPrefab)
        {
            _lootPrefab = lootPrefab;
        }

        public Loot Create()
        {
            return Object.Instantiate(_lootPrefab);
        }
    }
}
