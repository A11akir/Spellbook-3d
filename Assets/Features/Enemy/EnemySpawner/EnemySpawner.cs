using System.Collections;
using System.Collections.Generic;
using Features.AbstractMinion;
using Features.Enemy.EnemyStats;
using Features.Hero.HeroStats.HeroHP;
using UnityEngine;
using Zenject;

namespace Features.Enemy.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        [Inject] private EnemyProvider _enemyProvider;
        [Inject] private BaseSpawnerStatsData _spawnerStatsData;
        [Inject] private  HpBarPresenterFactory _presenterFactory;
        
        private SpawnerConfigData _config;
        private List<GameObject> _enemyPrefabs;
        private GameObject _hpBarPrefab;
        private Transform _hpBarParent;
        private Transform _enemySpawnParent;

        private int _meleeLeft;
        private int _rangeLeft;
        private int _gromillaLeft;

        private bool _running;

        public void InitSpawner(
            SpawnerConfigData config,
            List<GameObject> enemyPrefabs,
            GameObject hpBarPrefab,
            Transform hpBarParent, Transform enemySpawnParent)
        {
            _config = config;
            _enemyPrefabs = enemyPrefabs;
            _hpBarPrefab = hpBarPrefab;
            _hpBarParent = hpBarParent;
            _enemySpawnParent = enemySpawnParent;

            _meleeLeft = _config.CountMeleeSpawners;
            _rangeLeft = _config.CountRangeSpawners;
            _gromillaLeft = _config.CountGromillaSpawners;

            _running = true;
            
            var canvas = _container.InstantiatePrefab(_hpBarPrefab, _hpBarParent);
            
            var viewBar = canvas.GetComponentInChildren<HpBarView>();
            var canvasSystem = canvas.GetComponent<CanvasMinionSystem>();
            var enemyHealth = GetComponentInChildren<IHealth>();

            enemyHealth.MaxHp = _spawnerStatsData.Health;
            enemyHealth.ResetHp();
            canvasSystem.Init(gameObject, enemyHealth);
            _presenterFactory.Create(viewBar, enemyHealth);


            StartCoroutine(SpawnerLoop());
        }

        private IEnumerator SpawnerLoop()
        {
            while (_running)
            {
                ResetCounters();
                yield return SpawnWave();
                yield return new WaitForSeconds(_config.SpawnInterval);
            }
        }

        private void ResetCounters()
        {
            _meleeLeft = _config.CountMeleeSpawners;
            _rangeLeft = _config.CountRangeSpawners;
            _gromillaLeft = _config.CountGromillaSpawners;
        }

        private IEnumerator SpawnWave()
        {
            List<EnemyType> queue = BuildSpawnQueue();

            Shuffle(queue);

            foreach (var type in queue)
            {
                if (!_running) yield break;

                SpawnSingle(type);
                yield return new WaitForSeconds(_config.SpawnDelay);
            }
        }
        
        private List<EnemyType> BuildSpawnQueue()
        {
            var list = new List<EnemyType>();

            for (int i = 0; i < _meleeLeft; i++) list.Add(EnemyType.Melee);
            for (int i = 0; i < _rangeLeft; i++) list.Add(EnemyType.Range);
            for (int i = 0; i < _gromillaLeft; i++) list.Add(EnemyType.Gromilla);

            return list;
        }

        private void SpawnSingle(EnemyType type)
        {
            if (type == EnemyType.Melee && _meleeLeft > 0) _meleeLeft--;
            else if (type == EnemyType.Range && _rangeLeft > 0) _rangeLeft--;
            else if (type == EnemyType.Gromilla && _gromillaLeft > 0) _gromillaLeft--;

            GameObject selected = SelectPrefab(type);

            var enemy = _container.InstantiatePrefab(selected, transform.position, Quaternion.identity, _enemySpawnParent);
            var canvas = _container.InstantiatePrefab(_hpBarPrefab, _hpBarParent);

            _enemyProvider.RegisterEnemy(enemy, canvas, type);

            float h = enemy.transform.localScale.y;
            enemy.transform.position += Vector3.up * h;
        }

        private GameObject SelectPrefab(EnemyType type)
        {
            int index = type switch
            {
                EnemyType.Melee => 0,
                EnemyType.Range => 1,
                EnemyType.Gromilla => 2,
                _ => 0
            };

            return _enemyPrefabs[index];
        }

        private void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int r = Random.Range(0, i + 1);
                (list[i], list[r]) = (list[r], list[i]);
            }
        }

        private void OnDestroy() => _running = false;
    }
}
