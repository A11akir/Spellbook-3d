using UnityEngine;

namespace Features.Hero.HeroAnimator
{
    public class HeroAnimator : MonoBehaviour
    {
        private static readonly int IsMove = Animator.StringToHash("IsMove");
        [SerializeField] private Animator _animator;

        private void OnEnable()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
        }

        public void PlayMove()
        {
            _animator.SetBool(IsMove, true);
        }

        public void StopMove()
        {
            _animator.SetBool(IsMove, false);
        }
    }
}