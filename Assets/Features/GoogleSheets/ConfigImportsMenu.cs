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
        private static string credentialsPath = "spellbook3d-48835cb066bc.json";
        
        [MenuItem("GoogleSheets/Import All Configs")]
        private static async void LoadItemsSettings()
        {
            var sheetsImporter = new GoogleSheetsImporter(credentialsPath, spreadsheetId);

            itemsSheetsName = await sheetsImporter.GetSheetNames();

            var gameSetting = new AllGameConfig();
            
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
                
                await sheetsImporter.DownloadAndParseSheet(sheet, parser);
                
                parser.ApplyToSO();
            }
        }
        
    }
}