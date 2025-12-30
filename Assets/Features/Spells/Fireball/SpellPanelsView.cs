using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

namespace Features.Spells.Fireball
{
    public class SpellPanelsView : MonoBehaviour
    {
        [SerializeField] private List<SkillPanelView> _skillPanels;
        [Inject] SpellSystem _spellSystem;
        
        public int lastUsedSpellIndex { get; set; } = -1;

        private void OnEnable() => _spellSystem.SpellUsed += OnSpellUsed;

        private void OnDisable() => _spellSystem.SpellUsed -= OnSpellUsed;

        private void OnSpellUsed(int number, SpellStateBase state)
        {
            if (lastUsedSpellIndex != -1) 
                _skillPanels[lastUsedSpellIndex].HideAutocastImage();
            _skillPanels[number].UseSpell(state);
            lastUsedSpellIndex = number;
        }

        public void SilenceViewActivate()
        {
            foreach (var panel in _skillPanels)
            {
                panel.ActivateSilenceView();
            }
        }

        public void TickSkillPanel()
        {
            foreach (var panel in _skillPanels)
            {
                panel.TickSkillPanel();
            }
        }

        public void SilenceViewInactivate()
        {
            foreach (var panel in _skillPanels)
            {
                panel.InactivateSilenceView();
            }
        }

        public void DisactiveAllAutocastImage()
        {
            foreach (var panel in _skillPanels)
            {
                panel.HideAutocastImage();
                panel.DisableAutocastImage();
            }
        }

        public void ActiveAllAutocastImage()
        {
            foreach (var panel in _skillPanels)
            {
                panel.EnableAutocastImage();
            }
        }
    }
}