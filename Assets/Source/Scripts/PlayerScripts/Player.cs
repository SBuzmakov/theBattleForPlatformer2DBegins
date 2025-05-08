using System;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Collector _collector;
        [SerializeField] private PlayerMover _playerMover;
        [SerializeField] private FootGroundDetector _footGroundDetector;
        [SerializeField] private InputService _inputService;
        [SerializeField] private PlayerAnimator _playerAnimator;

        private void OnEnable()
        {
            _inputService.PressedJumpKey += Jump;
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void OnDisable()
        {
            _inputService.PressedJumpKey -= Jump;
        }

        private void Jump()
        {
            if (_footGroundDetector.IsGrounded)
                _playerMover.Jump(_rigidbody);
        }

        private void Move()
        {
            _playerMover.Move(_rigidbody, _inputService.Direction);
            
            if (_inputService.Direction == 0f)
                _playerAnimator.PlayIdleClip();
            else
                _playerAnimator.PlayWalkClip();
        }
    }
}