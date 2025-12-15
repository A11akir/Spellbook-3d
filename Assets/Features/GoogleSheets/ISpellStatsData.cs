namespace Features.GoogleSheets
{
    public interface ISpellStatsData
    {
        int Damage { get; set; }
        int LifeTime { get; set; }
        int MissileSpeed { get; set; }
        int Cooldown { get; set; }
    }
}