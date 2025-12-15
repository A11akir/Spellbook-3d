using System.Collections;
using DG.Tweening;
using Features.Enemy.NavMesh;
using Features.Hero.HeroInstance;
using UnityEngine;
using Zenject;

namespace Features.Enemy.EnemyAttack
{
    public class RangeEnemyAttack : MonoBehaviour, IEnemyAttack
    {
        [Inject] private DiContainer _container;
        [Inject] private HeroProvider _heroProvider;

        private AgentMoveToPlayer _agentMove;

        [SerializeField] private GameObject _projectilePrefab;

        public int _damage { get; set; }

        public float AttackCooldown = 3f;
        public float ProjectileSpeed = 6f;
        public float LifeTime = 3f;

        private float _cooldown;
        private int _mask;
        private bool _attackIsActive;
        private bool _isAttacking;

        private readonly Collider[] _hits = new Collider[3];

        private void OnEnable()
        {
            _agentMove = GetComponentInChildren<AgentMoveToPlayer>();
            _mask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            if (!_attackIsActive) 
                return;

            if (_cooldown > 0)
            {
                _cooldown -= Time.deltaTime;
                return;
            }

            TryAttack();
        }

        private void TryAttack()
        {
            if (_isAttacking) return;
            if (!_hero_provider_valid()) return;

            StartCoroutine(AttackRoutine());
            _cooldown = AttackCooldown;
        }

        private bool _hero_provider_valid()
        {
            return _heroProvider && _heroProvider.HeroReference;
        }

        private IEnumerator AttackRoutine()
        {
            _isAttacking = true;

            if (_agentMove != null) _agentMove.DisableMovement();

            yield return new WaitForSeconds(0.5f);

            ShootProjectile();

            yield return null;

            if (_agentMove != null) _agentMove.EnableMovement();

            _isAttacking = false;
        }

        private void ShootProjectile()
        {
            if (!_hero_provider_valid()) return;

            var hero = _heroProvider.HeroReference.transform;
            Vector3 direction = (hero.position - transform.position).normalized;

            Vector3 spawnPos = transform.position + direction * 1f + Vector3.up * 1f;

            var projectile = _container.InstantiatePrefab(
                _projectilePrefab,
                spawnPos,
                Quaternion.LookRotation(direction),
                null
            );

            float maxDistance = ProjectileSpeed * LifeTime;
            Vector3 targetPos = spawnPos + direction * maxDistance;

            projectile.transform
                .DOMove(targetPos, LifeTime)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    if (projectile != null)
                        Destroy(projectile);
                });

            StartCoroutine(TrackProjectile(projectile));
        }

        private IEnumerator TrackProjectile(GameObject proj)
        {
            if (!proj) yield break;

            var col = proj.GetComponent<Collider>();
            float radius = Mathf.Max(col.bounds.extents.x, col.bounds.extents.y, col.bounds.extents.z);

            float timer = 0f;

            while (timer < LifeTime)
            {
                if (!proj) yield break;

                CheckHit(proj, radius);

                timer += Time.deltaTime;
                yield return null;
            }
        }

        private void CheckHit(GameObject proj, float radius)
        {
            int count = Physics.OverlapSphereNonAlloc(
                proj.transform.position,
                radius,
                _hits,
                _mask
            );

            if (count > 0)
            {
                var heroHp = _heroProvider.Health;
                if (heroHp != null)
                    heroHp.TakeDamage(_damage);

                Destroy(proj);
            }
        }

        public void DisableAttack() => _attackIsActive = false;
        public void EnableAttack() => _attackIsActive = true;
    }
}
