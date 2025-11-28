using Features.Scripts.Input;
using Features.Spells;
using UnityEngine;
using Zenject;

namespace Features.Input.Scripts
{
    public class InputSpells
    {
        [Inject]
        public InputSpells(InputGamePlay inputGamePlay, SpellSystem spellSystem)
        {
            var spellSystem1 = spellSystem;

            inputGamePlay._inputActions.PlayerControl.FirstSpell.performed += ctx =>
            {
                spellSystem1.ExecuteFirstSpell(1);
            };
        }
    }
}