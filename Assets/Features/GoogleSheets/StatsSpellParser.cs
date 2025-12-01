using System;
using System.Collections.Generic;

namespace Features.GoogleSheets
{
    public class StatsSpellParser : IGoggleSheetsParser
    {
        private readonly AllGameConfig _allGameConfig;
        private SpellStatsConfig _currentStatsSpellConfig;

        public StatsSpellParser(AllGameConfig allGameConfig)
        {
            _allGameConfig = allGameConfig;
            _allGameConfig.StatsSpell = new List<SpellStatsConfig>();
        }
        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "Id":
                    _currentStatsSpellConfig = new SpellStatsConfig
                    {
                        Id = token
                    };
                    _allGameConfig.StatsSpell.Add(_currentStatsSpellConfig);
                    break;
                case "Damage":
                    _currentStatsSpellConfig.Damage = Convert.ToInt32(token);
                    break;
                case "LifeTime":
                    _currentStatsSpellConfig.LifeTime = Convert.ToInt32(token);
                    break;
                case "MissleSpeed":
                    _currentStatsSpellConfig.MissleSpeed = Convert.ToInt32(token);
                    break;
            }
        }
    }
}