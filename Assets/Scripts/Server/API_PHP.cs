using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;


public static class Encryptor
{
    public static string MD5Hash(string text)
    {
        MD5 md5 = new MD5CryptoServiceProvider();

        //compute hash from the bytes of text
        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

        //get hash result after compute it
        byte[] result = md5.Hash;

        StringBuilder strBuilder = new StringBuilder();
        for (int i = 0; i < result.Length; i++)
        {
            //change it into 2 hexadecimal digits
            //for each byte
            strBuilder.Append(result[i].ToString("x2"));
        }

        return strBuilder.ToString();
    }
}

public class API_PHP
{
    public static String GetTimestamp(DateTime value)
    {
        return value.ToString("yyyyMMddHHmmssffff");
    }

    private static string ExportJsonToPost(DataPostModel dataPost)
    {
        return JsonUtility.ToJson(dataPost);
    }

    public static IEnumerator PosDataEvent(DataPostModel dataPost)
    {
        string url = Config.LINK_SERVER;
        // string url = "";
        WWWForm post = new WWWForm();
        var jsonData = ExportJsonToPost(dataPost);
       // Debug.Log("jsonData " + jsonData);
        post.AddField("content", jsonData);
        WWW Post = new WWW(url, post);
        yield return Post;
        Debug.Log("Result: " + Post.text);
        if (Post.text != "")
        {
            var dataRespone = JsonUtility.FromJson<DataPost>(Post.text);
        }
    }

    public static IEnumerator PosDataUser()
    {
        string url = Config.LINK_SERVER_USER_DATA;
        // string url = "";
        WWWForm post = new WWWForm();

        DataUser dataUser = new DataUser();
        dataUser.ID_User = SystemInfo.deviceUniqueIdentifier;
        dataUser.version = Config.versionName;
#if UNITY_ANDROID
        dataUser.os = "android";
#elif UNITY_IOS
         dataUser.os = "ios";
#elif UNITY_EDITOR
        dataUser.os = "editor_unity";
#endif
        dataUser.date = UnbiasedTime.Instance.Now.ToString();
        dataUser.phone_name = SystemInfo.deviceModel;
        dataUser.id_games = Config.ID_GAME;

        var jsonData = JsonUtility.ToJson(dataUser);
        post.AddField("content", jsonData);
        WWW Post = new WWW(url, post);
        yield return Post;
        if (Post.text != "")
        {
            var dataRespone = JsonUtility.FromJson<DataPost>(Post.text);
        }
    }
}

[System.Serializable]
public class DataUser
{
    public string ID_User;
    public string version;
    public string os;
    public string date;
    public string phone_name;
    public int id_games;
}

[System.Serializable]
public class DataPost
{
    public int status;
    public DataPostModel data;
}
                       
[System.Serializable]
public class DataPostModel
{
    public int id_games;
    public string ID_User;
    public string version;
    public string os;
    public string date;

    public List<EventActionClick> Data_Action_Click;
    public List<EventTimePlay> Data_Time_Play;
    public List<EventHintLevel> Data_Hint_level;
    public List<EventAddHint> Data_Add_Hint;
}


[System.Serializable]
public class EventActionClick
{
    public int id_action_click;
    public string name_action_click;
    public int num_turn;
}
[System.Serializable]
public class EventTimePlay
{
    public int id_level;
    public string name_level;
    public float time;
}

[System.Serializable]
public class EventHintLevel
{
    public int level;
    public int id_reason_use_hint;
    public string name_reason_use_hint;
    public int value_count;
    public int numTurn;
}

[System.Serializable]
public class EventAddHint
{
    public int id_reason_add_hint;
    public string name_reason_add_hint;
    public float value_count;
}
