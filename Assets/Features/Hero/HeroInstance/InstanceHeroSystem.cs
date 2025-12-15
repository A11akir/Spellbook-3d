using UnityEngine;
using Zenject;

namespace Features.Hero.HeroInstance
{
    public class InstanceHeroSystem : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        [Inject] private HeroProvider _heroProvider;
        [SerializeField] private GameObject _heroPrefab;
        
        public void InstanceHero() =>
            _heroProvider.SetDependencies(_container.InstantiatePrefab(_heroPrefab));
    }
}