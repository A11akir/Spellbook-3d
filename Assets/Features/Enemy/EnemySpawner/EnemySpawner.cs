using System;
using System.Collections;
using System.Collections.Generic;
using Features.Hero.HeroStats.HeroHP;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Features.Enemy.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] DiContainer _container;
        [Inject] private EnemyProvider _enemyProvider;
        public void StartSpawnerEnemy(int enemyCont, float spawnInterval, GameObject enemy)
        {
            StartCoroutine(SpawnerEnemy(enemyCont, spawnInterval, enemy));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator SpawnerEnemy(int enemyCount, float spawnInterval, GameObject enemyPrefab)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                var enemy = _container.InstantiatePrefab(enemyPrefab, transform);
                _enemyProvider.SetDependies(enemy);

                float height = enemy.transform.localScale.y;
                Vector3 spawnPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + height, enemy.transform.position.z);
                enemy.transform.position = spawnPos;

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}