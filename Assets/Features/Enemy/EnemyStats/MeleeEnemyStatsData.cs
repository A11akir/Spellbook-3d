using Features.AbstractMinion;
using Features.AbstractMinion.Script;
using Features.Hero.HeroStats.HeroHP;
using UnityEngine;

namespace Features.Enemy.EnemyStats
{
    [CreateAssetMenu(
        fileName = "HeroStatsData",
        menuName = "Configs/Enemy/MeleeEnemy Stats Data",
        order = 1)]
    public class MeleeEnemyStatsData : ScriptableObject, IMinionStatsData
    {
        [SerializeField] private int _moveSpeed;
        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private int _attackSpeed;
        [SerializeField] private EnemyType _enemyType; 

        public EnemyType EnemyType
        {
            get => _enemyType;
            set => _enemyType = value;
        }
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