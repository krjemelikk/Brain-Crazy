using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Collections;

//===============================================================
//Developer:  CuongCT
//Company:    ONESOFT
//Date:       2017
//================================================================
[CanEditMultipleObjects]
[CustomEditor(typeof(UILocalize), true)]
public class UILocalizeEditor : Editor
{
    static List<string> mKeys;

    void OnEnable() {
        ReloadDictionary();
    }

    private static void ReloadDictionary() {
        var dict = Localization.dictionary;

        if (dict.Count > 0) {
            mKeys = new List<string>();

            foreach (var pair in dict) {
                if (pair.Key == "KEY") continue;
                mKeys.Add(pair.Key);
            }

            mKeys.Sort((left, right) => String.Compare(left, right, StringComparison.Ordinal));
        }
    }

    static IEnumerator testRoutine()
    {
        //string url = "https://docs.google.com/spreadsheets/d/16mLAT8_u2_FuPTp93H9JLC3b4sPFtOqvSXZigblFC1A/export?format=csv&gid=345164090";
        //var url = "https://docs.google.com/spreadsheets/d/16mLAT8_u2_FuPTp93H9JLC3b4sPFtOqvSXZigblFC1A/export?format=csv&id=16mLAT8_u2_FuPTp93H9JLC3b4sPFtOqvSXZigblFC1A&gid=345164090";
       
        var url = "https://docs.google.com/spreadsheets/d/1B2HQWodSkq9Dn6_E8kNBetNUou6r-2NvIzCnU59YH7I/export?format=csv&gid=0";
        var www = new WWW(url);
        float time = 0;
        while (!www.isDone)
        {
            time += 0.001f;
            if (time > 10000)
            {
                yield return null;
                //Debug.Log("Downloading...");

                time = 0;
            }
        }
        if (www.isDone && string.IsNullOrEmpty(www.error))
        {
            var outputFile = Path.Combine(Application.dataPath, "Resources/Localization.txt");
            string data = www.text;
            data = www.text.Replace("\n\n\"","\"").Replace("\n\"","\"");
            File.WriteAllText(outputFile, data);
        }

        yield return null;
        EditorUtility.DisplayDialog("Notify", "Sync Data from google Success!", "OK");
        AssetDatabase.SaveAssets ();
        AssetDatabase.Refresh();
        Localization.Reload();
        ParseArabicData();
        ReloadDictionary();
    }

    [MenuItem("Tools/Localize/Reload")]
    public static void ReloadData()
    {
        EditorCoroutine.start(testRoutine());
    }

    [MenuItem("Tools/Localize/Abric")]
    public static void ReloadAbricData()
    {
        Localization.Reload();
        ParseArabicData();
        ReloadDictionary();
    }


    //[MenuItem("Tools/Localize/ParseArabic")]
    public static void ParseArabicData()
    {
        var lang = Resources.Load<TextAsset>("Localization");
        var outputFile = Path.Combine(Application.dataPath, "Resources/Localization.txt");
        var outputData = LoadCSV(lang.bytes);
        using (var file = new StreamWriter(outputFile)) {
            foreach (var item in outputData) {
                for (var i = 0; i < item.Length; i++) {
                    file.Write(item[i] + ",");
                               // Debug.Log(item[i] + ",");
                }
                file.Write(Environment.NewLine);
            }
            file.Flush();
            file.Close();
        }        
        EditorUtility.DisplayDialog("title", "Parse Done", "OK");
    }

    private static List<string[]> LoadCSV(byte[] bytes) {
        var reader = new ByteReader(bytes);
        var outputData = new List<string[]>();
        // The first line should contain "KEY", followed by languages.
        var header = reader.ReadCSV();
        string[] headeData = new string[header.size];
        for (int i = 0; i < header.size; i++) {
            headeData[i] = header[i];
        }
        outputData.Add(headeData);
        int arabicInex;
        for (arabicInex = 0; arabicInex < header.size; arabicInex++) {
            if (string.CompareOrdinal(header[arabicInex], SystemLanguage.Arabic.ToString()) == 0)
                break;
            if (arabicInex == header.size-1)
                return null;
        }
        while(true){
            var temp = reader.ReadCSV();
            if (temp == null || temp.size == 0) break;
            if (string.IsNullOrEmpty(temp[0])) continue;
            /*            
            Debug.Log(temp[0]);
            Debug.LogFormat("{0} {1}"
                , temp[1].PadRight(100)
                , ArabicSupport.ArabicFixer.Fix(temp[arabicInex], true, false).Replace('>', '(')
                    .Replace('<', ')')
                    .Replace('(', '<').Replace(')', '>'));
                    */
            string[] data = new string[temp.size];
            for (int i = 0; i < temp.size; i++) {
                data[i] = i == arabicInex
                    ? ArabicSupport.ArabicFixer.Fix(temp[arabicInex], true, false).Replace('>', '(')
                        .Replace('<', ')')
                        .Replace('(', '<').Replace(')', '>')
                    : temp[i];
                data[i] = string.Format("\"{0}\"",data[i].Replace("\n", @"\n"));
            }
            outputData.Add(data);
        }
        return outputData;
    }

