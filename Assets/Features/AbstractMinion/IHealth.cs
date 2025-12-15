using System;

namespace Features.AbstractMinion
{
    public interface IHealth
    {
        float CurrentHp { get; }
        float MaxHp { get; set; }
        event Action HpChanged;
        void TakeDamage(float amount);
        void ResetHp();
        void Heal(float amount);
    }
}