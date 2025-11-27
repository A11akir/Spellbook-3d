using System;
using UnityEngine;

namespace Features.Hero.HeroStats.HeroHP
{
    public class HeroHp
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

        public float MaxHp;
        
        public void ResetHp() => CurrentHp = MaxHp;

        public void TakeDamage(float damage)
        {
            if (CurrentHp <= 0) return;
            CurrentHp -= damage;
        }
    }
}