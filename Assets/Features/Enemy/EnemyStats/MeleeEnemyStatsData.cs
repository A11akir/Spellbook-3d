using Features.Hero.HeroStats.HeroHP;
using UnityEngine;

namespace Features.Enemy.EnemyStats
{
    [CreateAssetMenu(
        fileName = "HeroStatsData",
        menuName = "Configs/Enemy/MeleeEnemy Stats Data",
        order = 1)]
    public class MeleeEnemyStatsData : ScriptableObject, IMinion
    {
        [SerializeField] private int _moveSpeed;
        [SerializeField] private int _speed;
        [SerializeField] private int _damage;
        public int MoveSpeed
        {
            get => _moveSpeed;
            set => _moveSpeed = value;
        }

        public int Health
        {
            get => _speed;
            set => _speed = value;
        }
        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }
    }
}