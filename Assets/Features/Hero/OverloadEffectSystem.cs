using Features.Spells;
using Zenject.SpaceFighter;
using DG.Tweening;
using Features.Enemy.EnemyStats;
using Features.Hero.HeroInstance;
using Features.ScaleOrderDebuff.Script;
using Unity.VisualScripting;

namespace Features.Hero
{
    public class OverloadEffectSystem
    {
        private readonly ScaleSpellSystem _scaleSystem;
        private readonly SilenceHeroSystem _silenceHeroSystem;
        private readonly ChaosHeroSystem _chaosHeroSystem;
        private readonly SpellSystem _spellSystem;
        private readonly ChaosDebuffStatsData _chaosDebuffStatsData;
        private readonly OrderDebuffStatsData _orderDebuffStatsData;
        

        public OverloadEffectSystem(ScaleSpellSystem scaleSystem, SilenceHeroSystem silenceHeroSystem, SpellSystem spellSystem,
            OrderDebuffStatsData orderDebuffStatsData, ChaosDebuffStatsData chaosDebuffStatsData, ChaosHeroSystem chaosHeroSystem)
        {
            _orderDebuffStatsData = orderDebuffStatsData;
            _chaosDebuffStatsData = chaosDebuffStatsData;
            _silenceHeroSystem = silenceHeroSystem;
            _chaosHeroSystem = chaosHeroSystem;
            _scaleSystem = scaleSystem;
            _spellSystem = spellSystem;
            _scaleSystem.OverloadTriggered += OnOverload;
        }

        private void OnOverload(bool isChaos)
        {
            if (isChaos) ApplyChaosOverloadEffect();
            else ApplyOrderOverloadEffect();
        }

        private void ApplyOrderOverloadEffect()
        {
            _silenceHeroSystem.ActivateSilence();
            _spellSystem.HeroSilenced = true;

            DOVirtual.DelayedCall(_orderDebuffStatsData.TimeDebuff, () =>
            {
                _spellSystem.HeroSilenced = false;
                _silenceHeroSystem.DeactivateSilence();
                _scaleSystem.ResetOverload();
            });
        }

        private void ApplyChaosOverloadEffect()
        {
            _chaosHeroSystem.ActivateChaosMode();
            
            DOVirtual.DelayedCall(_chaosDebuffStatsData.TimeDebuff, () =>
            {
                _chaosHeroSystem.DiactivateChaosMode();
                _scaleSystem.ResetOverload();
            });
        }
    }
}

