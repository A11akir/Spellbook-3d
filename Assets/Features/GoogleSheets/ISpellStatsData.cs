namespace Features.GoogleSheets
{
    public interface ISpellStatsData
    {
        int Damage { get; set; }
        int LifeTime { get; set; }
        int MissileSpeed { get; set; }
        float Cooldown { get; set; }
        float MaxCooldown { get; set; }
        int Cost { get; set; }
        bool TypeMagic { get; set; }
        int Range { get; set; }
    }
}