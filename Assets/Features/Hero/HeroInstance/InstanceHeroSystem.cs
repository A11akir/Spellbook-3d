using Cinemachine;
using Features.Hero.HeroMove;
using UnityEngine;
using Zenject;

namespace Features.Hero.HeroInstance
{
    public class InstanceHeroSystem : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        [SerializeField] private GameObject _heroPrefab;
        [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;

        public void InstanceHero()
        {
            var hero = _container.InstantiatePrefab(_heroPrefab);
            _cinemachineVirtualCamera.Follow = hero.transform;
        }
    }
}