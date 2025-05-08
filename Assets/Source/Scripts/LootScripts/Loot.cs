using System;
using UnityEngine;

namespace Source.Scripts.LootScripts
{
    public class Loot : MonoBehaviour
    {
        public event Action<Loot> Destroyed;
        public event Action<Loot> PickedUp;

        private void OnDestroy()
        {
            Destroyed?.Invoke(this);
        }

        public void OnPickUp()
        {
            PickedUp?.Invoke(this);
        }
    }
}