using System.Collections;
using DG.Tweening;
using Features.Hero.HeroInstance;
using Features.Hero.HeroStats.HeroHP;
using UnityEngine;
using Zenject;

namespace Features.Spells.Fireball
{
    public class FireballLogic : MonoBehaviour, ISpellLogic
    {
        [Inject] private DiContainer _container;
        [Inject] private HeroProvider _heroProvider;
        [Inject] private SpellSystem spellSystem;
        private FireballStatsData _stats;
        
        public GameObject _fireballPrefab;
        
        private bool _damageDealt;
        private readonly Collider[] _hits = new Collider[5];
        private int _mask;

        public void SetStats(FireballStatsData stats)
        {
            _stats = stats;
        }

        void Update()
        {
            spellSystem.TickCooldowns(Time.deltaTime);
        }
        
        public void ExecuteSpell()
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

            float maxDistance = _stats.LifeTime * _stats.MissileSpeed;
            Vector3 targetPos = spawnPos + direction * maxDistance;

            fireball.transform.DOMove(targetPos, _stats.LifeTime)
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

            StartCoroutine(TrackRoutine(fireball, radius, _stats.LifeTime));
        }

        private IEnumerator TrackRoutine(GameObject fb, float radius, float lifetime)
        {
            float timer = 0f;

            while (!_damageDealt && timer < lifetime)
            {
                
                if (!fb) yield break;
                
                CheckDamage(fb, radius);
                timer += Time.deltaTime;
                yield return null;
            }
        }

        private void CheckDamage(GameObject fb, float radius)
        {
            if (_damageDealt || !fb) return;

            int count = Physics.OverlapSphereNonAlloc(fb.transform.position, radius, _hits, _mask);

            if (count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    var enemyHp = _hits[i].GetComponent<Health>();

                    if (enemyHp)
                    {
                        enemyHp.TakeDamage(_stats.Damage);
                        _damageDealt = true;
                        Destroy(fb);
                        break;
                    }
                }
            }
        }
    }
}
