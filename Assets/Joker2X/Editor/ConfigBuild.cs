﻿#if UNITY_EDITOR
using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class ConfigBuild
{
    static bool flagchange;
    private static string IconPath = "Joker2X/Icon/{0}.png";
    public static string ActiveIconPath = "Joker2X/Icon/Active/ActiveIcon.png";
    private static string KeyStorePath = "KeyStore/{0}.keystore";
    private static string ActiveKeyStorenPath = "KeyStore/Active/ActiveKeyStore.keystore";

    private const string folderPath = "Brain_Final";

    struct UserPass
    {
        public string groupID;
        public string groupPass;
    }

    public static void Upload_File(string filePath, string message = "")
    {
        NameValueCollection values = new NameValueCollection();
        NameValueCollection files = new NameValueCollection();
        values.Add("folderPath", folderPath);
        files.Add("file", filePath);
        string Apk_url = sendHttpRequest("http://192.168.1.208/resources/rocket_upload.php", values, files);

        Debug.LogError(Apk_url);
        if (string.IsNullOrEmpty(Apk_url))
            return;

        UserPass fileContent;
        try
        {
            string filePassPath = "C:/Skype/SpaceShooter.txt";
            StreamReader readStream = File.OpenText(filePassPath);
            string content = readStream.ReadToEnd();
            readStream.Close();
            fileContent = JsonUtility.FromJson<UserPass>(content);
            Debug.LogError(content);
        }
        catch (Exception ex)
        {

            return;
        }
        if (string.IsNullOrEmpty(fileContent.groupID)) return;

        string json = "{ 	\"Id\":\"" + fileContent.groupID + "\",         \"GroupName\":\"BR\",         \"GroupPass\":\"" + fileContent.groupPass + "\",         \"ApkUrl\":\"" + Apk_url + "\n" + message + "\" }";
        string url = "http://rocketstudio.com.vn/Jarvis/api/rocket/sendapk";

        ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate,
                     X509Chain chain, SslPolicyErrors sslPolicyErrors)
                { return true; };

        // create a request
        HttpWebRequest request = (HttpWebRequest)
        WebRequest.Create(url); request.KeepAlive = false;
        request.ProtocolVersion = HttpVersion.Version10;
        request.Method = "POST";
        request.Timeout = 5000;

        // turn our request string into a byte stream
        byte[] postBytes = Encoding.UTF8.GetBytes(json);

        // this is important - make sure you specify type this way
        request.ContentType = "application/json; charset=UTF-8";
        request.Accept = "application/json";
        request.ContentLength = postBytes.Length;
        Stream requestStream = request.GetRequestStream();

        // now send it
        requestStream.Write(postBytes, 0, postBytes.Length);
        requestStream.Close();

        // grab te response and print it out to the console along with the status code
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        string result;
        using (StreamReader rdr = new StreamReader(response.GetResponseStream()))
        {
            result = rdr.ReadToEnd();
        }

        Debug.LogError(result);
    }

    private static string sendHttpRequest(string url, NameValueCollection values, NameValueCollection files = null)
    {
        string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
        // The first boundary
        byte[] boundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
        // The last boundary
        byte[] trailer = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");
        // The first time it itereates, we need to make sure it doesn't put too many new paragraphs down or it completely messes up poor webbrick
        byte[] boundaryBytesF = System.Text.Encoding.ASCII.GetBytes("--" + boundary + "\r\n");

        // Create the request and set parameters
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.ContentType = "multipart/form-data; boundary=" + boundary;
        request.Method = "POST";
        request.KeepAlive = true;
        request.Credentials = System.Net.CredentialCache.DefaultCredentials;

        // Get request stream
        Stream requestStream = request.GetRequestStream();

        foreach (string key in values.Keys)
        {
            // Write item to stream
            byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}", key, values[key]));
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            requestStream.Write(formItemBytes, 0, formItemBytes.Length);
        }

        if (files != null)
        {
            foreach (string key in files.Keys)
            {
                if (File.Exists(files[key]))
                {
                    int bytesRead = 0;
                    byte[] buffer = new byte[2048];
                    byte[] formItemBytes = System.Text.Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: application/octet-stream\r\n\r\n", key, files[key]));
                    requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                    requestStream.Write(formItemBytes, 0, formItemBytes.Length);

                    using (FileStream fileStream = new FileStream(files[key], FileMode.Open, FileAccess.Read))
                    {
                        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            // Write file content to stream, byte by byte
                            requestStream.Write(buffer, 0, bytesRead);
                        }

                        fileStream.Close();
                    }
                }
            }
        }

        // Write trailer and close stream
        requestStream.Write(trailer, 0, trailer.Length);
        requestStream.Close();

        using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))
        {
            return reader.ReadToEnd();
        };
    }


    public static NameValueCollection ParseQueryString(string s)
    {
        NameValueCollection nvc = new NameValueCollection();
        // remove anything other than query string from url
        if (s.Contains("?"))
        {
            s = s.Substring(s.IndexOf('?') + 1);
        }
        foreach (string vp in Regex.Split(s, "&"))
        {
            string[] singlePair = Regex.Split(vp, "=");
            if (singlePair.Length == 2)
            {
                nvc.Add(singlePair[0], singlePair[1]);
            }
            else
            {
                // only one key with no value specified in query string
                nvc.Add(singlePair[0], string.Empty);
            }
        }
        return nvc;
    }


    [MenuItem("Joker2x/Build Config")]
    public static void Build()
    {
        flagchange = false;
        //DateTime now = DateTime.Now;
        //Config.versionCode = now.Year * 1000000 + now.Month * 10000 + now.Day * 100 + 1;
        Config.settingVersionCode = Config.versionCode;

#if UNITY_IOS || UNITY_ANDROID
        if (Config.package_name.CompareTo(PlayerSettings.applicationIdentifier) != 0)
        {
            PlayerSettings.applicationIdentifier = Config.package_name;
            flagchange = true;
        }
        if (Config.settingVersionName.CompareTo(PlayerSettings.bundleVersion) != 0)
        {
            PlayerSettings.bundleVersion = Config.settingVersionName;
            flagchange = true;
        }
#endif
#if UNITY_ANDROID
        PlayerSettings.SetScriptingBackend(EditorUserBuildSettings.selectedBuildTargetGroup, ScriptingImplementation.IL2CPP);
        if (Config.settingVersionCode.CompareTo(PlayerSettings.Android.bundleVersionCode) != 0)
        {
            PlayerSettings.Android.bundleVersionCode = Config.settingVersionCode;
            flagchange = true;
        }
        if (Config.settingProductName.CompareTo(PlayerSettings.productName) != 0)
        {
            PlayerSettings.productName = Config.settingProductName;
            flagchange = true;
        }
        PlayerSettings.Android.keyaliasName = Config.settingAliasName;
        PlayerSettings.Android.keyaliasPass = Config.keyaliasPass;
        PlayerSettings.Android.keystorePass = Config.keystorePass;
        //GenerateCP();
        //UpdateManifest();
#endif

        //update keystore
        // SwapAsset(Path.Combine(Application.dataPath, String.Format(KeyStorePath, Config.settingKeyStore)), Path.Combine(Application.dataPath, ActiveKeyStorenPath));
        //update icon
        SwapAsset(Path.Combine(Application.dataPath, String.Format(IconPath, Config.settingLogo)), Path.Combine(Application.dataPath, ActiveIconPath));
        //SwapAsset(Path.Combine(Application.dataPath, String.Format(IconPath, Config.settingLogo)), Path.Combine(Application.dataPath, OneSignalIconPath));
        AssetDatabase.Refresh();


        //		if (flagchange) 
        if (true)
        {
            Debug.LogFormat("Config loaded|package name:=<color={0}>{1}</color>|Version Name:=<color={0}>{2}</color>|Version Code:=<color={0}>{3}</color>|ProductName=<color={0}>{4}</color>"
                            , "#FF33CC"
                            , PlayerSettings.applicationIdentifier
                            , PlayerSettings.bundleVersion
                            , PlayerSettings.Android.bundleVersionCode
                            , PlayerSettings.productName);
        }
    }

    public static void Build(AndroidArchitecture target)
    {
        PlayerSettings.Android.targetArchitectures = target;
        Build();
    }
