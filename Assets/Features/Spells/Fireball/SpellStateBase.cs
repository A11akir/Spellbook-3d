using UnityEngine;

namespace Features.Spells.Fireball
{
    public abstract class SpellStateBase : ScriptableObject
    {
        public Spells Spells;
        public abstract float Cooldown { get; set; }
        public abstract float  MaxCooldown { get; set; }
    }

}