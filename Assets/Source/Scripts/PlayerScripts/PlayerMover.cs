using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private Transform _modelTransform;

        private bool _isFacingRight = true;

        public void Jump(Rigidbody2D rb)
        {
            if (rb == null)
                return;

            rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        public void Move(Transform objectTransform, float direction)
        {
            if (objectTransform == null)
                return;

            ChangeFacing(objectTransform, direction);

            objectTransform.transform.Translate(Vector3.right * (_speed * Time.deltaTime * direction));
        }

        private void ChangeFacing(Transform objectTransform, float direction)
        {
            if (objectTransform == null)
                return;

            if (direction < 0f && _isFacingRight ||
                direction > 0f && _isFacingRight == false)
            {
                _modelTransform.FlipByAxisY();

                _isFacingRight = !_isFacingRight;
            }
        }
    }
}