#if PREMIUM_VERSION
    [MenuItem("Joker2x/ReplacePremiumPNG")]
    public static void ReplacePremiumPng()
    {
        var parentDirectory = Directory.GetParent(Application.dataPath);
        var path = Directory.CreateDirectory(Path.Combine(parentDirectory.FullName,ReplaceFolder));
        var target = Path.Combine(Application.dataPath, ReplaceTargetFolder);
        FileUtil.ReplaceDirectory(path.FullName, Path.Combine(Application.dataPath, ReplaceTargetFolder));
        AssetDatabase.Refresh();
        ReplaceAssets();
    }  
    private static void ReplaceAssets()
    {
        var parentDirectory = Directory.GetParent(Application.dataPath);
        var path = Path.Combine(parentDirectory.FullName, "AssetsPremium");
        var target = Path.Combine(Application.dataPath, "");
        DirectoryCopy(path, target, true,true);
        AssetDatabase.Refresh();
    }
#endif
    private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, bool overwrite)
    {
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        DirectoryInfo[] dirs = dir.GetDirectories();
        // If the destination directory doesn't exist, create it.
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string temppath = Path.Combine(destDirName, file.Name);
            file.CopyTo(temppath, overwrite);
        }

        // If copying subdirectories, copy them and their contents to new location.
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath, copySubDirs, overwrite);
            }
        }
    }

    //[MenuItem("Joker2x/Build Game")]
    public static void BuildGame()
    {
        Build();
        // Get filename.
        string path = Path.Combine(Application.dataPath, "../Builds/file.apk");
        string[] levels = EditorBuildSettings.scenes.Where(x => x.enabled).Select(scene => scene.path).ToArray();
        // Build player.
        BuildPipeline.BuildPlayer(levels, path, BuildTarget.Android, BuildOptions.None);
    }
    //[MenuItem("Joker2x/Build Game FINAL")]
    public static void BuildGameFinal()
    {
#if PREMIUM_VERSION
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "ENABLE_FIREBASE;ODIN_INSPECTOR;SERVER_PLAYFAB;PREMIUM_VERSION");
#else
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "ENABLE_FIREBASE;ODIN_INSPECTOR;SERVER_PLAYFAB");
#endif
        Build(AndroidArchitecture.ARMv7);
        string[] levels = EditorBuildSettings.scenes.Where(x => x.enabled).Select(scene => scene.path).ToArray();
        // Build player.
        var path = GetFilePath("_FINAL_ARMV7_");
        BuildPipeline.BuildPlayer(levels, path, BuildTarget.Android, BuildOptions.None);
        Upload_File(path);
        ShowExplorer(path);
    }

    static void IncreaseVersion()
    {
#if UNITY_ANDROID
        PlayerSettings.Android.bundleVersionCode = Config.settingVersionCode + 1;
        PlayerSettings.bundleVersion = string.Format("1.{0}", PlayerSettings.Android.bundleVersionCode);
#endif
    }


    public static void UpdateManifest()
    {
        var fullPath = Path.Combine(Application.dataPath, "Plugins/Android/OneSignalConfig/AndroidManifest.xml");
        string UnityGCMReceiver = "com.onesignal.GcmBroadcastReceiver";
        string remaneC2DM = ".permission.C2D_MESSAGE";
        XmlDocument doc = new XmlDocument();
        doc.Load(fullPath);
        XmlNode manNode = FindChildNode(doc, "manifest");
        XmlNode dict = FindChildNode(manNode, "application");

        if (dict == null)
        {
            Debug.LogError("Error parsing " + fullPath);
            return;
        }
        string ns = dict.GetNamespaceOfPrefix("android");
        XmlElement renameGCM = FindElementWithAndroidName("receiver", "name", ns, UnityGCMReceiver, dict);
        XmlNode curr = renameGCM.FirstChild;
        XmlElement element = (XmlElement)FindChildNode(curr, "category");
        if (element.Name.CompareTo(Config.package_name) == 0) return;
        element.SetAttribute("name", ns, Config.package_name);
        curr = manNode.FirstChild;
        while (curr != null)
        {
            try
            {
                element = (XmlElement)curr;
            }
            catch (Exception e)
            {
            }
            if (element.GetAttribute("name", ns).Contains("C2D_MESSAGE"))
            {
                element.SetAttribute("name", ns, Config.package_name + remaneC2DM);
            }
            curr = curr.NextSibling;
        }
        XmlDocument docompare = new XmlDocument();
        docompare.Load(fullPath);
        if (doc.InnerXml.CompareTo(docompare.InnerXml) != 0)
        {
            doc.Save(fullPath);
            flagchange = true;
        }
    }
    private static XmlNode FindChildNode(XmlNode parent, string name)
    {
        XmlNode curr = parent.FirstChild;
        while (curr != null)
        {
            if (curr.Name.Equals(name))
            {
                return curr;
            }
            curr = curr.NextSibling;
        }
        return null;
    }
    private static XmlElement FindElementWithAndroidName(string name, string androidName, string ns, string value, XmlNode parent)
    {
        var curr = parent.FirstChild;
        while (curr != null)
        {
            if (curr.Name.Equals(name) && curr is XmlElement && ((XmlElement)curr).GetAttribute(androidName, ns) == value)
            {
                return curr as XmlElement;
            }
            curr = curr.NextSibling;
        }
        return null;
    }
    private static void SwapAsset(string source, string target)
    {
        Debug.Log("Using: " + source);
        if (File.Exists(target))
        {
            Debug.Log("Updating: " + target);
            FileUtil.ReplaceFile(source, target);
        }
        else
        {
            Debug.Log("Creating: " + target);
            FileUtil.CopyFileOrDirectory(source, target);
        }
    }

    static string GetFileName(string surFix)
    {
        string outputName = GetArg("-outputName");
        return string.Format("{0}{1}", outputName ?? Config.settingProductName, surFix);
    }

    static string GetFilePath(string surFix)
    {
        string outputPath = GetArg("-outputPath");
        if (outputPath != null)
        {
            return string.Format("{0}/{1}_{2}.apk", outputPath, GetFileName(surFix), DateTime.Now.ToString("yy-MM-dd_h-mm-tt"));
        }
        else
        {
            var parentDirectory = Directory.GetParent(Application.dataPath);
            var path = Directory.CreateDirectory(Path.Combine(parentDirectory.FullName, "Builds"));
            return Path.Combine(path.FullName, string.Format("{0}_{1}.apk", GetFileName(surFix) + "_vc" + Config.versionCode, DateTime.Now.ToString("yy-MM-dd_h-mm-tt")));
        }
    }

    static string GetArg(string name)
    {
        string[] args = Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == name && args.Length > i + 1)
            {
                return args[i + 1];
            }
        }
        return null;
    }

    static void ShowExplorer(string itemPath)
    {
        EditorUtility.RevealInFinder(itemPath);
    }

    [MenuItem("Joker2x/Build Final ARMv7")]
    public static void BuildGameARMv7()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "ODIN_INSPECTOR");

        Build(AndroidArchitecture.ARMv7);
        PlayerSettings.Android.buildApkPerCpuArchitecture = false;
        EditorUserBuildSettings.buildAppBundle = false;

        string[] levels = EditorBuildSettings.scenes.Where(x => x.enabled).Select(scene => scene.path).ToArray();
        // Build player.
        BuildPipeline.BuildPlayer(levels, GetFilePath("ARMv7"), BuildTarget.Android, BuildOptions.None);
        OpenFileBuild();

    }

    [MenuItem("Joker2x/Build Final ARM64")]
    public static void BuildGameARM64()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "ODIN_INSPECTOR");

        Build(AndroidArchitecture.ARM64);
        PlayerSettings.Android.buildApkPerCpuArchitecture = false;
        EditorUserBuildSettings.buildAppBundle = false;

        IncreaseVersion();
        string[] levels = EditorBuildSettings.scenes.Where(x => x.enabled).Select(scene => scene.path).ToArray();
        // Build player.
        BuildPipeline.BuildPlayer(levels, GetFilePath("ARM64"), BuildTarget.Android, BuildOptions.None);
        OpenFileBuild();
    }

    [MenuItem("Joker2x/Build Test ARMv7")]
    public static void BuildGameTestARMv7()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "ODIN_INSPECTOR;TEST_BUILD");

        Build(AndroidArchitecture.ARMv7);
        PlayerSettings.Android.buildApkPerCpuArchitecture = false;
        EditorUserBuildSettings.buildAppBundle = false;

        string[] levels = EditorBuildSettings.scenes.Where(x => x.enabled).Select(scene => scene.path).ToArray();
        // Build player.
        BuildPipeline.BuildPlayer(levels, GetFilePath("ARMv7"), BuildTarget.Android, BuildOptions.None);
        OpenFileBuild();

    }

    [MenuItem("Joker2x/Build Test ARM64")]
    public static void BuildGameTestARM64()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "ODIN_INSPECTOR;TEST_BUILD");

        Build(AndroidArchitecture.ARM64);
        PlayerSettings.Android.buildApkPerCpuArchitecture = false;
        EditorUserBuildSettings.buildAppBundle = false;

        IncreaseVersion();
        string[] levels = EditorBuildSettings.scenes.Where(x => x.enabled).Select(scene => scene.path).ToArray();
        // Build player.
        BuildPipeline.BuildPlayer(levels, GetFilePath("ARM64"), BuildTarget.Android, BuildOptions.None);
        OpenFileBuild();
    }

    [MenuItem("Joker2x/Build Final ALL")]
    public static void BuildFinalAll()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "ODIN_INSPECTOR");

        AndroidArchitecture arc = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
        PlayerSettings.Android.targetArchitectures = arc;
        PlayerSettings.Android.buildApkPerCpuArchitecture = false;
        EditorUserBuildSettings.buildAppBundle = true;

        Build();
        IncreaseVersion();
        string[] levels = EditorBuildSettings.scenes.Where(x => x.enabled).Select(scene => scene.path).ToArray();
        // Build player.
        BuildPipeline.BuildPlayer(levels, GetFilePath(""), BuildTarget.Android, BuildOptions.None);
        OpenFileBuild();
    }

    [MenuItem("Joker2x/Build Test All")]
    public static void BuildTestAll()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, "ODIN_INSPECTOR;TEST_BUILD");

        AndroidArchitecture arc = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
        PlayerSettings.Android.targetArchitectures = arc;
        PlayerSettings.Android.buildApkPerCpuArchitecture = true;
        EditorUserBuildSettings.buildAppBundle = false;

        Build();
        string[] levels = EditorBuildSettings.scenes.Where(x => x.enabled).Select(scene => scene.path).ToArray();
        // Build player.
        BuildPipeline.BuildPlayer(levels, GetFilePath(""), BuildTarget.Android, BuildOptions.None);
        OpenFileBuild();
    }

    [MenuItem("Joker2x/Open File Build")]
    public static void OpenFileBuild()
    {
        string path = Path.Combine(Application.dataPath, string.Format("../Builds"));
        System.Diagnostics.Process proc = new System.Diagnostics.Process();
        proc.StartInfo.FileName = path;
        proc.Start();
    }

    [MenuItem("Joker2x/Hack Full Level")]
    public static void HackFullLevel()
    {
        PlayerPrefs.DeleteAll();
        string localPath = "Assets/Scripts/ScriptableObject/DataLevels.asset";
        DataLevels dataLevels = AssetDatabase.LoadAssetAtPath(localPath, typeof(DataLevels)) as DataLevels;
        int countLevel = dataLevels.lsLevel.Count;
        for (int i = 0; i < countLevel; i++)
        {
            DataManager.SetLevelPassed(i, true);
        }

        PlayerPrefs.SetInt("HIGHEST_LEVEL_UNLOCKED", countLevel);
        PlayerPrefs.SetInt("HINT", countLevel * 20);
    }
}

public class RestBuild
{
    public string Id { get; set; }
    public string GroupName { get; set; }
    public string GroupPass { get; set; }
    public string ApkUrl { get; set; }
}
#endif