using Features.Enemy.EnemyHp;
using Features.Hero.HeroMove;
using Features.Hero.HeroStats.HeroHP;

namespace Features.Hero.HeroStats
{
    public class PlayerProgress
    {
        private HeroHp _heroHp;
        private EnemyHp _enemyHp;
        private HeroStatsData _heroStatsData;
        private MillyEnemyStatsData _millyEnemyStatsData;
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
            
            _enemyHp.MaxHp = _heroStatsData.Health;
            _movementHero.speed = _heroStatsData.Speed;
            _heroHp.MaxHp = _millyEnemyStatsData.Health;
            _heroHp.ResetHp();
            _enemyHp.ResetHp();
        }
    }
}