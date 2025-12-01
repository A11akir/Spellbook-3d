using System;

[Serializable]
public class HeroStatsData : IMinion
{
    public int Speed { get; set; }
    public int Health { get; set; }
}
[Serializable]
public class MillyEnemyStatsData : IMinion
{
    public int Speed { get; set; }
    public int Health { get; set; }
}

public interface IMinion
{
     int Speed { get; set; }
     int Health { get; set; }
}