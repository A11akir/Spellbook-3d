using Features.Hero.HeroInstance;
using Features.Spells.Fireball;
using UnityEngine;
using Zenject;

namespace Features.Spells
{
    public class LightningLogic : MonoBehaviour, ISpellLogic
    {
        [Inject] private DiContainer _container;
        [Inject] private HeroProvider _heroProvider;
        [Inject] private SpellSystem spellSystem;
        private LightningStatsData _stats;
        public GameObject lightningPrefab;

        public void ExecuteSpell()
        {
            var hero = _heroProvider.HeroReference.transform;
        }

        public void SetStats(LightningStatsData stats)
        {
            _stats = stats;
        }
    }
    
}