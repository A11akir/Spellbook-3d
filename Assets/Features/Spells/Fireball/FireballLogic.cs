using System.Collections;
using DG.Tweening;
using Features.Hero.HeroInstance;
using Features.Hero.HeroStats.HeroHP;
using Features.PoolObject;
using UnityEngine;
using Zenject;

namespace Features.Spells.Fireball
{
    public class FireballLogic : MonoBehaviour, ISpellLogic, ITargetSpell
    {
        [Inject] private HeroProvider _heroProvider;

        private FireballStatsData _stats;
        private PoolMono<FireballProjectile> _pool;
        private bool _randomTargetMode;

        public void InitializePool(FireballProjectile prefab, int count)
        {
            _pool = new PoolMono<FireballProjectile>(prefab, count, transform)
            {
                autoExpand = true
            };
        }

        public void SetStats(FireballStatsData stats) => _stats = stats;

        public void ChangeSelectTargetMode() => _randomTargetMode = true;
        public void DisableSelectTargetMode() => _randomTargetMode = false;

        public void ExecuteSpell()
        {
            var hero = _heroProvider.HeroReference.transform;
            var direction = GetDirection(hero);

            var projectile = _pool.GetFreeElement();
            projectile.transform.position = GetSpawnPosition(hero, direction);
            projectile.gameObject.SetActive(true);

            projectile.Launch(
                direction,
                _stats.MissileSpeed,
                _stats.LifeTime,
                _stats.Damage
            );
        }

        private Vector3 GetDirection(Transform hero)
        {
            if (!_randomTargetMode)
                return hero.forward;

            var dir = Random.insideUnitSphere;
            dir.y = 0f;
            return dir.normalized;
        }

        private Vector3 GetSpawnPosition(Transform hero, Vector3 dir)
        {
            return hero.position + dir + Vector3.up * _heroProvider.GetSkinWidth();
        }
    }
}
