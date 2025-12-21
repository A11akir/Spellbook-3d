using System.Collections.Generic;
using Features.AbstractMinion;
using Features.Enemy.EnemyAttack;
using Features.Enemy.EnemyStats;
using Features.Enemy.NavMesh;
using Features.Hero.HeroStats.HeroHP;
using UnityEngine;

namespace Features.Enemy.EnemySpawner
{
    public class EnemyProvider
    {
        private readonly List<GameObject> _enemies = new();
        private readonly MeleeEnemyStatsData _meleeStats;
        private readonly RangeEnemyStatsData _rangeStats;
        private readonly GromillaEnemyStatsData _gromillaStats;
        private readonly HpBarPresenterFactory _presenterFactory;
        private AgentMoveToPlayer _enemyMove;
        private IEnemyAttack _enemyAttack;
        private IHealth _enemyHealth;
        private readonly Dictionary<EnemyType, IMinionStatsData> _statsByType;
        public EnemyProvider(
            MeleeEnemyStatsData meleeStats,
            RangeEnemyStatsData rangeStats,
            GromillaEnemyStatsData gromillaStats,
            HpBarPresenterFactory presenterFactory)
        {
            _meleeStats = meleeStats;
            _rangeStats = rangeStats;
            _gromillaStats = gromillaStats;
            _presenterFactory = presenterFactory;

            _statsByType = new Dictionary<EnemyType, IMinionStatsData>
            {
                { EnemyType.Melee, meleeStats },
                { EnemyType.Range, rangeStats },
                { EnemyType.Gromilla, gromillaStats }
            };
        }
        
        public void RegisterEnemy(GameObject enemy, GameObject enemyCanvas, EnemyType type)
        {
            _enemies.Add(enemy);

            var enemyMove = enemy.GetComponentInChildren<AgentMoveToPlayer>();
            var enemyAttack = enemy.GetComponentInChildren<IEnemyAttack>();
            var enemyHealth = enemy.GetComponentInChildren<IHealth>();

            var viewBar = enemyCanvas.GetComponentInChildren<HpBarView>();
            var canvasSystem = enemyCanvas.GetComponent<CanvasMinionSystem>();
    
            canvasSystem.Init(enemy, enemyHealth);
            _presenterFactory.Create(viewBar, enemyHealth);
            
            var stats = _statsByType[type];
            
            SetConfig(enemyMove, enemyAttack, enemyHealth, stats);
        }

        private void SetConfig(AgentMoveToPlayer move, IEnemyAttack attack, IHealth health, IMinionStatsData stats)
        {
            move.agent.speed = stats.MoveSpeed;
            attack._damage = stats.Damage;
            attack._attackSpeed = stats.AttackSpeed;
            health.MaxHp = stats.Health;
            health.ResetHp();
        }

    }
}