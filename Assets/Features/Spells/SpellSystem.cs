using System;
using System.Collections.Generic;
using Features.Hero;
using Features.Hero.HeroInstance;
using Features.Spells.Fireball;
using UnityEngine;

namespace Features.Spells
{
    public class SpellSystem
    {
        private FireballStatsData _fireballStatsData;
        private LightningStatsData _lightningStatsData;
        private List<SpellStateBase> _spellStates = new List<SpellStateBase>();
        private SpellsKitData _spellsKitData;
        private SilenceHeroSystem _silenceHeroSystem;

        public int LastUsedSpellIndex = -1;
        public List<int> LastUsedSpells = new List<int>();
        
        private HeroProvider _heroProvider;
        
        public event Action<int, SpellStateBase> SpellUsed;
        public bool HeroSilenced { get; set; }
        public int SpellsCount => _spellStates.Count;

        private List<ISpellLogic> _spellLogics;
        private readonly List<ITargetSpell> _targetSpells = new();

        public SpellSystem(HeroProvider heroProvider, FireballStatsData fireballStatsData, SpellsKitData spellsKitData, LightningStatsData lightningStatsData, SilenceHeroSystem silenceHeroSystem)
        {
            _heroProvider = heroProvider;
            _fireballStatsData = fireballStatsData;
            _spellsKitData = spellsKitData;
            _lightningStatsData = lightningStatsData;
            _silenceHeroSystem = silenceHeroSystem;
        }


        public void RegisterSpell()
        {
            for (int i = 0; i < _spellsKitData.SpellsKit.Count; i++)
            {
                switch (_spellsKitData.SpellsKit[i])
                {
                    case "Fireball":
                        _heroProvider._spellsMonobehSpawner
                            .SpawnSpellSystem(Spells.Fireball, _fireballStatsData);

                        _spellStates.Add(_fireballStatsData);
                        _fireballStatsData.Cooldown = 0;
                        break;

                    case "Lightning":
                        _heroProvider._spellsMonobehSpawner
                            .SpawnSpellSystem(Spells.Lightning, _lightningStatsData);

                        _spellStates.Add(_lightningStatsData);
                        _lightningStatsData.Cooldown = 0;
                        break;
                }
            }

            foreach (var spellLogic in _heroProvider._spellsMonobehSpawner._spellLogics)
            {
                if (spellLogic is ITargetSpell targetSpell)
                    _targetSpells.Add(targetSpell);
            }
        }

        public void TryExecuteSpell(int number)
        {
            if (_spellStates[number].Cooldown > 0) return;

            if (HeroSilenced)
            {
                _silenceHeroSystem.SielenceSpellFeedback();
                return;
            }
            
            _spellStates[number].Cooldown = _spellStates[number].MaxCooldown;

            LastUsedSpellIndex = number;
            LastUsedSpells.Add(LastUsedSpellIndex);
            
            _heroProvider._spellsMonobehSpawner._spellLogics[number].ExecuteSpell();
  
            SpellUsed?.Invoke(number, _spellStates[number]);
            
            //TODO: Сделать класс-систему невозможности каста заклинаний и уже оттуда выдавать фидбек а тут дергать ивент
        }
        public bool CanUseLastUsedSpell()
        {
            if (LastUsedSpellIndex < 0) return false;

            if (HeroSilenced) return false;

            return _spellStates[LastUsedSpellIndex].Cooldown <= 0f;
        }
        public bool CanUseSpell(int index)
        {
            if (index < 0 || index >= _spellStates.Count)
                return false;

            if (HeroSilenced) return false;

            return _spellStates[index].Cooldown <= 0f;
        }
        public void TickCooldowns(float deltaTime)
        {
            foreach (var state in _spellStates)
            {
                if (state.Cooldown > 0f)
                    state.Cooldown -= deltaTime;
            }
        }
        public void EnableChaosMode()
        {
            foreach (var spell in _targetSpells)
                spell.ChangeSelectTargetMode();
        }
        public void DisableChaosMode()
        {
            foreach (var spell in _targetSpells)
                spell.DisableSelectTargetMode();
        }
    }
}