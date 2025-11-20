using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.Enemy.EnemySpawner
{
    public class EnemySpawnerSystem : MonoBehaviour
    {
        [Inject] DiContainer _container;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _enemyCount;

        public void StartSpawn()
        {
            StartCoroutine(SpawnerEnemy());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator SpawnerEnemy()
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                var enemy = _container.InstantiatePrefab(_enemyPrefab);
                enemy.transform.position = _spawnPoint.position;
                yield return new WaitForSeconds(_spawnInterval);
            }

        }
    }
}