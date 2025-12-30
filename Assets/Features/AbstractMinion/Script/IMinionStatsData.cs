namespace Features.AbstractMinion.Script
{
    public interface IMinionStatsData
    {
        EnemyType EnemyType { get; set; }
        int MoveSpeed { get; set; }
        int Health { get; set; }
        int Damage { get; set; }
        int AttackSpeed { get; set; }
    }
}