using Cinemachine;
using Features.AbstractMinion;
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
        
        private HpBarPresenter _hpBarPresenter;
        private HeroStatsData _heroStatsData;
        private IHealth _health;
        public IHealth Health => _health;
        private CharacterController _characterController;
        private float _heroWidth;
        
        public HeroProvider(HeroStatsData heroStatsData, IHealth health)
        {
            _heroStatsData = heroStatsData;
            _health = health;
        }
        
        public void SetDependencies(GameObject heroReference)
        {
            HeroReference = heroReference;
            _cinemachineVirtualCamera.Follow = heroReference.transform;
            _characterController = HeroReference.GetComponent<CharacterController>();

            var movement = HeroReference.GetComponent<MovementHero>();
            movement._moveSpeed = _heroStatsData.MoveSpeed;
            
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