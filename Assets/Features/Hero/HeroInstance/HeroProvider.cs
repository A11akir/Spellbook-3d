using Cinemachine;
using Features.AbstractMinion;
using Features.AbstractMinion.Script;
using Features.Enemy.EnemySpawner;
using Features.Hero.HeroMove;
using Features.Hero.HeroStats;
using Features.Hero.HeroStats.HeroHP;
using Features.Spells;
using UnityEngine;
using Zenject;

namespace Features.Hero.HeroInstance
{
    public class HeroProvider : MonoBehaviour
    {
        [SerializeField] private HpBarView _heroBarView;
        [Inject] private CinemachineVirtualCamera _cinemachineVirtualCamera;
        public GameObject HeroReference { get; private set; }

        public SpellsMonobehSpawner _spellsMonobehSpawner;
        private MovementHero _movementHero;
        private ChaosVisualAnchor _chaosVisualAnchor;
        private HpBarPresenterFactory _presenterFactory;
        private HeroStatsData _heroStatsData;
        private CharacterController _characterController;
        private float _heroWidth;
        public IHealth Health;

        [Inject]
        private void Construct(HeroStatsData heroStatsData, HpBarPresenterFactory presenterFactory)
        {
            _heroStatsData = heroStatsData;
            _presenterFactory = presenterFactory;
        }
        
        public void SetDependencies(GameObject heroReference)
        {
            HeroReference = heroReference;
            _cinemachineVirtualCamera.Follow = heroReference.transform;
            _chaosVisualAnchor = HeroReference.GetComponentInChildren<ChaosVisualAnchor>();
            _characterController = HeroReference.GetComponentInChildren<CharacterController>();
            _spellsMonobehSpawner = HeroReference.GetComponentInChildren<SpellsMonobehSpawner>();
            _movementHero = HeroReference.GetComponent<MovementHero>();
            Health = HeroReference.GetComponent<Health>();
            _presenterFactory.Create(_heroBarView, Health);
        }

        public void SetConfig()
        {
            _movementHero._moveSpeed = _heroStatsData.MoveSpeed;
            Health.MaxHp = _heroStatsData.Health;
            Health.ResetHp();
        }

        public float GetSkinWidth()
        { 
            _heroWidth = _characterController.radius * 2;
            return _heroWidth;
        }

        public IHealth GetHealth() => Health;

        public ChaosVisualAnchor GetPentagramAnchor() => _chaosVisualAnchor;
    }
}