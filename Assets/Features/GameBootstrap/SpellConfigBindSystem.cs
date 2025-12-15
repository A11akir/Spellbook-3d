using System.Collections.Generic;
using Features.Enemy.EnemySpawner;
using Features.Hero.HeroInstance;
using Features.Spells;
using UnityEngine;
using Zenject;

namespace Features.GameBootstrap
{
    public class SpellConfigBindSystem
    {
        private readonly HeroProvider _heroProvider;
        private EnemySpawnerSystem _enemySpawnerSystem;

        public SpellConfigBindSystem(HeroProvider heroProvider, EnemySpawnerSystem enemySpawnerSystem)
        {
            _heroProvider = heroProvider;
            _enemySpawnerSystem = enemySpawnerSystem;
        }
        public void BindConfig()
        {
            _heroProvider.SetConfig();
            _enemySpawnerSystem.SetConfig();
        }
    }
}