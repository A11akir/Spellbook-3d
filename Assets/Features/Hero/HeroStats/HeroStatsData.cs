using System;
using Features.AbstractMinion;
using Features.AbstractMinion.Script;
using UnityEngine;
using UnityEngine.Serialization;

namespace Features.Hero.HeroStats.HeroHP
{
    [CreateAssetMenu(
        fileName = "HeroStatsData",
        menuName = "Configs/Hero/Hero Stats Data",
        order = 1)]
    public class HeroStatsData : ScriptableObject, IMinionStatsData
    {
        [SerializeField] private int _moveSpeed;
        [SerializeField] private int _speed;
        [SerializeField] private int _health;

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
        public int Health
        {
            get => _health;
            set => _health = value;
        }

        public int Damage { get; set; }
        public int AttackSpeed { get; set; }
    }
}