using Source.Scripts.Scene;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class FootGroundDetector : MonoBehaviour
    {
        private int _groundCollisionsEntered;

        public bool IsGrounded { get; private set; }

        private void Update()
        {
            IsGrounded = _groundCollisionsEntered > 0;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Platform _))
                _groundCollisionsEntered++;
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Platform _))
                _groundCollisionsEntered--;
        }
    }
}