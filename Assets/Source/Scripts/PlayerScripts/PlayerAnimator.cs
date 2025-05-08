using UnityEngine;

namespace Source.Scripts.PlayerScripts
{
    public class PlayerAnimator : MonoBehaviour
    {
        private const string WalkClipName = "Walk";
        private const string IdleClipName = "Idle";
        
        private readonly int _walkClipHash = Animator.StringToHash(WalkClipName);
        private readonly int _idleClipHash = Animator.StringToHash(IdleClipName);
        
        [SerializeField] private Animator _animator;

        public void PlayWalkClip()
        {
            _animator.Play(_walkClipHash);
        }

        public void PlayIdleClip()
        {
            _animator.Play(_idleClipHash);
        }
    }
}