using UnityEngine;

namespace Features.Hero.HeroStats.HeroHP
{
    [CreateAssetMenu(
        fileName = "Spawners",
        menuName = "Configs/Spawners/Level1.1",
        order = 1)]
    public class SpawnerLevel1_1 : ScriptableObject, ISpawner
    {
        [SerializeField] private int _countMelee;
        [SerializeField] private int _countRange;
        [SerializeField] private int _countGorilla;
        [SerializeField] private int _countSpawners;
        [SerializeField] private int _spawnInterval;
        [SerializeField] private int _speed;

        [SerializeField] private int _meleeCoefIncrease;
        [SerializeField] private int _rangeCoefIncrease;
        [SerializeField] private int _gorillaCoefIncrease;

        public int CountMelee
        {
            get => _countMelee;
            set => _countMelee = value;
        }

        public int CountRange
        {
            get => _countRange;
            set => _countRange = value;
        }

        public int CountGorilla
        {
            get => _countGorilla;
            set => _countGorilla = value;
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

        public int Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public int MeleeCoefIncrease
        {
            get => _meleeCoefIncrease;
            set => _meleeCoefIncrease = value;
        }

        public int RangeCoefIncrease
        {
            get => _rangeCoefIncrease;
            set => _rangeCoefIncrease = value;
        }

        public int GorillaCoefIncrease
        {
            get => _gorillaCoefIncrease;
            set => _gorillaCoefIncrease = value;
        }
    }
}