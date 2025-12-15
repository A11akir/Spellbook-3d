using System.Collections.Generic;
using Features.AbstractMinion;
using Features.Enemy.EnemyAttack;
using Features.Enemy.NavMesh;
using Features.Hero.HeroStats.HeroHP;
using UnityEngine;

namespace Features.Enemy.EnemySpawner
{
    public class EnemyProvider
    {
        private readonly List<GameObject> _enemies = new();
        private readonly HpBarPresenterFactory _presenterFactory;
        private IMinionStatsData _stats;
        private AgentMoveToPlayer _enemyMove;
        private IEnemyAttack _enemyAttack;
        private Health _enemyHealth;

        public EnemyProvider(IMinionStatsData stats, HpBarPresenterFactory presenterFactory, Health enemyHealth, IEnemyAttack enemyAttack, AgentMoveToPlayer enemyMove)
        {
            _stats = stats;
            _presenterFactory = presenterFactory;
            _enemyHealth = enemyHealth;
            _enemyAttack = enemyAttack;
            _enemyMove = enemyMove;
        }
        
        public void RegisterEnemy(GameObject enemy, GameObject enemyCanvas, EnemySpawner.EnemyType  type)
        {
            _enemies.Add(enemy);

            _enemyMove = enemy.GetComponentInChildren<AgentMoveToPlayer>();
            _enemyAttack = enemy.GetComponentInChildren<IEnemyAttack>();
            _enemyHealth = enemy.GetComponentInChildren<Health>();
            
            var viewBar = enemyCanvas.GetComponentInChildren<HpBarView>();
            var canvasSystem = enemyCanvas.GetComponent<CanvasMinionSystem>();
            
            canvasSystem.Init(enemy, _enemyHealth);
            _presenterFactory.Create(viewBar, _enemyHealth);
            
            SetConfig();
        }

        public void SetConfig()
        {
            _enemyMove.agent.speed = _stats.MoveSpeed;
            /*_enemyAttack._damage = _stats.Damage;*/
            _enemyHealth.MaxHp = _stats.Health;
            _enemyHealth.ResetHp();
        }
    }
}