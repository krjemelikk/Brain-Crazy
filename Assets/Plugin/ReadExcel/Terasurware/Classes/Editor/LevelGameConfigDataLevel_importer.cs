using UnityEngine;
using System.IO;
using UnityEditor;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

public class LevelGameConfigDataLevel_importer : AssetPostprocessor
{
    private static readonly string filePath = "Assets/Excel/LevelGameConfig.xlsx";
    private static readonly string[] sheetNames = { "Sheet1", };

    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets)
        {
            if (!filePath.Equals(asset))
                continue;

            using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                IWorkbook book = null;
                if (Path.GetExtension(filePath) == ".xls")
                {
                    book = new HSSFWorkbook(stream);
                }
                else
                {
                    book = new XSSFWorkbook(stream);
                }

                foreach (string sheetName in sheetNames)
                {
                    // check sheet
                    var sheet = book.GetSheet(sheetName);
                    if (sheet == null)
                    {
                        Debug.LogError("[QuestData] sheet not found:" + sheetName);
                        continue;
                    }
                    
                    var exportPath = "Assets/Scripts/ScriptableObject/DataLevels.asset";
                    if (!Directory.Exists("Assets/Scripts/ScriptableObject/"))
                    {
                        Directory.CreateDirectory("Assets/Scripts/ScriptableObject/");
                    }
                    // check scriptable object
                    var data = (DataLevels)AssetDatabase.LoadAssetAtPath(exportPath, typeof(DataLevels));
                    if (data == null)
                    {
                        data = ScriptableObject.CreateInstance<DataLevels>();
                        AssetDatabase.CreateAsset((ScriptableObject)data, exportPath);
                    }

                    data.hideFlags = HideFlags.None;
                    data.lsLevel = new System.Collections.Generic.List<DataLevel>();
                    data.lsLevel.Clear();
                    var p = data;

                    // add infomation
                    for (int i = 1; i < sheet.GetSheetLength() + 1; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        ICell cell = null;
                        cell = row.GetCell(1);
                        string _name = cell.ToString();
                        if (string.IsNullOrEmpty(_name))
                        {
                            _name = sheetName + i;
                        }
                       
                        var level = new DataLevel();

                        cell = row.GetCell(0); level.ID = cell.TryGetCell<int>();
                        cell = row.GetCell(1); level.NAME_LEVEL = (cell == null ? "" : cell.StringCellValue);

                        p.lsLevel.Add(level);
                    }

                    Debug.Log("Done!!!");

                    ScriptableObject obj = AssetDatabase.LoadAssetAtPath(exportPath, typeof(ScriptableObject)) as ScriptableObject;
                    EditorUtility.SetDirty(obj);
                }
            }

        }
    }
}
