using System;

namespace Features.GoogleSheets
{
    [Serializable]
    public class StatsMinionConfig
    {
        public string Id;
        public int Damage;
        public int Speed;
        public int Health;
    }  
    [Serializable]
    public class SpellStatsConfig
    {
        public string Id;
        public int Damage;
        public int LifeTime;
        public int MissleSpeed;
    }
    
    
}