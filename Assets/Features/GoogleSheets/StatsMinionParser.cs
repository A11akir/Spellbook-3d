using System;
using System.Collections.Generic;
using UnityEngine;

namespace Features.GoogleSheets
{
    public class StatsMinionParser : IGoggleSheetsParser
    {
        private readonly AllGameConfig _allGameConfig;
        private StatsMinionConfig _currentStatsMinionConfig;

        public StatsMinionParser(AllGameConfig allGameConfig)
        {
            _allGameConfig = allGameConfig;
            _allGameConfig.StatsMinion = new List<StatsMinionConfig>();
        }
        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "Id":
                    _currentStatsMinionConfig = new StatsMinionConfig
                {
                    Id = token
                };
                    _allGameConfig.StatsMinion.Add(_currentStatsMinionConfig);
                    break;
                case "Damage":
                    _currentStatsMinionConfig.Damage = Convert.ToInt32(token);
                    break;
                case "Speed":
                    _currentStatsMinionConfig.Speed = Convert.ToInt32(token);
                    break;
                case "Health":
                    _currentStatsMinionConfig.Health = Convert.ToInt32(token);
                    break;
            }
        }
    }
}