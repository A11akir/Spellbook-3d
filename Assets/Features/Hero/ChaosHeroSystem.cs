using Features.GameplayEffects;
using Features.Spells;
using Features.Spells.Fireball;

namespace Features.Hero
{
    public class ChaosHeroSystem
    {
        private readonly AutoCastSpellSystem _autoCastSpellSystem;
        private readonly SpellSystem _spellSystem;
        private readonly ChaosVisualEffectSystem _chaosVisualEffectSystem;
        private readonly SpellPanelsView _spellsPanelView;
        private readonly TakeDamageOnCastSpellEffect _takeDamageOnCastSpellEffect;

        public ChaosHeroSystem(AutoCastSpellSystem autoCastSpellSystem, SpellSystem spellSystem, ChaosVisualEffectSystem chaosVisualEffectSystem, SpellPanelsView spellsPanelView, TakeDamageOnCastSpellEffect takeDamageOnCastSpellEffect)
        {
            _autoCastSpellSystem = autoCastSpellSystem;
            _spellSystem = spellSystem;
            _chaosVisualEffectSystem = chaosVisualEffectSystem;
            _spellsPanelView = spellsPanelView;
            _takeDamageOnCastSpellEffect = takeDamageOnCastSpellEffect;
        }

        public void ActivateChaosMode()
        {
            _chaosVisualEffectSystem.EnableChaosMode();
            _autoCastSpellSystem.EnableChaosMode();
            _spellsPanelView.DisactiveAllAutocastImage();
            _spellSystem.EnableChaosMode();
            _takeDamageOnCastSpellEffect.ApplyEffect();
        }

        public void DiactivateChaosMode()
        {
            _autoCastSpellSystem.DisableChaosMode();
            _spellSystem.DisableChaosMode();
            _spellsPanelView.ActiveAllAutocastImage();
            _chaosVisualEffectSystem.DisableChaosMode();
            _takeDamageOnCastSpellEffect.DisableEffect();
        }
    }
}