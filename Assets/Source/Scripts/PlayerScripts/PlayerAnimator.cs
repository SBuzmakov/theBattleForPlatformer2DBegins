using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class PlayerAnimator : MonoBehaviour
    {
        private readonly int _speed = Animator.StringToHash("speed");
        private readonly int _attackTrigger = Animator.StringToHash("AttackTrigger");
        
        [SerializeField] private Animator _animator;

        public void PlayAttackClip()
        {
            _animator.SetTrigger(_attackTrigger);
        }

        public void SetWalkSpeed(float inputServiceDirection)
        {
            _animator.SetFloat(_speed, inputServiceDirection);    
        }
    }
}