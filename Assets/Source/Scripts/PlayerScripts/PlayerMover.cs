using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;

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

            if (_isFacingRight)
                objectTransform.transform.Translate(Vector3.right * (_speed * Time.deltaTime * direction));
            else
                objectTransform.transform.Translate(Vector3.left * (_speed * Time.deltaTime * direction));
        }

        private void ChangeFacing(Transform objectTransform, float direction)
        {
            if (objectTransform == null)
                return;
            
            if (direction < 0f && _isFacingRight ||
                direction > 0f && _isFacingRight == false)
            {
                objectTransform.transform.FlipByAxisY();

                _isFacingRight = !_isFacingRight;
            }
        }
    }
}