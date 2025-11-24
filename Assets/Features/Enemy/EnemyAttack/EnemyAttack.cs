using System;
using System.Linq;
using Features.Hero.HeroInstance;
using Features.Hero.HeroStats.HeroHP;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Features.Enemy.EnemyAttack
{
    public class EnemyAttack : MonoBehaviour
    {
        [Inject] private HeroMarker _hero;
        
        public float AttackCooldown = 3f;
        private float _attackCooldown;
        private bool _isAttacking;
        private int _layerMask;

        public float Damage = 5;
        public float Cleavage = 5f;
        public float EffectiveDistance = 0.5f;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        private void OnEnable()
        {
            _layerMask = 1 << LayerMask.NameToLayer($"Player");
        }

        private void Update()
        {
            
            UpdateCooldown();
            if (CanAttack())
            {
                Debug.Log("CanAttack");
                StartAttack();
            }
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
            Debug.Log("oNattack");
            if (Hit(out Collider hit))
            {
                Debug.Log("Hit");
                hit.transform.GetComponent<HeroHP>().TakeDamage(Damage);
            }
        }

        private bool Hit(out Collider hit)
        {
            var hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), Cleavage, _hits, _layerMask);

            hit = _hits.FirstOrDefault();
            
            return hitCount > 0;
        }

        private Vector3 StartPoint()
        {
            return new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveDistance;
        }


        private bool CooldownIsUp()
        {
            return _attackCooldown <= 0;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private void StartAttack()
        {
            transform.LookAt(_hero.GetTransform());
            _isAttacking = true;
            
            Debug.Log("StartAttack");
            OnAttack();
        }
        private void OnAttackEnded()
        {
            _attackCooldown = AttackCooldown;
            _isAttacking = false;
        } 

        public void DisableAttack()
        {
            _attackIsActive = false;
        }
        public void EnablleAttack()
        {
            _attackIsActive = true;
        }
    }
}