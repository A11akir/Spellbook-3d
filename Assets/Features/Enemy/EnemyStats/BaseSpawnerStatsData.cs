using Features.AbstractMinion;
using UnityEngine;

namespace Features.Enemy.EnemyStats
{
    [CreateAssetMenu(
        fileName = "HeroStatsData",
        menuName = "Configs/Enemy/BaseSpawner Stats Data",
        order = 1)]
    public class BaseSpawnerStatsData : ScriptableObject, IMinionStatsData
    {
        [SerializeField] private int _moveSpeed;
        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private int _attackSpeed;
        public EnemyType EnemyType { get; set; }

        public int MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }
        public int AttackSpeed
        {
            get => _attackSpeed;
            set => _attackSpeed = value;
        }

        public int Health
        {
            get => _health;
            set => _health = value;
        }
        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }
    }
}