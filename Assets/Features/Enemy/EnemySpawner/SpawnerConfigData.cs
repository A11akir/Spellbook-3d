using UnityEngine;

namespace Features.Enemy.EnemySpawner
{
    [CreateAssetMenu(
        fileName = "SpawnerConfigData",
        menuName = "Configs/Enemy/Spawner Config Data",
        order = 1)]
    public class SpawnerConfigData : ScriptableObject
    {
        [SerializeField] private string _level;
        [SerializeField] private int _countMeleeSpawners;
        [SerializeField] private int _countRangeSpawners;
        [SerializeField] private int _countGromillaSpawners;
        [SerializeField] private int _countSpawners;
        [SerializeField] private int _spawnInterval;
        [SerializeField] private float _spawnDelay;

        public string Level
        {
            get => _level;
            set => _level = value;
        }

        public int CountMeleeSpawners
        {
            get => _countMeleeSpawners;
            set => _countMeleeSpawners = value;
        }

        public int CountRangeSpawners
        {
            get => _countRangeSpawners;
            set => _countRangeSpawners = value;
        }

        public int CountGromillaSpawners
        {
            get => _countGromillaSpawners;
            set => _countGromillaSpawners = value;
        }

        public int CountSpawners
        {
            get => _countSpawners;
            set => _countSpawners = value;
        }

        public int SpawnInterval
        {
            get => _spawnInterval;
            set => _spawnInterval = value;
        }

        public float SpawnDelay
        {
            get => _spawnDelay;
            set => _spawnDelay = value;
        }
    }
}