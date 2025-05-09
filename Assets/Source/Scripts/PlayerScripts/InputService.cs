using System;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class InputService : MonoBehaviour
    {
        private const string AxisHorizontalName = "Horizontal";
        private const KeyCode JumpKey = KeyCode.Space;
        private const KeyCode AttackKey = KeyCode.Mouse1;

        public event Action PressedJumpKey;
        public event Action PressedAttackKey;

        public float Direction { get; private set; }

        private void Update()
        {
            Direction = Input.GetAxis(AxisHorizontalName);
            
            UpdateJumpInput();
            
            UpdateAttackInput();
        }

        private void UpdateJumpInput()
        {
            if (Input.GetKeyDown(JumpKey))
                PressedJumpKey?.Invoke();
        }

        private void UpdateAttackInput()
        {
            if (Input.GetKeyDown(AttackKey))
                PressedAttackKey?.Invoke();
        }
    }
}