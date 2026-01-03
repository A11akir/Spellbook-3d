using System.Collections.Generic;
using Features.Hero.HeroStats.HeroHP;
using Features.Spells.Fireball;
using Features.Spells.Lightning;
using UnityEngine;
using Zenject;

namespace Features.Spells
{
    public class SpellsMonobehSpawner : MonoBehaviour
    {
        public List<ISpellLogic> _spellLogics = new List<ISpellLogic>();
        [SerializeField] private FireballProjectile prefab;
        [SerializeField] private GameObject _lightningPrefab;
        [Inject] private DiContainer _container;

        public void SpawnSpellSystem(Spells type, SpellStateBase stats)
        {
            switch (type)
            {
                case Spells.Fireball:
                    var fireball = _container.InstantiateComponent<FireballLogic>(gameObject);
                    fireball.InitializePool(prefab, 5);
                    _spellLogics.Add(fireball);
                    fireball.SetStats((FireballStatsData)stats);
                    break;
                case Spells.Lightning:
                    var lightning = _container.InstantiateComponent<LightningLogic>(gameObject);
                    _spellLogics.Add(lightning);
                    lightning.SetStats((LightningStatsData)stats);
                    lightning._lightningPrefab = _lightningPrefab;
                    break;
            }
        }
    }
}