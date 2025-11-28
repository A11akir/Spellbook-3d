using System;
using Features.Enemy.EnemySpawner;
using Features.Hero.HeroInstance;
using Features.Hero.HeroStats;
using Features.Hero.HeroStats.HeroHP;
using Features.MapGenerate;
using UnityEngine;
using Zenject;

namespace Features.GameBootstrap
{
    public class LevelBootstrap : MonoBehaviour
    {
        private InstanceHeroSystem _instanceHeroSystem;
        private EnemySpawnerSystem _enemySpawnerSystem;
        private SpawnMapSystem _spawnMapSystem;
        private PlayerProgress _playerProgress;
        
        [Inject]
        private void Construct(InstanceHeroSystem instanceHeroSystem, SpawnMapSystem spawnMapSystem, EnemySpawnerSystem enemySpawnerSystem, PlayerProgress playerProgress)
        {
            _instanceHeroSystem = instanceHeroSystem;
            _spawnMapSystem = spawnMapSystem;
            _enemySpawnerSystem = enemySpawnerSystem;
            _playerProgress = playerProgress;
        }

        public void StartLevel()
        {
            _spawnMapSystem.SpawnMap();
            _instanceHeroSystem.InstanceHero();
            _playerProgress.NewProgress();
            _enemySpawnerSystem.StartSpawnEnemy();
        }
    }
}