using System.Collections.Generic;
using Features.Spells.Fireball;
using UnityEditor;
using UnityEngine;

namespace Features.GoogleSheets
{
    public class DashParser : IGoggleSheetsParser
    {
        private readonly AllGameConfig _allGameConfig;
        private DashConfig _current;

        public DashParser(AllGameConfig all)
        {
            _allGameConfig = all;
            _allGameConfig.DashStats = new List<DashConfig>();
        }

        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "Id":
                    _current = new DashConfig
                    {
                        Id = token,
                    };
                    _allGameConfig.DashStats.Add(_current);
                    break;

                case "Duration":
                    _current.Duration = ParseFloat(token);
                    break;

                case "Distance":
                    _current.Distance = ParseInt(token);
                    break;
                case "Damage":
                    _current.Damage = ParseInt(token);
                    break;
            }
        }

        private int ParseInt(string t) => int.TryParse(t, out var v) ? v : 0;
        private float ParseFloat(string t) => float.TryParse(t, out var v) ? v : 0f;

        public void ApplyToSO()
        {
            const string featuresPath = "Assets/Features";
            const string heroPath = "Assets/Features/Hero";
            const string heroMovePath = "Assets/Features/Hero/HeroMove";
            const string resourcesPath = "Assets/Features/Hero/HeroMove/Resources";
            const string configPath = "Assets/Features/Hero/HeroMove/Resources/Config";
            const string baseDashPath = "Assets/Features/Hero/HeroMove/Resources/Config/BaseDash";

            CreateFolderIfNotExists("Assets", "Features");
            CreateFolderIfNotExists(featuresPath, "Hero");
            CreateFolderIfNotExists(heroPath, "HeroMove");
            CreateFolderIfNotExists(heroMovePath, "Resources");
            CreateFolderIfNotExists(resourcesPath, "Config");
            CreateFolderIfNotExists(configPath, "BaseDash");

            foreach (var cfg in _allGameConfig.DashStats)
            {
                string assetPath = $"{baseDashPath}/{cfg.Id}.asset";

                var so = AssetDatabase.LoadAssetAtPath<DashData>(assetPath);

                if (so == null)
                {
                    so = ScriptableObject.CreateInstance<DashData>();
                    AssetDatabase.CreateAsset(so, assetPath);
                }

                so.Distance = cfg.Distance;
                so.Duration = cfg.Duration;
                so.Damage = cfg.Damage;

                EditorUtility.SetDirty(so);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void CreateFolderIfNotExists(string parent, string folder)
        {
            string fullPath = $"{parent}/{folder}";
            if (!AssetDatabase.IsValidFolder(fullPath))
            {
                AssetDatabase.CreateFolder(parent, folder);
            }
        }



    }
}