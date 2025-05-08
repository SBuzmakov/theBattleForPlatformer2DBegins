using System;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class InputService : MonoBehaviour
    {
        private const string AxisHorizontalName = "Horizontal";
        private const KeyCode JumpKey = KeyCode.Space;

        public event Action PressedJumpKey;

        public float Direction { get; private set; }

        private void Update()
        {
            Direction = Input.GetAxis(AxisHorizontalName);
            
            UpdateJumpInput();
        }

        private void UpdateJumpInput()
        {
            if (Input.GetKeyDown(JumpKey))
                PressedJumpKey?.Invoke();
        }
    }
}