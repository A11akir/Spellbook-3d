using System;
using System.Linq;
using Features.Enemy.EnemyStats;
using Features.Hero.HeroInstance;
using Features.Hero.HeroStats.HeroHP;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Features.Enemy.EnemyAttack
{
    public class EnemyAttack : MonoBehaviour
    {
        private Health _health;
        private HeroProvider _heroProvider;
        private MeleeEnemyStatsData _stats;
         
        public float AttackCooldown = 3f;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask;
        
        public float Cleavage = 5f;
        public float EffectiveDistance = 0.5f;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        [Inject]
        private void Construct(Health health, HeroProvider heroProvider)
        {
            _health = health;
            _heroProvider = heroProvider;
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
                _attackCooldown -= Time.deltaTime;
        }

        private bool CanAttack() =>
            _attackIsActive && !_isAttacking && CooldownIsUp();

        private void OnAttack()
        {
            if (Hit(out Collider hit))
            {
                _health.TakeDamage(_stats.Damage);
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
             _attackCooldown <= 0;

        // ReSharper disable Unity.PerformanceAnalysis
        private void StartAttack()
        {
            transform.LookAt(_heroProvider.HeroReference.transform);
            _isAttacking = true;
            
            OnAttack();
        }
        private void OnAttackEnded()
        {
            _attackCooldown = AttackCooldown;
            _isAttacking = false;
        } 

        public void DisableAttack() => _attackIsActive = false;
        public void EnableAttack() => _attackIsActive = true;
    }
}