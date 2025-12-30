using System.Collections;
using System.Collections.Generic;
using Features.AbstractMinion;
using Features.AbstractMinion.Script;
using Features.Enemy.EnemyStats;
using Features.Hero.HeroStats.HeroHP;
using Features.PoolObject;
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
        
        private Camera _camera;
        
        private PoolMono<MinionTag> _enemyPool;
        private PoolMono<CanvasMinionSystem> _canvasPool;
        
        private Dictionary<EnemyType, PoolMono<MinionTag>> _enemyPools;
        private SpawnerConfigData _config;
        private List<MinionTag> _enemyPrefabs;
        private CanvasMinionSystem _hpBarPrefab;
        private Transform _hpBarParent;
        private Transform _enemySpawnParent;

        private int _meleeLeft;
        private int _rangeLeft;
        private int _gromillaLeft;

        private bool _running;

        public void InitSpawner(
            SpawnerConfigData config,
            List<MinionTag> enemyPrefabs,
            CanvasMinionSystem hpBarPrefab,
            Transform hpBarParent, Transform enemySpawnParent,
            Camera camera)
        {
            _camera = camera;
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
            canvasSystem.Init(gameObject, enemyHealth, camera);
            _presenterFactory.Create(viewBar, enemyHealth);
            
            CreatePool();

            _canvasPool = new PoolMono<CanvasMinionSystem>(_hpBarPrefab, 15, hpBarParent)
            { autoExpand = true };
            
            StartCoroutine(SpawnerLoop());
        }

        private void CreatePool()
        {
            _enemyPools = new Dictionary<EnemyType, PoolMono<MinionTag>>
            {
                { EnemyType.Melee, new PoolMono<MinionTag>(_enemyPrefabs[0], 10, _enemySpawnParent) },
                { EnemyType.Range, new PoolMono<MinionTag>(_enemyPrefabs[1], 10, _enemySpawnParent) },
                { EnemyType.Gromilla, new PoolMono<MinionTag>(_enemyPrefabs[2], 5, _enemySpawnParent) }
            };

            foreach (var pool in _enemyPools.Values)
                pool.autoExpand = true;
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
            if (!_enemyPools.TryGetValue(type, out var pool))
            {
                return;
            }

            var enemy = pool.GetFreeElement();
            
            enemy.gameObject.SetActive(true);
            enemy.transform.SetParent(_enemySpawnParent);
            enemy.transform.position = transform.position;

            var canvas = _canvasPool.GetFreeElement();
            canvas.gameObject.SetActive(true);
            canvas.transform.SetParent(_hpBarParent);

            _enemyProvider.RegisterEnemy(enemy.gameObject, canvas.gameObject, type);

            float h = enemy.transform.localScale.y;
            enemy.transform.position += Vector3.up * h;
        }

        private void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int r = Random.Range(0, i + 1);
                (list[i], list[r]) = (list[r], list[i]);
            }
        }

        private void OnDisable() => _running = false;
    }
}
