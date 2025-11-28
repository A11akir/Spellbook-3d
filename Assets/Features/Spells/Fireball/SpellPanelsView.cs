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

        private void OnEnable()
        {
            _spellSystem.SpellUsed += OnSpellUsed;
        }

        public void OnSpellUsed(int number)
        {
            float cooldown = _spellSystem.GetCooldown(spellIndex);
        }
    }
}