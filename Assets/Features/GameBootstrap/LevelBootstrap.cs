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
        private SpellConfigBindSystem _spellConfigBindSystem;
        
        [Inject]
        private void Construct(InstanceHeroSystem instanceHeroSystem, SpawnMapSystem spawnMapSystem, EnemySpawnerSystem enemySpawnerSystem,
            SpellConfigBindSystem spellConfigBindSystem)
        {
            _instanceHeroSystem = instanceHeroSystem;
            _spawnMapSystem = spawnMapSystem;
            _enemySpawnerSystem = enemySpawnerSystem;
            _spellConfigBindSystem = spellConfigBindSystem;
        }

        public void StartLevel()
        {
            _spellConfigBindSystem.BindConfig();
            _spawnMapSystem.SpawnMap();
            _instanceHeroSystem.InstanceHero();
            _enemySpawnerSystem.StartSpawnEnemy();
        }
    }
}