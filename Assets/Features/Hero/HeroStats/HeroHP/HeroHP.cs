using System;
using UnityEngine;

namespace Features.Hero.HeroStats.HeroHP
{
    public class HeroHP : MonoBehaviour
    {
        public event Action HPChanged;

        private float _currentHp;
        public float CurrentHP
        {
            get => _currentHp;
            set
            {
                if (!Mathf.Approximately(_currentHp, value))
                {
                    Debug.Log("CurrentHp");
                    _currentHp = value;
                    HPChanged?.Invoke();
                }
            }
        }

        public float MaxHP;

        public void ResetHP() => CurrentHP = MaxHP;

        public void TakeDamage(float damage)
        {
            Debug.Log(CurrentHP);
            if (CurrentHP <= 0) return;
            CurrentHP -= damage;
        }
    }
}