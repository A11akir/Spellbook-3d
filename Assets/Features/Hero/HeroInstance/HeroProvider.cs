using Cinemachine;
using Features.Hero.HeroMove;
using Features.Hero.HeroStats;
using Features.Hero.HeroStats.HeroHP;
using UnityEngine;
using Zenject;

namespace Features.Hero.HeroInstance
{
    public class HeroProvider 
    {
        [Inject] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        public GameObject HeroReference { get; private set; }
        
        private HeroStatsData _heroStatsData;
        private Health _health;
        private CharacterController _characterController;
        private float _heroWidth;
        public HeroProvider(HeroStatsData heroStatsData, Health health)
        {
            _heroStatsData = heroStatsData;
            _health = health;
        }
        
        public void SetDependencies(GameObject heroReference)
        {
            HeroReference = heroReference;
            _cinemachineVirtualCamera.Follow = heroReference.transform;
            
            _characterController = HeroReference.GetComponent<CharacterController>();
            
            _health.MaxHp = _heroStatsData.Health;
            _health.ResetHp();
        }

        public float GetSkinWidth()
        { 
            _heroWidth = _characterController.radius * 2;
            return _heroWidth;
        }
    }
}