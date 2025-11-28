using System.Collections;
using System.Collections.Generic;
using Features.MapGenerate;
using UnityEngine;
using Zenject;

namespace Features.Enemy.EnemySpawner
{
    public class EnemySpawnerSystem : MonoBehaviour
    {
        [Inject] private DiContainer _container;

        [SerializeField] private GameObject _spawnerPrefab;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private List<float> _spawnIntervals;
        [SerializeField] private List<int> _enemyCounts;
        [SerializeField] private int _spawnerCount = 3;

        private SpawnMapSystem _spawnMapSystem;

        [Inject]
        private void Construct(SpawnMapSystem spawnMapSystem)
        {
            _spawnMapSystem = spawnMapSystem;
            
        }

        public void StartSpawnEnemy()
        {
            for (int i = 0; i < _spawnerCount; i++)
            {
                Vector3 spawnPos = _spawnMapSystem.GetRandomPointForSpawnEnemy();
                
                var spawner = _container.InstantiatePrefab(_spawnerPrefab, spawnPos, Quaternion.identity, transform);
                float height = spawner.transform.localScale.y/2;
                
                spawner.transform.position = new Vector3(spawnPos.x, spawnPos.y + height, spawnPos.z);
                spawner.transform.SetParent(transform);

                spawner.GetComponent<EnemySpawner>().StartSpawnerEnemy(_enemyCounts[i], _spawnIntervals[i], _enemyPrefab);
            }
        }
       
    }
}
