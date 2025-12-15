using System.Collections.Generic;
using UnityEngine;

namespace Features.Spells
{
    [CreateAssetMenu(
        fileName = "SpellsKitStatsData",
        menuName = "Configs/SpellsKit",
        order = 1)]
    public class SpellsKitData : ScriptableObject, ISpellKitData
    {
        [SerializeField] private List<string> _spellsKit;
        public List<string> SpellsKit
        {
            get => _spellsKit;
            set => _spellsKit = value;
        }
    }
}