using Features.GoogleSheets;
using UnityEngine;

namespace Features.Spells.Fireball
{
    [CreateAssetMenu(
        fileName = "SpellStatsData",
        menuName = "Configs/Spell/Fireball Stats Data",
        order = 1)]
    public class FireballStatsData : SpellStateBase, ISpellStatsData
    {
        [SerializeField] private float _cooldown;
        [SerializeField] private float _maxCooldown;
        [SerializeField] private int _missleSpeed;
        [SerializeField] private int _damage;
        [SerializeField] private int _lifeTime;
        [SerializeField] private int _cost;
        [SerializeField] private bool _typeMagic;

        public override int Cost
        {
            get => _cost;
            set => _cost = value;
        }
        public override bool TypeMagic
        {
            get => _typeMagic;
            set => _typeMagic = value;
        }

        public int Range { get; set; }

        public int LifeTime
        {
            get => _lifeTime;
            set => _lifeTime = value;
        }

        public int MissileSpeed
        {
            get => _missleSpeed;
            set => _missleSpeed = value;
        }
        public override float Cooldown
        {
            get => _cooldown;
            set => _cooldown = value;
        }
        public override float MaxCooldown
        {
            get => _maxCooldown;
            set => _maxCooldown = value;
        }
        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }
    }
}