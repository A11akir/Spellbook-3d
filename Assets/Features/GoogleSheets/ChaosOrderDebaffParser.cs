using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.GoogleSheets
{
    public class ChaosOrderDebuffParser : IGoggleSheetsParser
    {
        private readonly AllGameConfig _allGameConfig;
        private ScaleDebuffConfig _currentScaleDebuffConfig;

        private readonly List<IScaleDebuffStatsData> _targetSO = new();

        public ChaosOrderDebuffParser(AllGameConfig allGameConfig)
        {
            _allGameConfig = allGameConfig;
            _allGameConfig.ScaleDebuffStats = new List<ScaleDebuffConfig>();

            LoadAllSpellSO();
        }
        
        private void LoadAllSpellSO()
        {
            string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new[] {"Assets/Features"});
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ScriptableObject so = AssetDatabase.LoadAssetAtPath<ScriptableObject>(path);

                if (so is IScaleDebuffStatsData debuff)
                {
                    _targetSO.Add(debuff);
                }
            }
        }

        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "Id":
                    _currentScaleDebuffConfig = new ScaleDebuffConfig
                    {
                        Id = token
                    };
                    _allGameConfig.ScaleDebuffStats.Add(_currentScaleDebuffConfig);
                    break;

                case "TimeDebuff":
                    _currentScaleDebuffConfig.TimeDebuff = Convert.ToInt32(token);
                    break;
            }
        }

        public void ApplyToSO()
        {
            foreach (var cfg in _allGameConfig.ScaleDebuffStats)
            {
                var so = _targetSO
                    .FirstOrDefault(x => (x as ScriptableObject).name == cfg.Id);

                if (so == null)
                {
                    Debug.LogWarning($"❌ Spell SO not found for id: {cfg.Id}");
                    continue;
                }

                so.TimeDebuff = cfg.TimeDebuff;
 

                EditorUtility.SetDirty(so as Object);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}