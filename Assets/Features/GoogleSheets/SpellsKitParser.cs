using System.Collections.Generic;
using Features.Spells;
using UnityEditor;
using UnityEngine;

namespace Features.GoogleSheets
{
    public class SpellsKitParser : IGoggleSheetsParser
    {
        private readonly AllGameConfig _allGameConfig;
        private SpellsKitConfig _current;

        public SpellsKitParser(AllGameConfig all)
        {
            _allGameConfig = all;
            _allGameConfig.SpellsKit = new List<SpellsKitConfig>();
        }

        public void Parse(string header, string token)
        {
            switch (header)
            {
                case "Id":
                    _current = new SpellsKitConfig
                    {
                        Id = token,
                        SpellsKit = new List<string>()
                    };
                    _allGameConfig.SpellsKit.Add(_current);
                    break;

                case "Spell":
                    if (!string.IsNullOrWhiteSpace(token))
                        _current.SpellsKit.Add(token);
                    break;
            }
        }

        public void ApplyToSO()
        {
            string basePath = "Assets/Features/Spells";
            string configPath = $"{basePath}/Resources";
            string spellsFolder = $"{configPath}/Config";
            string spellsKitPath = $"{spellsFolder}/SpellsKit";

            if (!AssetDatabase.IsValidFolder(basePath))
                AssetDatabase.CreateFolder("Assets/Features", "Spells");

            if (!AssetDatabase.IsValidFolder(configPath))
                AssetDatabase.CreateFolder(basePath, "Resources");

            if (!AssetDatabase.IsValidFolder(spellsFolder))
                AssetDatabase.CreateFolder(configPath, "Config");

            if (!AssetDatabase.IsValidFolder(spellsKitPath))
                AssetDatabase.CreateFolder(spellsFolder, "SpellsKit");

            foreach (var cfg in _allGameConfig.SpellsKit)
            {
                string path = $"{spellsKitPath}/{cfg.Id}.asset";
                var so = AssetDatabase.LoadAssetAtPath<SpellsKitData>(path);

                if (so == null)
                {
                    so = ScriptableObject.CreateInstance<SpellsKitData>();
                    AssetDatabase.CreateAsset(so, path);
                }

                so.SpellsKit = cfg.SpellsKit;

                EditorUtility.SetDirty(so);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
