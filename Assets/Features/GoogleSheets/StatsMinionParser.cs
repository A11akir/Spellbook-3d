using System;
using System.Collections.Generic;
using System.Linq;
using Features.AbstractMinion;
using Features.AbstractMinion.Script;
using Features.Hero.HeroStats.HeroHP;
using UnityEditor;
using UnityEngine;

namespace Features.GoogleSheets
{
    public class StatsMinionParser : IGoggleSheetsParser
    {
        private readonly AllGameConfig _allGameConfig;
        private StatsMinionConfig _currentStatsMinionConfig;
        
        private readonly List<IMinionStatsData> _targetSO = new();

        public StatsMinionParser(AllGameConfig allGameConfig)
        {
            _allGameConfig = allGameConfig;
            _allGameConfig.StatsMinion = new List<StatsMinionConfig>();
            
            LoadAllMinionSO();
        }

        private void LoadAllMinionSO()
        {
            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] {"Assets/Features"});
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

                if (so is IMinionStatsData minion)
                {
                    _targetSO.Add(minion);
                }
            }
        }

        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "Id":
                    _currentStatsMinionConfig = new StatsMinionConfig { Id = token };
                    _allGameConfig.StatsMinion.Add(_currentStatsMinionConfig);
                    break;
                case "Damage":
                    _currentStatsMinionConfig.Damage = Convert.ToInt32(token);
                    break;
                case "MoveSpeed":
                    _currentStatsMinionConfig.MoveSpeed = Convert.ToInt32(token);
                    break;
                case "Health":
                    _currentStatsMinionConfig.Health = Convert.ToInt32(token);
                    break;  
                case "AttackSpeed":
                    _currentStatsMinionConfig.AttackSpeed = Convert.ToInt32(token);
                    break;
            }
        }

        public void ApplyToSO()
        {
            foreach (var cfg in _allGameConfig.StatsMinion)
            {
                var so = _targetSO
                    .FirstOrDefault(x => (x as ScriptableObject).name == cfg.Id);
                if (so == null)
                {
                    Debug.LogWarning($"SO not found for minion: {cfg.Id}");
                    continue;
                }

                so.MoveSpeed = cfg.MoveSpeed;
                so.Health = cfg.Health;
                so.Damage = cfg.Damage;
                so.AttackSpeed = cfg.AttackSpeed;

                EditorUtility.SetDirty(so as UnityEngine.Object);
                Debug.Log($"✅ Updated SO: {cfg.Id}");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
