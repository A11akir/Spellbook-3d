using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Features.Enemy.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] DiContainer _container;
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

                float height = enemy.transform.localScale.y;
                Vector3 spawnPos = new Vector3(enemy.transform.position.x, enemy.transform.position.y + height, enemy.transform.position.z);
                enemy.transform.position = spawnPos;

                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}