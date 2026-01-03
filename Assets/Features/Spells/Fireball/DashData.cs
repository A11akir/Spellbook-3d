using UnityEngine;

namespace Features.Spells.Fireball
{
    [CreateAssetMenu(
        fileName = "DashData",
        menuName = "Configs/Dash Data",
        order = 1)]
    public class DashData : ScriptableObject
    {
        [SerializeField] private int _distance;
        [SerializeField] private float _duration;
        [SerializeField] private int _damage;
    
        public float Duration
        {
            get => _duration;
            set => _duration = value;
        }
        public int Distance
        {
            get => _distance;
            set => _distance = value;
        }

        public int Damage
        {
            get => _damage;
            set => _damage = value;
        }
    }
}