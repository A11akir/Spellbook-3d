using System;
using Features.Enemy.EnemySpawner;
using Features.GoogleSheets;
using Features.Hero.HeroInstance;
using Features.Hero.HeroStats;
using Features.Hero.HeroStats.HeroHP;
using Features.MapGenerate;
using Features.Spells;
using UnityEngine;
using Zenject;

namespace Features.GameBootstrap
{
    public class LevelBootstrap : MonoBehaviour
    {
        private InstanceHeroSystem _instanceHeroSystem;
        private EnemySpawnerSystem _enemySpawnerSystem;
        private SpawnMapSystem _spawnMapSystem;
        private SpellSystem _spellSystem;
        private SpellConfigBindSystem _spellConfigBindSystem;
        
        [Inject]
        private void Construct(InstanceHeroSystem instanceHeroSystem, SpawnMapSystem spawnMapSystem, EnemySpawnerSystem enemySpawnerSystem,
            SpellConfigBindSystem spellConfigBindSystem, SpellSystem spellSystem)
        {
            _instanceHeroSystem = instanceHeroSystem;
            _spawnMapSystem = spawnMapSystem;
            _enemySpawnerSystem = enemySpawnerSystem;
            _spellConfigBindSystem = spellConfigBindSystem;
            _spellSystem = spellSystem;
        }

        public void StartLevel()
        {
            _spawnMapSystem.SpawnMap();
            _instanceHeroSystem.InstanceHero();
            _spellSystem.RegisterSpell();
            _enemySpawnerSystem.StartSpawnEnemy();
            _spellConfigBindSystem.BindConfig();
        }
    }
}