/*using Features.Enemy.EnemyAttack;
using Features.Enemy.EnemyHp;
using Features.Enemy.EnemyStats;
using Features.Hero.HeroMove;
using Features.Hero.HeroStats.HeroHP;

namespace Features.Hero.HeroStats
{
    public class PlayerProgress
    {
        private HeroHp _heroHp;
        private EnemyHp _enemyHp;
        private EnemyAttack _enemyAttack;
        private HeroStatsData _heroStatsData;
        private MeleeEnemyStatsData _millyEnemyStatsData;
        private MovementHero _movementHero;

        public PlayerProgress(HeroStatsData heroStatsData, HeroHp heroHp)
        {
            _heroStatsData = heroStatsData;
            _heroHp = heroHp;
        }
        public void SetStatsInMonobeh(MovementHero movementHero, EnemyAttack enemyAttack)
        {
            _movementHero = movementHero;e
        }
        public void NewProgress()
        {
            _enemyHp.MaxHp = _heroStatsData.Health;
            _movementHero.moveSpeed = _heroStatsData.MoveSpeed;
            _heroHp.MaxHp = _millyEnemyStatsData.Health;
            _heroHp.ResetHp();
            _enemyHp.ResetHp();
        }
    }
}*/