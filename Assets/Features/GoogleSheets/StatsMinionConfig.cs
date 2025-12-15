using System;
using System.Collections.Generic;

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
        public int Cooldown;
    } 
    [Serializable]
    public class SpellsKitConfig
    {
        public string Id;
        public List<string> SpellsKit;
    }
    [Serializable]
    public class SpawnersConfig
    {
        public string Level;
        public int CountMeleeSpawners;
        public int CountRangeSpawners;
        public int CountGromillaSpawners;
        public int CountSpawners;
        public int SpawnInterval;
        public float SpawnDelay;
    }
    
    
}