using Features.Hero.HeroMove;
using Features.Hero.HeroStats.HeroHP;

namespace Features.Hero.HeroStats
{
    public class PlayerProgress
    {
        private HeroHp _heroHp;
        private HeroHP.HeroStats _heroStats;
        private MovementHero _movementHero;
    
        public PlayerProgress(HeroHp heroHp, HeroHP.HeroStats heroStats, MovementHero movementHero)
        {
            _heroHp = heroHp;
            _heroStats = heroStats;
            _movementHero = movementHero;
        }
        public void NewProgress()
        {
            _heroHp.MaxHp = _heroStats.Health;
            _movementHero.speed = _heroStats.Speed;
            _heroHp.ResetHp();
        }
    }
}