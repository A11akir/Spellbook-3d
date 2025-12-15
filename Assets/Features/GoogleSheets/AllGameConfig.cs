using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Features.GoogleSheets
{
    [Serializable]
    public class AllGameConfig
    {
        public List<StatsMinionConfig> StatsMinion;
        public List<SpellStatsConfig> StatsSpell;
        public List<SpellsKitConfig> SpellsKit;
        public List<SpawnersConfig> SpawnersStats;
    }

}