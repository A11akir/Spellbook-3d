using Features.Hero.HeroInstance;
using UnityEngine;
using Zenject;

namespace Features.Hero
{
    public class ChaosVisualEffectSystem : MonoBehaviour
    {
        [Inject] private HeroProvider _heroProvider;
        [SerializeField] private GameObject _lightScene;
        private GameObject _pentagram;
        
        private void Start()
        {
            ChaosVisualAnchor anchor = _heroProvider.GetPentagramAnchor();
            _pentagram = anchor.Pentagram;
            _pentagram.SetActive(false);
        }
        
        public void EnableChaosMode()
        {
            _lightScene.SetActive(false);
            _pentagram.SetActive(true);
        }
    
        public void DisableChaosMode()
        {
            _lightScene.SetActive(true);
            _pentagram.SetActive(false);
        }
    }
}