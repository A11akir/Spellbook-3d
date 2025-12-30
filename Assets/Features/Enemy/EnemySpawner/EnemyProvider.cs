using System;
using System.Collections.Generic;
using System.Linq;
using Features.AbstractMinion.Script;
using Features.Enemy.EnemyAttack;
using Features.Enemy.EnemyStats;
using Features.Enemy.NavMesh;
using Features.Hero.HeroInstance;
using Features.Hero.HeroStats.HeroHP;
using UnityEngine;

namespace Features.Enemy.EnemySpawner
{
    public class EnemyProvider
    {
        private readonly MeleeEnemyStatsData _meleeStats;
        private readonly RangeEnemyStatsData _rangeStats;
        private readonly GromillaEnemyStatsData _gromillaStats;
        private readonly HpBarPresenterFactory _presenterFactory;
        private readonly Camera _camera;
        public readonly List<CanvasMinionSystem> _canvasMinionSystems = new();
        private AgentMoveToPlayer _enemyMove;
        private IEnemyAttack _enemyAttack;
        private IHealth _enemyHealth;
        private readonly HeroProvider _heroProvider;
        
        private readonly List<IMinionStatsData> _allStats = new();

        public EnemyProvider(
            MeleeEnemyStatsData meleeStats,
            RangeEnemyStatsData rangeStats,
            GromillaEnemyStatsData gromillaStats,
            HpBarPresenterFactory presenterFactory, HeroProvider heroProvider, Camera camera)
        {
            _meleeStats = meleeStats;
            _rangeStats = rangeStats;
            _gromillaStats = gromillaStats;
            _presenterFactory = presenterFactory;
            _heroProvider = heroProvider;
            _camera = camera;
            
            _allStats.Add(meleeStats);
            _allStats.Add(rangeStats);
            _allStats.Add(gromillaStats);
        }
        
        public void RegisterEnemy(GameObject enemy, GameObject enemyCanvas, EnemyType type)
        {
            var enemyMove = enemy.GetComponentInChildren<AgentMoveToPlayer>();
            var enemyAttack = enemy.GetComponentInChildren<IEnemyAttack>();
            var enemyHealth = enemy.GetComponentInChildren<IHealth>();

            var viewBar = enemyCanvas.GetComponentInChildren<HpBarView>();
            var canvasSystem = enemyCanvas.GetComponent<CanvasMinionSystem>();
    
            canvasSystem.Init(enemy, enemyHealth, _camera);
            enemyMove.Init(_heroProvider);
            enemyAttack.Init(_heroProvider);
            _canvasMinionSystems.Add(canvasSystem);
            _presenterFactory.Create(viewBar, enemyHealth);
            
            IMinionStatsData stats = _allStats.Find(s => s != null && s.EnemyType == type);
            
            SetConfig(enemyMove, enemyAttack, enemyHealth, stats);
            enemyHealth.ResetHp();
        }

        private void SetConfig(AgentMoveToPlayer move, IEnemyAttack attack, IHealth health, IMinionStatsData stats)
        {
            move.agent.speed = stats.MoveSpeed;
            attack._damage = stats.Damage;
            attack._attackSpeed = stats.AttackSpeed;
            health.MaxHp = stats.Health;
        }
    }
}