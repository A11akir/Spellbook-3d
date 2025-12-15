using System.Collections;
using System.Collections.Generic;
using Features.MapGenerate;
using UnityEngine;
using Zenject;

namespace Features.Enemy.EnemySpawner
{
    public class EnemySpawnerSystem : MonoBehaviour
    {
        private SpawnerConfigData _configData;
        private SpawnMapSystem _spawnMapSystem;
        private DiContainer _container;
        
        [SerializeField] private GameObject _hpBarPrefab;
        [SerializeField] private Transform _hpBarParent;

        [SerializeField] private GameObject _spawnerPrefab;
        [SerializeField] private List<GameObject> _enemyPrefab;


        [Inject]
        private void Construct(SpawnMapSystem spawnMapSystem, DiContainer container, SpawnerConfigData configData)
        {
            _spawnMapSystem = spawnMapSystem;
            _container = container;
            _configData = configData;
        }

        public void SetConfig()
        {
            
        }

        public void StartSpawnEnemy()
        {
            for (int i = 0; i < _configData.CountSpawners; i++)
            {
                Vector3 pos = _spawnMapSystem.GetRandomPointForSpawnEnemy();

                var spawner = _container.InstantiatePrefab(
                    _spawnerPrefab,
                    pos,
                    Quaternion.identity,
                    transform);

                spawner.GetComponent<EnemySpawner>()
                    .InitSpawner(_configData, _enemyPrefab, _hpBarPrefab, _hpBarParent);
            }
        }
    }
}
