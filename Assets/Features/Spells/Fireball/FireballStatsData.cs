using UnityEngine;

namespace Features.Spells.Fireball
{
    [CreateAssetMenu(
        fileName = "SpellStatsData",
        menuName = "Configs/Spell/Fireball Stats Data",
        order = 1)]
    public class FireballStatsData : SpellStateBase
    {
        [SerializeField] private int _cooldown;
        [SerializeField] private int _missleSpeed;
        [SerializeField] private int _damage;
        [SerializeField] private int _lifeTime;

        public int LifeTime
        {
            get => _lifeTime;
            set => _lifeTime = value;
        }
        public int MissleSpeed
        {
            get => _missleSpeed;
            set => _missleSpeed = value;
        }
        public int Cooldown
        {
            get => _cooldown;
            set => _cooldown = value;
        }
        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }
    }
}