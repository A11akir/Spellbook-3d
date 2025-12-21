using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Features.MapGenerate
{
    public class SpawnMapSystem : MonoBehaviour
    {
        [Inject] private DynamicNavMeshBake _navMeshBake;
        [SerializeField] private List<GameObject> _mapPrefabs;
        [SerializeField] private Transform _mapParent;
        private List<Transform> _spawnPoints = new List<Transform>();

        [SerializeField] private int gridSize = 3;
        [SerializeField] private float chunkSize;

        public void GenerateSpawnPoints()
        {
            int halfGrid = gridSize / 2;

            chunkSize = _mapPrefabs.FirstOrDefault()!.transform.localScale.x;

            for (int x = -halfGrid; x <= halfGrid; x++)
            {
                for (int z = -halfGrid; z <= halfGrid; z++)
                {
                    GameObject point = new GameObject($"SpawnPoint_{x}_{z}");
                    point.transform.SetParent(_mapParent);
                    point.transform.position = new Vector3(x * chunkSize, 0f, z * chunkSize);

                    _spawnPoints.Add(point.transform);
                }
            }
        }

        public void SpawnMap()
        {
            GenerateSpawnPoints();

            foreach (var point in _spawnPoints)
            {
                var randomPrefab = _mapPrefabs[Random.Range(0, _mapPrefabs.Count)];

                var obj = Instantiate(randomPrefab, Vector3.zero, Quaternion.identity);
                obj.transform.SetParent(point.transform);
                var col = obj.GetComponent<Collider>();
                float halfHeight = col.bounds.extents.y;

                Vector3 spawnPos = point.position;
                spawnPos.y -= halfHeight;
                obj.transform.position = spawnPos;
            }

            _navMeshBake.BuildNavMesh();
        }

        private int GetRandomSpawnIndex()
        {
            int count = _spawnPoints.Count;

            if (count == 0)
                return -1;

            bool isOdd = count % 2 != 0;
            int middleIndex = count / 2;

            // если чётное — просто любой
            if (!isOdd)
                return Random.Range(0, count);

            // если нечётное — исключаем центр
            int index = Random.Range(0, count - 1);
            if (index >= middleIndex)
                index++;

            return index;
        }

        public Vector3 GetRandomPointForSpawnEnemy(GameObject enemyPrefab)
        {
            if (_spawnPoints.Count == 0)
                return Vector3.zero;

            int randomIndex = GetRandomSpawnIndex();
            Vector3 pos = _spawnPoints[randomIndex].position;

            Collider col = enemyPrefab.GetComponent<Collider>();

            pos.y += col.bounds.extents.y;

            return pos;
        }
    }
}