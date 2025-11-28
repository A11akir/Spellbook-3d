using Cinemachine;
using Features.Hero.HeroMove;
using Features.Hero.HeroStats;
using UnityEngine;
using Zenject;

namespace Features.Hero.HeroInstance
{
    public class HeroProvider 
    {
        [Inject] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        [Inject] private PlayerProgress _playerProgress;
        public GameObject HeroReference { get; private set; }
        private CharacterController _characterController;
        private float _heroWidth;
        
        public void SetDependencies(GameObject heroReference)
        {
            HeroReference = heroReference;
            _cinemachineVirtualCamera.Follow = heroReference.transform;
            
            var move = HeroReference.GetComponent<MovementHero>();
            _playerProgress.SetStatsInMonobeh(move);
            _characterController = HeroReference.GetComponent<CharacterController>();
            
        }

        public float GetSkinWidth()
        { 
            _heroWidth = _characterController.radius * 2;
            Debug.Log(_heroWidth);
            return _heroWidth;
        }
    }
}