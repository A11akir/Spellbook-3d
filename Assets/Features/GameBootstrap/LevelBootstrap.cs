using System;
using Features.Enemy.EnemySpawner;
using Features.GoogleSheets;
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
        
        [Inject]
        private void Construct(InstanceHeroSystem instanceHeroSystem, SpawnMapSystem spawnMapSystem, EnemySpawnerSystem enemySpawnerSystem)
        {
            _instanceHeroSystem = instanceHeroSystem;
            _spawnMapSystem = spawnMapSystem;
            _enemySpawnerSystem = enemySpawnerSystem;
        }

        public void StartLevel()
        {
            _spawnMapSystem.SpawnMap();
            _instanceHeroSystem.InstanceHero();
            _enemySpawnerSystem.StartSpawnEnemy();
        }
    }
}