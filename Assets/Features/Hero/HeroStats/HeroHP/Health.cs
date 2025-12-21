using System;
using Features.AbstractMinion;
using Features.Spells.Fireball;
using UnityEngine;

namespace Features.Hero.HeroStats.HeroHP
{
    public class Health : MonoBehaviour, IHealth, IDamageable
    {
        public event Action HpChanged;
        public event Action OnDeath;

        private float _currentHp;
        public float CurrentHp
        {
            get => _currentHp;
            set
            {
                if (!Mathf.Approximately(_currentHp, value))
                {
                    _currentHp = value;
           
                    HpChanged?.Invoke();
                }
            }
        }

        public float MaxHp { get; set; }


        public void ResetHp() => CurrentHp = MaxHp;
        public void Heal(float amount)
        {
            
        }

        public void TakeDamage(float damage)
        {
            CurrentHp -= damage;

            if (CurrentHp <= 0)
            {
                OnDeath?.Invoke();
                Destroy(gameObject);
          
            }
        }
    }
}