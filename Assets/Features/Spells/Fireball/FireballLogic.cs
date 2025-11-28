using System.Collections;
using DG.Tweening;
using Features.Enemy.EnemyHp;
using Features.Hero.HeroInstance;
using UnityEngine;
using Zenject;

namespace Features.Spells.Fireball
{
    public class FireballLogic : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        [Inject] private HeroProvider _heroProvider;

        [SerializeField] private GameObject _fireballPrefab;

        [SerializeField] private float _lifeTime = 2f;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _damage = 5f;
        [SerializeField] private float _hitRadius = 1.5f;

        private bool _damageDealt;
        private readonly Collider[] _hits = new Collider[5];
        private int _mask;

        public void ExecuteFireball()
        {
            var hero = _heroProvider.HeroReference.transform;

            _mask = 1 << LayerMask.NameToLayer("Enemy");
            
            Vector3 direction = hero.forward.normalized;

            Vector3 spawnPos = hero.position + direction + Vector3.up * _heroProvider.GetSkinWidth();

            var fireball = _container.InstantiatePrefab(
                _fireballPrefab,
                spawnPos,
                hero.rotation,
                null
            );

            float maxDistance = _speed * _lifeTime;
            Vector3 targetPos = spawnPos + direction * maxDistance;

            fireball.transform.DOMove(targetPos, _lifeTime)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    if (fireball != null) Destroy(fireball);
                });

            TrackFireball(fireball);
        }


        private void TrackFireball(GameObject fireball)
        {
            _damageDealt = false;
            
            var col = fireball.GetComponent<Collider>();
            
            float radius = Mathf.Max(col.bounds.extents.x, col.bounds.extents.y, col.bounds.extents.z);

            StartCoroutine(TrackRoutine(fireball, radius, _lifeTime));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator TrackRoutine(GameObject fb, float radius, float lifetime)
        {
            float timer = 0f;

            while (!_damageDealt && timer < lifetime)
            {
                CheckDamage(fb, radius);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        private void CheckDamage(GameObject fb, float radius)
        {
            if (_damageDealt) return;

            int count = Physics.OverlapSphereNonAlloc(fb.transform.position, radius, _hits, _mask);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var enemyHp = _hits[i].GetComponent<EnemyHp>();

                    if (enemyHp != null)
                    {
                        enemyHp.TakeDamage(_damage);
                        _damageDealt = true;
                        Destroy(fb);
                        break;
                    }
                }
            }
        }
    }
}
