using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.Enemy.EnemySpawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        [Inject] private EnemyProvider _enemyProvider;

        private SpawnerConfigData _config;
        private List<GameObject> _enemyPrefabs;
        private GameObject _hpBarPrefab;
        private Transform _hpBarParent;

        private int meleeLeft;
        private int rangeLeft;
        private int gromillaLeft;

        private bool _running;

        public void InitSpawner(
            SpawnerConfigData config,
            List<GameObject> enemyPrefabs,
            GameObject hpBarPrefab,
            Transform hpBarParent)
        {
            _config = config;
            _enemyPrefabs = enemyPrefabs;
            _hpBarPrefab = hpBarPrefab;
            _hpBarParent = hpBarParent;

            meleeLeft = _config.CountMeleeSpawners;
            rangeLeft = _config.CountRangeSpawners;
            gromillaLeft = _config.CountGromillaSpawners;

            _running = true;

            StartCoroutine(SpawnerLoop());
        }

        private IEnumerator SpawnerLoop()
        {
            while (_running)
            {
                yield return SpawnWave();
                yield return new WaitForSeconds(_config.SpawnInterval);
            }
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

        public enum EnemyType { Melee, Range, Gromilla }

        private List<EnemyType> BuildSpawnQueue()
        {
            var list = new List<EnemyType>();

            for (int i = 0; i < meleeLeft; i++) list.Add(EnemyType.Melee);
            for (int i = 0; i < rangeLeft; i++) list.Add(EnemyType.Range);
            for (int i = 0; i < gromillaLeft; i++) list.Add(EnemyType.Gromilla);

            return list;
        }

        private void SpawnSingle(EnemyType type)
        {
            if (type == EnemyType.Melee && meleeLeft > 0) meleeLeft--;
            else if (type == EnemyType.Range && rangeLeft > 0) rangeLeft--;
            else if (type == EnemyType.Gromilla && gromillaLeft > 0) gromillaLeft--;

            GameObject selected = SelectPrefab(type);

            var enemy = _container.InstantiatePrefab(selected, transform.position, Quaternion.identity, transform);
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
