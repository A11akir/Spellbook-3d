using System;
using System.Collections.Generic;
using Features.Hero.HeroInstance;
using Features.Spells.Fireball;
using UnityEngine;
using Zenject;

namespace Features.Spells
{
    public class SpellSystem
    {
        private FireballStatsData _fireballStatsData;
        private LightningStatsData _lightningStatsData;
        private List<SpellStateBase> _spellStates = new List<SpellStateBase>();
        private SpellsKitData _spellsKitData;

        private HeroProvider _heroProvider;
        public event Action<int, SpellStateBase> SpellUsed;

        private List<ISpellLogic> _spellLogics;

        public SpellSystem(HeroProvider heroProvider, FireballStatsData fireballStatsData, SpellsKitData spellsKitData, LightningStatsData lightningStatsData)
        {
            _heroProvider = heroProvider;
            _fireballStatsData = fireballStatsData;
            _spellsKitData = spellsKitData;
            _lightningStatsData = lightningStatsData;
        }

        public void RegisterSpell()
        {
            for (int i = 0; i < _spellsKitData.SpellsKit.Count; i++)
            {
                switch (_spellsKitData.SpellsKit[i])
                {
                    case "Fireball":
                        _heroProvider._spellsMonobehSpawner.SpawnSpellSystem(Spells.Fireball, _fireballStatsData);
                        _spellStates.Add(_fireballStatsData);
                        _fireballStatsData.Cooldown = 0;
                        break;
                    case "Lightning":
                        _heroProvider._spellsMonobehSpawner.SpawnSpellSystem(Spells.Lightning, _lightningStatsData);
                        _spellStates.Add(_lightningStatsData);
                        _lightningStatsData.Cooldown = 0;
                        break;
                    
                    
                }
            }
            Debug.Log($"[SpellSystem] SpellStates count = {_spellStates.Count}");

            for (int i = 0; i < _spellStates.Count; i++)
            {
                var spell = _spellStates[i];

                Debug.Log(
                    $"[{i}] " +
                    $"Type: {spell.GetType().Name}, " +
                    $"Cooldown: {spell.Cooldown}, "
                );
            }
            
        }

        public void ExecuteSpell(int number)
        {
            if (_spellStates[number].Cooldown > 0) return;

            _spellStates[number].Cooldown = _spellStates[number].MaxCooldown;
            _heroProvider._spellsMonobehSpawner
                ._spellLogics[number]
                .ExecuteSpell();

            _spellStates[number].Cooldown = _spellStates[number].MaxCooldown;
            SpellUsed?.Invoke(number, _spellStates[number]);
        }
        
        public void TickCooldowns(float deltaTime)
        {
            foreach (var state in _spellStates)
            {
                if (state.Cooldown > 0f)
                    state.Cooldown -= deltaTime;
            }
        }

    }
}