using Features.GameplayEffects.Scripts;
using Features.Hero.HeroInstance;
using Features.Spells;
using Features.Spells.Fireball;

namespace Features.GameplayEffects
{
    public class TakeDamageOnCastSpellEffect : IGameplayEffect
    {
        private readonly SpellSystem _spellSystem;
        private readonly HeroProvider _heroProvider;
        
        public TakeDamageOnCastSpellEffect(SpellSystem spellSystem, HeroProvider heroProvider)
        {
            _spellSystem = spellSystem;
            _heroProvider = heroProvider;
        }
        public void ApplyEffect()
        {
            _spellSystem.SpellUsed += TakeDamageOnCastSpell;
        }

        private void TakeDamageOnCastSpell(int moc, SpellStateBase state)
        {
            _heroProvider.Health.TakeDamage(state.Cost);
        }

        public void DisableEffect()
        {
            _spellSystem.SpellUsed -= TakeDamageOnCastSpell;
        }
    }
}