    public override void OnInspectorGUI()
    {

        if (GUILayout.Button("Reload"))
        {
            //EditorCoroutine.start(testRoutine());
            ReloadDictionary();

        }


        serializedObject.Update();

        GUILayout.Space(6f);
        LocalizeEditorTools.SetLabelWidth(80f);

        GUILayout.BeginHorizontal();
        // Key not found in the localization file -- draw it as a text field
        var sp = LocalizeEditorTools.DrawProperty("Key", serializedObject, "key");

        var myKey = sp.stringValue;
        var isPresent = (mKeys != null) && mKeys.Contains(myKey);
        GUI.color = isPresent ? Color.green : Color.red;
        GUILayout.BeginVertical(GUILayout.Width(22f));
        GUILayout.Space(2f);
        GUILayout.Label(isPresent ? "\u2714" : "\u2718", "TL SelectionButtonNew", GUILayout.Height(20f));
        GUILayout.EndVertical();
        GUI.color = Color.white;
        GUILayout.EndHorizontal();

        if (isPresent)
        {
            if (LocalizeEditorTools.DrawHeader("Preview"))
            {
                LocalizeEditorTools.BeginContents();

                var keys = Localization.knownLanguages;
                string[] values;

                if (Localization.dictionary.TryGetValue(myKey, out values))
                {
                    if (keys.Length != values.Length)
                    {
                        EditorGUILayout.HelpBox("Number of keys doesn't match the number of values! Did you modify the dictionaries by hand at some point?", MessageType.Error);
                    }
                    else
                    {
                        for (var i = 0; i < keys.Length; ++i)
                        {
                            GUILayout.BeginHorizontal();
                            GUILayout.Label(keys[i], GUILayout.Width(66f));

                            if (GUILayout.Button(values[i], "AS TextArea", GUILayout.MinWidth(80f), GUILayout.MaxWidth(Screen.width - 110f)))
                            {
                                ((UILocalize) target).value = values[i];
                                GUIUtility.hotControl = 0;
                                GUIUtility.keyboardControl = 0;
                            }
                            GUILayout.EndHorizontal();
                        }
                    }
                }
                else GUILayout.Label("No preview available");

                LocalizeEditorTools.EndContents();
            }
        }
        else if (mKeys != null && !string.IsNullOrEmpty(myKey))
        {
            GUILayout.BeginHorizontal();
            GUILayout.Space(80f);
            GUILayout.BeginVertical();
            GUI.backgroundColor = new Color(1f, 1f, 1f, 0.35f);

            var matches = 0;

            for (int i = 0, imax = mKeys.Count; i < imax; ++i)
            {
                if (mKeys[i].StartsWith(myKey, StringComparison.OrdinalIgnoreCase) || mKeys[i].Contains(myKey))
                {
                    if (GUILayout.Button(mKeys[i] + " \u25B2", "CN CountBadge"))
                    {
                        sp.stringValue = mKeys[i];
                        GUIUtility.hotControl = 0;
                        GUIUtility.keyboardControl = 0;
                    }

                    if (++matches == 8)
                    {
                        GUILayout.Label("...and more");
                        break;
                    }
                }
            }
            GUI.backgroundColor = Color.white;
            GUILayout.EndVertical();
            GUILayout.Space(22f);
            GUILayout.EndHorizontal();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
public class EditorCoroutine
{
    public static EditorCoroutine start(IEnumerator _routine)
    {
        var coroutine = new EditorCoroutine(_routine);
        coroutine.start();
        return coroutine;
    }

    readonly IEnumerator routine;
    EditorCoroutine(IEnumerator _routine)
    {
        routine = _routine;
    }

    void start()
    {
        //Debug.Log("start");
        EditorApplication.update += update;
    }
    public void stop()
    {
        //Debug.Log("stop");
        EditorApplication.update -= update;
    }

    void update()
    {
        /* NOTE: no need to try/catch MoveNext,
         * if an IEnumerator throws its next iteration returns false.
         * Also, Unity probably catches when calling EditorApplication.update.
         */

        //Debug.Log("update");
        if (!routine.MoveNext())
        {
            stop();
        }
    }
}
