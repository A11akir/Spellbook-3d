using System;
using Features.Enemy.EnemySpawner;
using Features.Spells;
using Features.Spells.Fireball;
using UnityEngine;
using Zenject;

namespace Features.GameplayLogic
{
    public class GameLoopUpdater : MonoBehaviour
    {
        private SpellSystem _spellSystem;
        private SpellPanelsView _spellPanelsView;
        private EnemyProvider _enemyProvider;
        private AutoCastSpellSystem _autoCastSpellSystem;

        [Inject]
        public void Construct(SpellSystem spellSystem, SpellPanelsView spellPanelsView, EnemyProvider enemyProvider,
            AutoCastSpellSystem autoCastSpellSystem)
        {
            _spellSystem = spellSystem;
            _enemyProvider = enemyProvider;
            _spellPanelsView = spellPanelsView;
            _autoCastSpellSystem = autoCastSpellSystem;
        }
        
        private void Update()
        {
            _spellSystem.TickCooldowns(Time.deltaTime);
            _spellPanelsView.TickSkillPanel();
            _autoCastSpellSystem.TickAutocast();

            if (_enemyProvider._canvasMinionSystems != null)
            {
                foreach (var canvas in _enemyProvider._canvasMinionSystems)
                {
                    canvas.UpdateCanvasPos();
                }
            }

        }
    }
}