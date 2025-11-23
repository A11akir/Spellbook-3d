using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Features.MapGenerate
{
    public class SpawnMapSystem : MonoBehaviour
    {
        [Inject] private DynamicNavMeshBake _navMeshBake;
        [SerializeField] private List<GameObject> _mapPrefabs;
        [SerializeField] private List<Transform> _spawnPoints;

        public void SpawnMap()
        {
            foreach (var point in _spawnPoints)
            {
                var randomPrefab = _mapPrefabs[Random.Range(0, _mapPrefabs.Count)];
                
                var obj = Instantiate(randomPrefab, Vector3.zero, Quaternion.identity);

                // берём коллайдер уже у инстанса
                var col = obj.GetComponent<Collider>();
                float halfHeight = col.bounds.extents.y;

                // ставим объект ровно на платформу
                Vector3 spawnPos = point.position;
                spawnPos.y -= halfHeight; // если платформа на y=0, иначе + platformTop
                obj.transform.position = spawnPos;
            }
            
            _navMeshBake.BuildNavMesh();
        }

        public Vector3 GetRandomPointForSpawnEnemy()
        {
            int count = _spawnPoints.Count;

            if (count == 0)
                return Vector3.zero;
            
            bool isOdd = count % 2 != 0;
            int middleIndex = count / 2;

            int randomIndex;

            if (isOdd)
            {
                randomIndex = Random.Range(0, count - 1);
                if (randomIndex >= middleIndex)
                    randomIndex++;
            }
            else
                randomIndex = Random.Range(0, count);

            return _spawnPoints[randomIndex].position;
        }

    }
}