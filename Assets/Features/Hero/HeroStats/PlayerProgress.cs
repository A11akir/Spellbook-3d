using Features.Hero.HeroMove;
using Features.Hero.HeroStats.HeroHP;

namespace Features.Hero.HeroStats
{
    public class PlayerProgress
    {
        private HeroHp _heroHp;
        private HeroStatsData _heroStatsData;
        private MovementHero _movementHero;

        public PlayerProgress(HeroStatsData heroStatsData, HeroHp heroHp)
        {
            _heroStatsData = heroStatsData;
            _heroHp = heroHp;
        }
        public void SetStatsInMonobeh(MovementHero movementHero)
        {
            _movementHero = movementHero;
        }
        public void NewProgress()
        {
            _heroHp.MaxHp = _heroStatsData.Health;
            _movementHero.speed = _heroStatsData.Speed;
            _heroHp.ResetHp();
        }
    }
}