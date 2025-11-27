using System;
using Cinemachine;
using Features.Hero.HeroMove;
using Features.Hero.HeroStats.HeroHP;
using UnityEngine;
using Zenject;

namespace Features.Hero.HeroInstance
{
    public class InstanceHeroSystem : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        [SerializeField] private GameObject _heroPrefab;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        public GameObject HeroReference { get; private set; }
        
        public void InstanceHero()
        {
            HeroReference = _container.InstantiatePrefab(_heroPrefab);
            _cinemachineVirtualCamera.Follow = HeroReference.transform;
        }


    }
}