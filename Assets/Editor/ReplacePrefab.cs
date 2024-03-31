using UnityEngine;
using UnityEditor;
using System.IO;

public class ReplacePrefab : EditorWindow
{
    [MenuItem("Examples/Edit Level")]
    public static void EditLevel()
    {
        for (int i = 1; i <= 48; i++)
        {
            string localPath = "Assets/Resources/Levels/Level_" + i.ToString() + ".prefab";
            if (!Directory.Exists(localPath))
            {
                GameObject objLevel = AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)) as GameObject;
                BaseLevel level = objLevel.GetComponent<BaseLevel>();
                level.IDQuestion = level.ID;

                Debug.Log("IDQuestion_" + i);

                EditorUtility.SetDirty(level);

                // Save scenes + prefab instances
                EditorApplication.ExecuteMenuItem("File/Save");

                // Save dirty ScriptableOjects (.assets)
                AssetDatabase.SaveAssets();

                Debug.Log("[EditorSaveAll] Done!");
            }
        }
    }

    // Disable the menu item if no selection is in place
    [MenuItem("Examples/Create Prefab From Selected", true)]
    static bool ValidateCreatePrefab()
    {
        return Selection.activeGameObject != null;
    }

    static void CreateNew(GameObject obj, string localPath)
    {
        Object prefab = PrefabUtility.CreateEmptyPrefab(localPath);
        PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
    }
}
