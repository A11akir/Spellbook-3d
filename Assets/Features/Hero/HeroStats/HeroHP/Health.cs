using System;
using Features.AbstractMinion;
using UnityEngine;

namespace Features.Hero.HeroStats.HeroHP
{
    public class Health : IHealth
    {
        public event Action HpChanged;

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
            if (CurrentHp <= 0) return;
            CurrentHp -= damage;
        }
    }
}