using UnityEngine;

namespace Features.Hero.HeroAnimator
{
    public class HeroAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private void OnEnable()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
        }

        public void PlayMove()
        {
            _animator.Play("move");
        }

        public void StopMove()
        {
            _animator.Play("idle");
        }
    }
}