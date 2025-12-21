using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.GoogleSheets
{
    public class StatsSpellParser : IGoggleSheetsParser
    {
        private readonly AllGameConfig _allGameConfig;
        private SpellStatsConfig _currentStatsSpellConfig;

        private readonly List<ISpellStatsData> _targetSO = new();

        public StatsSpellParser(AllGameConfig allGameConfig)
        {
            _allGameConfig = allGameConfig;
            _allGameConfig.StatsSpell = new List<SpellStatsConfig>();

            LoadAllSpellSO();
        }
        
        private void LoadAllSpellSO()
        {
            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] {"Assets/Features"});
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

                if (so is ISpellStatsData spell)
                {
                    _targetSO.Add(spell);
                }
            }
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
                case "Cooldown":
                    _currentStatsSpellConfig.Cooldown = float.Parse(token);
                    _currentStatsSpellConfig.MaxCooldown = float.Parse(token);
                    break;
            }
        }

        public void ApplyToSO()
        {
            foreach (var cfg in _allGameConfig.StatsSpell)
            {
                var so = _targetSO
                    .FirstOrDefault(x => (x as ScriptableObject).name == cfg.Id);

                if (so == null)
                {
                    Debug.LogWarning($"❌ Spell SO not found for id: {cfg.Id}");
                    continue;
                }

                so.Damage = cfg.Damage;
                so.LifeTime = cfg.LifeTime;
                so.MissileSpeed = cfg.MissleSpeed;
                Debug.Log(so.Damage);
                Debug.Log(so);
                so.Cooldown = cfg.Cooldown;
                so.MaxCooldown = cfg.MaxCooldown;

                EditorUtility.SetDirty(so as Object);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
