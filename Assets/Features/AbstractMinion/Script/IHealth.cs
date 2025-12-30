using System;

namespace Features.AbstractMinion.Script
{
    public interface IHealth
    {
        float CurrentHp { get; }
        float MaxHp { get; set; }
        event Action HealthOver;
        event Action HpChanged;
        void TakeDamage(float amount);
        void ResetHp();
        void Heal(float amount);
    }
}