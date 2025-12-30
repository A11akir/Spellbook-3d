using System.Collections;
using Features.AbstractMinion;
using Features.Hero.HeroInstance;
using UnityEngine;
using Zenject;
using System.Collections.Generic;
using Features.AbstractMinion.Script;
using Features.Spells.Fireball;

namespace Features.Spells.Lightning
{
    public class LightningLogic : MonoBehaviour, ISpellLogic
    {
        [Inject] private DiContainer _container;
        [Inject] private HeroProvider _heroProvider;
        [Inject] private SpellSystem spellSystem;
        public GameObject _lightningPrefab;
        private LightningStatsData _stats;

        private Transform _heroTransform;

        private readonly Collider[] _hits = new Collider[20];
        private readonly List<IHealth> _targetsHealth = new List<IHealth>(20);
        private readonly List<Transform> _targetsTransforms = new List<Transform>(20);
        private int _enemyMask;

        private void OnEnable()
        {
            _enemyMask = LayerMask.GetMask("Enemy");
        }

        public void SetStats(LightningStatsData stats)
        {
            _stats = stats;
        }

        public void ExecuteSpell()
        {
            _heroTransform = _heroProvider.HeroReference.transform;
            FindEnemies();
            StartCoroutine(CastLightningCoroutine());
        }

        private void FindEnemies()
        {
            _targetsHealth.Clear();
            _targetsTransforms.Clear();

            int count = Physics.OverlapSphereNonAlloc(
                _heroTransform.position,
                _stats.Range,
                _hits,
                _enemyMask
            );

            for (int i = 0; i < count; i++)
            {
                Collider hit = _hits[i];

                if (hit.TryGetComponent<IHealth>(out var health))
                {
                    _targetsHealth.Add(health);
                    _targetsTransforms.Add(hit.transform);
                }
            }
        }
        
        private IEnumerator CastLightningCoroutine()
        {
            int count = _targetsHealth.Count;

            GameObject[] lightnings = new GameObject[count];
            
            for (int i = 0; i < count; i++)
            {
                lightnings[i] = Instantiate(
                    _lightningPrefab,
                    _targetsTransforms[i].position,
                    Quaternion.identity, parent: _targetsTransforms[i].parent
                );
            }

            yield return new WaitForSeconds(0.5f);
            
            for (int i = 0; i < count; i++)
            {
                if (lightnings[i])
                    Destroy(lightnings[i]);
                
                _targetsHealth[i].TakeDamage(_stats.Damage);
            }
        }

    }
}