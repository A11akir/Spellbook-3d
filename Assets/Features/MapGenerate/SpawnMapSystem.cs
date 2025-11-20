using System.Collections.Generic;
using UnityEngine;

namespace Features.MapGenerate
{
    public class SpawnMapSystem : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _mapPrefabs;
        [SerializeField] private List<Transform> _spawnPoints;


        public void SpawnMap()
        {
            foreach (var point in _spawnPoints)
            {
                Instantiate(_mapPrefabs[Random.Range(0, _mapPrefabs.Count)], point.position, Quaternion.identity);
            }
        }
    }
}