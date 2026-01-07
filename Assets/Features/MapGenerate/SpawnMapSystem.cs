using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Features.MapGenerate
{
    public class SpawnMapSystem : MonoBehaviour
    {
        [Inject] private DynamicNavMeshBake _navMeshBake;
        [System.Serializable]
        public class GameObjectArray
        {
            public GameObject[] Prefabs;
        }

        [SerializeField] private GameObjectArray[] _mapPrefabs = new GameObjectArray[9];
        [SerializeField] private Transform _mapParent;
        private List<Transform> _spawnPoints = new List<Transform>();

        [SerializeField] private int gridSize = 3;
        [SerializeField] private float chunkSize;

        public void GenerateSpawnPoints()
        {
            int halfGrid = gridSize / 2;

            /*chunkSize = _mapPrefabs.FirstOrDefault()!.transform.localScale.x;*/

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

            for (int i = 0; i < Mathf.Min(_spawnPoints.Count, _mapPrefabs.Length); i++)
            {
                var point = _spawnPoints[i];
                var prefabArray = _mapPrefabs[i];
                
                if (prefabArray == null || prefabArray.Prefabs == null || prefabArray.Prefabs.Length == 0) 
                  continue;
                
                var randomPrefab = prefabArray.Prefabs[Random.Range(0, prefabArray.Prefabs.Length)];
                
                if (randomPrefab == null) { continue; }
        
                var obj = Instantiate(randomPrefab, point.position, randomPrefab.transform.rotation);
                obj.transform.SetParent(point.transform);
            }
            _navMeshBake.BuildNavMesh();
        }

        private int GetRandomSpawnIndex()
        {
            int count = _spawnPoints.Count;

            if (count == 0) return -1;

            bool isOdd = count % 2 != 0;
            int middleIndex = count / 2;
            
            if (!isOdd) return Random.Range(0, count);
            
            int index = Random.Range(0, count - 1);
            if (index >= middleIndex) index++;

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