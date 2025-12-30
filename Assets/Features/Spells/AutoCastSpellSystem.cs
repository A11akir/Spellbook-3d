using Features.Spells;

public class AutoCastSpellSystem
{
    private readonly SpellSystem _spellSystem;

    public bool ChaosModeEnabled { get; private set; }

    public AutoCastSpellSystem(SpellSystem spellSystem)
    {
        _spellSystem = spellSystem;
    }

    public void EnableChaosMode() => ChaosModeEnabled = true;
    public void DisableChaosMode() => ChaosModeEnabled = false;

    public void TickAutocast()
    {
        if (ChaosModeEnabled)
        {
            TickChaosAutocast();
        }
        else
        {
            TickSingleAutocast();
        }
    }

    private void TickSingleAutocast()
    {
        if (_spellSystem.CanUseLastUsedSpell())
        {
            _spellSystem.TryExecuteSpell(_spellSystem.LastUsedSpellIndex);
        }
    }

    private void TickChaosAutocast()
    {
        for (int i = 0; i < _spellSystem.SpellsCount; i++)
        {
            if (_spellSystem.CanUseSpell(i))
            {
                _spellSystem.TryExecuteSpell(i);
            }
        }
    }
}