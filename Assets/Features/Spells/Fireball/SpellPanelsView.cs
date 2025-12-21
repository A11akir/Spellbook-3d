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

        private void OnEnable() => _spellSystem.SpellUsed += OnSpellUsed;

        private void OnDisable() => _spellSystem.SpellUsed -= OnSpellUsed;

        private void OnSpellUsed(int number, SpellStateBase state)
        {
            _skillPanels[number].UseSpell(state);
        }

    }
}