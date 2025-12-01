using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Features.GoogleSheets
{
    public static class ConfigImportsMenu
    {
        private static string spreadsheetId = "1B-nYVnmV_UKYShUIgmLXGMUo63LR0XOzwpcAmj_BTqo";
        private static List<string> itemsSheetsName;
        private static string credentialsPath = "spellbook3d-546f90c63edf.json";
        private static string settingsFileName = "GameSettings";
        
        [MenuItem("GoogleSheets/Import All Configs")]
        private static async void LoadItemsSettings()
        {
            var sheetsImporter = new GoogleSheetsImporter(credentialsPath, spreadsheetId);

            itemsSheetsName = await sheetsImporter.GetSheetNames();

            var gameSetting = LoadSettings();
            
            foreach (var sheet in itemsSheetsName)
            {
                IGoggleSheetsParser parser;
                switch (sheet)
                {
                    case "StatsMinion":
                        parser = new StatsMinionParser(gameSetting);
                        break;
                    case "StatsSpell":
                        parser = new StatsSpellParser(gameSetting);
                        break;
                    default:
                        Debug.LogWarning($"No parser for sheet: {sheet}");
                        continue;
                }
                
                PlayerPrefs.SetString(settingsFileName, JsonUtility.ToJson(gameSetting));
                PlayerPrefs.Save();
                
                await sheetsImporter.DownloadAndParseSheet(sheet, parser);
            }
        }
        
        
        private static AllGameConfig LoadSettings()
        {
            var jsonLoaded = PlayerPrefs.GetString(settingsFileName);
            var gameSettings = !string.IsNullOrEmpty(jsonLoaded)
                ? JsonUtility.FromJson<AllGameConfig>(jsonLoaded)
                : new AllGameConfig();
            
            return gameSettings;
        }
    }
}