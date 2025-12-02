namespace Features.Hero.HeroStats.HeroHP
{
    public interface ISpawner
    {
        int CountMelee { get; set; }
        int CountRange { get; set; }
        int CountGorilla { get; set; }
        int CountSpawners { get; set; }
        int SpawnInterval { get; set; }
        int Speed { get; set; }
        int MeleeCoefIncrease { get; set; }
        int RangeCoefIncrease { get; set; }
        int GorillaCoefIncrease { get; set; }
    }
}