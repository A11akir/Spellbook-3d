using System;
using Features.Enemy.EnemySpawner;
using Features.Hero.HeroInstance;
using Features.MapGenerate;
using UnityEngine;
using Zenject;

namespace Features.GameBootstrap
{
    public class GameBootstrap : MonoBehaviour
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

        private void Awake()
        {
            StartLevel();
        }

        private void StartLevel()
        {
            _spawnMapSystem.SpawnMap();
            _instanceHeroSystem.InstanceHero();
            _enemySpawnerSystem.StartSpawn();
        }
    }
}