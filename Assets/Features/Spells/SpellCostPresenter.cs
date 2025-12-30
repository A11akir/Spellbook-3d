using Features.ScaleOrderDebuff.Script;
using Features.Spells.Fireball;

namespace Features.Spells
{
    public class SpellCostPresenter
    {
        private readonly SpellSystem _spellSystem;
        private readonly ScaleSpellSystem _scaleSystem;
        private readonly SpellCostView _view;

        public SpellCostPresenter(
            SpellSystem spellSystem,
            ScaleSpellSystem overloadSystem,
            SpellCostView view)
        {
            _spellSystem = spellSystem;
            _scaleSystem = overloadSystem;
            _view = view;

            _spellSystem.SpellUsed += OnSpellUsed;
            _scaleSystem.BalanceChanged += OnBalanceChanged;
        }

        private void OnSpellUsed(int index, SpellStateBase spell)
        {
            _scaleSystem.ApplySpell(spell.TypeMagic, spell.Cost);
        }

        private void OnBalanceChanged(int balance)
        {
            float normalized = balance / 50f;
            _view.SetBalanceNormalized(normalized);
        }
    }
}