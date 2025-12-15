using Features.Enemy.EnemyStats;
using Features.Hero.HeroInstance;
using UnityEngine;
using Zenject;

namespace Features.Enemy.EnemyAttack
{
    public class MeleeEnemyAttack : MonoBehaviour, IEnemyAttack
    {
        private HeroProvider _targetHero;
        private MeleeEnemyStatsData  _stats;
        private float _attackCooldownCache;
        private bool _isAttacking;
        private int _layerMask;
        
        public float Cleavage = 5f;
        public float EffectiveDistance = 0.5f;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        [Inject]
        private void Construct(HeroProvider heroProvider)
        {
            _targetHero = heroProvider;
        }
        
        private void OnEnable() =>
            _layerMask = 1 << LayerMask.NameToLayer($"Player");

        private void Update()
        {
            UpdateCooldown();
            if (CanAttack())
                StartAttack();
        }

        private void UpdateCooldown()
        {
            if (!CooldownIsUp())
                _attackCooldownCache -= Time.deltaTime;
        }

        private bool CanAttack() =>
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                _targetHero.Health.TakeDamage(_stats.Damage);
                OnAttackEnded();
            }
        }

        private bool Hit(out Collider hit)
        {
            var hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

            GetFirstHit(out hit, hitCount);

            if (hitCount > 0)
                return true;
            return false;
        }

        private void GetFirstHit(out Collider hit, int hitCount)
        {
            hit = hitCount > 0 ? _hits[0] : null;
        }

        private Vector3 StartPoint() => 
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveDistance;

        private bool CooldownIsUp() =>
             _attackCooldownCache <= 0;

        private void StartAttack()
        {
            transform.LookAt(_targetHero.HeroReference.transform);
            _isAttacking = true;
            
            OnAttack();
        }
        private void OnAttackEnded()
        {
            _attackCooldownCache = _stats.AttackSpeed;
            _isAttacking = false;
        } 

        public void DisableAttack() => _attackIsActive = false;
        public void EnableAttack() => _attackIsActive = true;
    }
}