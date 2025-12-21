namespace Features.Enemy.EnemyAttack
{
    public interface IEnemyAttack
    {
        void EnableAttack();
        void DisableAttack();
        
        int _damage { get; set; }
        int _attackSpeed { get; set; }
    }
}