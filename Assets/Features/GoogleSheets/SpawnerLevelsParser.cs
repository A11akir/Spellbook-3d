using System.Collections.Generic;
using Features.Enemy.EnemySpawner;
using Features.Spells;
using UnityEditor;
using UnityEngine;

namespace Features.GoogleSheets
{
    public class SpawnerLevelsParser : IGoggleSheetsParser
    {
        private readonly AllGameConfig _allGameConfig;
        private SpawnersConfig _current;

        public SpawnerLevelsParser(AllGameConfig all)
        {
            _allGameConfig = all;
            _allGameConfig.SpawnersStats = new List<SpawnersConfig>();
        }

        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "Level":
                    _current = new SpawnersConfig
                    {
                        Level = token,
                    };
                    _allGameConfig.SpawnersStats.Add(_current);
                    break;

                case "CountMeleeSpawners":
                    _current.CountMeleeSpawners = ParseInt(token);
                    break;

                case "CountRangeSpawners":
                    _current.CountRangeSpawners = ParseInt(token);
                    break;

                case "CountGromillaSpawners":
                    _current.CountGromillaSpawners = ParseInt(token);
                    break;

                case "CountSpawners":
                    _current.CountSpawners = ParseInt(token);
                    break;

                case "SpawnInterval":
                    _current.SpawnInterval = ParseInt(token);
                    break;

                case "SpawnDelay":
                    _current.SpawnDelay = ParseFloat(token);
                    break;
            }
        }

        private int ParseInt(string t) => int.TryParse(t, out var v) ? v : 0;
        private float ParseFloat(string t) => float.TryParse(t, out var v) ? v : 0f;

        public void ApplyToSO()
        {
            string basePath = "Assets/Features/Enemy/EnemySpawners";
            string configPath = $"{basePath}/Resources";
            string spawnersFolder = $"{configPath}/Config";
            string spawnersPath = $"{spawnersFolder}/Spawners";

            if (!AssetDatabase.IsValidFolder(basePath))
                AssetDatabase.CreateFolder("Assets/Features", "EnemySpawners");

            if (!AssetDatabase.IsValidFolder(configPath))
                AssetDatabase.CreateFolder(basePath, "Resources");

            if (!AssetDatabase.IsValidFolder(spawnersFolder))
                AssetDatabase.CreateFolder(configPath, "Config");

            if (!AssetDatabase.IsValidFolder(spawnersPath))
                AssetDatabase.CreateFolder(spawnersFolder, "Spawners");

            foreach (var cfg in _allGameConfig.SpawnersStats)
            {
                string path = $"{spawnersPath}/{cfg.Level}.asset";

                var so = AssetDatabase.LoadAssetAtPath<SpawnerConfigData>(path);

                if (so == null)
                {
                    so = ScriptableObject.CreateInstance<SpawnerConfigData>();
                    AssetDatabase.CreateAsset(so, path);
                }

                so.Level = cfg.Level;
                so.CountMeleeSpawners = cfg.CountMeleeSpawners;
                so.CountRangeSpawners = cfg.CountRangeSpawners;
                so.CountGromillaSpawners = cfg.CountGromillaSpawners;
                so.CountSpawners = cfg.CountSpawners;
                so.SpawnInterval = cfg.SpawnInterval;
                so.SpawnDelay = cfg.SpawnDelay;

                EditorUtility.SetDirty(so);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

    }
}