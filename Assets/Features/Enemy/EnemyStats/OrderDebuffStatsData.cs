using UnityEngine;

namespace Features.Enemy.EnemyStats
{
    [CreateAssetMenu(
        fileName = "HeroStatsData",
        menuName = "Configs/ScaleDebuff/Order",
        order = 1)]
    public class OrderDebuffStatsData : ScriptableObject, IScaleDebuffStatsData
    {
        [SerializeField] private int _timeDebuff;
        
        public int TimeDebuff
        {
            get => _timeDebuff;
            set => _timeDebuff = value;
        }
    }
}