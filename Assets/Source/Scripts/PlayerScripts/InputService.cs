using System;
using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class InputService : MonoBehaviour
    {
        private const string AxisHorizontalName = "Horizontal";
        private const KeyCode JumpKey = KeyCode.Space;
        private const KeyCode AttackKey = KeyCode.Mouse1;
        private const KeyCode VampyreSkillKey = KeyCode.F;

        public event Action PressedJumpKey;
        public event Action PressedAttackKey;
        public event Action PressedVampyreSkillKey;

        public float Direction { get; private set; }

        private void Update()
        {
            Direction = Input.GetAxis(AxisHorizontalName);
            
            UpdateJumpInput();
            
            UpdateAttackInput();
            
            UpdateVampyreSkillInput();
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

        private void UpdateVampyreSkillInput()
        {
            if (Input.GetKeyDown(VampyreSkillKey))
            {
                PressedVampyreSkillKey?.Invoke();
            }
        }
    }
}