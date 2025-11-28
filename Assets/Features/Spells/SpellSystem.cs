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
        public event Action<int> SpellUsed;
        [Inject] private FireballLogic _fireballLogic;
        public void ExecuteFirstSpell(int number)
        {
            _fireballLogic.ExecuteFireball();
            SpellUsed?.Invoke(number);
        }

        public float GetCooldown(int spellIndex)
        {
            throw new NotImplementedException();
        }
    }
}