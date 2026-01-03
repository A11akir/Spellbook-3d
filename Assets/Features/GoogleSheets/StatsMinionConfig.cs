using System;
using System.Collections.Generic;

namespace Features.GoogleSheets
{
    [Serializable]
    public class StatsMinionConfig
    {
        public string Id;
        public int Damage;
        public int MoveSpeed;
        public int Health;
        public int AttackSpeed;
    }  
    [Serializable]
    public class SpellStatsConfig
    {
        public string Id;
        public int Damage;
        public int Range;
        public int LifeTime;
        public int MissleSpeed;
        public float Cooldown;
        public float MaxCooldown;
        public int Cost;
        public bool TypeMagic;
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
    [Serializable]
    public class DashConfig
    {
        public string Id;
        public int Distance;
        public float Duration;
        public int Damage;
    }
    
}