using System.Collections;
using System.Collections.Generic;
using Features.MapGenerate;
using UnityEngine;
using Zenject;

namespace Features.Enemy.EnemySpawner
{
    public class EnemySpawnerSystem : MonoBehaviour
    {
        [Inject] DiContainer _container;

        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _spawnInterval;
        [SerializeField] private float _enemyCount;

        private SpawnMapSystem _spawnMapSystem;

        [Inject]
        private void Construct(SpawnMapSystem spawnMapSystem)
        {
            _spawnMapSystem = spawnMapSystem;
        }
        
        public void StartSpawnEnemy()
        {
            StartCoroutine(SpawnerEnemy());
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator SpawnerEnemy()
        {
            for (int i = 0; i < _enemyCount; i++)
            {
                var enemy = _container.InstantiatePrefab(_enemyPrefab);
                
                enemy.transform.SetParent(transform);
                Vector3 basePoint = _spawnMapSystem.GetRandomPointForSpawnEnemy();

                float height = enemy.transform.localScale.y;
                
                Vector3 spawnPos = new Vector3(basePoint.x, basePoint.y + height, basePoint.z);

                enemy.transform.position = spawnPos;

                yield return new WaitForSeconds(_spawnInterval);
            }
        }
    }
}