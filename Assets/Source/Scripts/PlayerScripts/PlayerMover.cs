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
            rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }

        public void Move(Rigidbody2D rb, float direction)
        {
            ChangeFacing(rb, direction);
        
            if (_isFacingRight)
                rb.transform.Translate(Vector3.right * (_speed * Time.deltaTime * direction));
            else
                rb.transform.Translate(Vector3.left * (_speed * Time.deltaTime * direction));
        }
        
        private void ChangeFacing(Rigidbody2D rb, float direction)
        {
            if (direction < 0f && _isFacingRight ||
                direction > 0f && _isFacingRight == false)
            {
                rb.transform.FlipByAxisY();
        
                _isFacingRight = !_isFacingRight;
            }
        }
    }
}