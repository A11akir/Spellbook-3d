using DG.Tweening;
using Features.AbstractMinion.Script;
using UnityEngine;

namespace Features.Spells.Fireball
{
    public class FireballProjectile : MonoBehaviour
    {
        private float _damage;
        private float _lifeTime;
        private Tween _moveTween;

        public void Launch(Vector3 direction, float speed, float lifetime, float damage)
        {
            _damage = damage;
            _lifeTime = lifetime;

            _moveTween?.Kill();

            _moveTween = transform.DOMove(
                    transform.position + direction * (speed * lifetime),
                    lifetime)
                .SetEase(Ease.Linear)
                .OnComplete(Deactivate);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<IDamageable>(out var damageable))
                return;

            damageable.TakeDamage(_damage);
            Deactivate();
        }

        private void Deactivate()
        {
            _moveTween?.Kill();
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            _moveTween?.Kill();
        }
    }
}