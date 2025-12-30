using System.Collections;
using System.Collections.Generic;
using Features.AbstractMinion.Script;
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
        private Camera _camera;
        [SerializeField] private CanvasMinionSystem _hpBarPrefab;
        [SerializeField] private Transform _hpBarParent;

        private Transform _spawnEnemyParent;

        [SerializeField] private GameObject _spawnerPrefab;
        [SerializeField] private List<MinionTag> _enemyPrefabs;
        
        [Inject]
        private void Construct(SpawnMapSystem spawnMapSystem, DiContainer container, SpawnerConfigData configData, Camera camera)
        {
            _camera = camera;
            _spawnMapSystem = spawnMapSystem;
            _container = container;
            _configData = configData;
        }

        public void SetConfig()
        {
            
        }

        public void StartSpawnEnemy()
        {
            _spawnEnemyParent = transform;
            for (int i = 0; i < _configData.CountSpawners; i++)
            {
                var spawner = _container.InstantiatePrefab(_spawnerPrefab, parentTransform: transform);
                
                spawner.transform.position = _spawnMapSystem.GetRandomPointForSpawnEnemy(spawner);

                spawner.GetComponent<EnemySpawner>()
                    .InitSpawner(_configData, _enemyPrefabs, _hpBarPrefab, _hpBarParent, _spawnEnemyParent, _camera);
            }
        }
    }
}
