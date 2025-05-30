using Source.Scripts.PlayerScripts;
using UnityEngine;

namespace Source.Scripts.EnemyScripts
{
    public class TargetDetector : MonoBehaviour
    {
        public Transform TargetTransform { get; private set; }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player player))
                SetTargetPosition(player.Position);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Player _))
                SetTargetPosition(null);
        }

        private void SetTargetPosition(Transform targetTransform)
        {
            TargetTransform = targetTransform;
        }
    }
}