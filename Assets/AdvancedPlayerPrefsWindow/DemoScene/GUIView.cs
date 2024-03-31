using UnityEngine;

public class GUIView : MonoBehaviour
{
    private float progress = 0;
    private bool muted = false;
    private int score = 0;
    private string playername = "Noname";

    private const string PROGRESS_KEY = "Progress";
    private const string MUTED_KEY = "IsSoundMuted";
    private const string SCORE_KEY = "Highscore";
    private const string PLAYERNAME_KEY = "PlayerName";

    // Use this for initialization
    void Start()
    {
        RefreshData();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
            RefreshData();
    }

    void OnGUI()
    {
        int width = 450;
        int height = 400;
        GUILayout.BeginArea(new Rect(Screen.width / 2 - width / 2, Screen.height / 2 - height / 2, width, height));
        {
            GUILayout.BeginVertical();
            {
                string macRefresh = "Unfortunately, this will take a a few seconds. This is due Unity working different on a Mac :(";
                GUILayout.TextArea(string.Format("Instructions: {0} 1) Open the Advanced PlayerPrefs Window and dock it somewhere. {0} 2) Change the values in the scene using the gui widgets. {0} 3) Go back to the Advanced PlayerPrefs Window and click the refresh button. " + (Application.platform == RuntimePlatform.OSXEditor ? macRefresh : "") + " {0} 4) Observe that the values in the Advanced PlayerPrefs Window has changed to your scene input. {0}{0} 5) Now in the Advanced PlayerPrefs Window, change the values and save those changes {0} 6) Go give the scene focus by clicking in the sceneview. {0} 7) Watch the gui values update to your changes", System.Environment.NewLine));

                GUILayout.Space(12);

                //Progress bar
                GUILayout.Label("Progress: " + (int)progress + "%");
                float newProgress = GUILayout.HorizontalSlider(progress, 0, 100);
                if (!Mathf.Approximately(newProgress, progress))
                {
                    progress = newProgress;
                    SaveData();
                }

                GUILayout.Space(12);

                //Audio mute toggle
                bool newMuted = GUILayout.Toggle(muted, "Is Audio Muted?");
                if (newMuted != muted)
                {
                    muted = newMuted;
                    SaveData();
                }

                GUILayout.Space(12);

                //Score label
                GUILayout.Label("Highscore: " + score);

                GUILayout.Space(12);

                //Player Name label
                GUILayout.Label("Playername");
                string newPlayerName = GUILayout.TextField(playername);
                if(newPlayerName != playername)
                {
                    playername = newPlayerName;
                    SaveData();
                }

            }
            GUILayout.EndVertical();
        }
        GUILayout.EndArea();
    }

    public void RefreshData()
    {
        progress = PlayerPrefs.GetFloat(PROGRESS_KEY, 100);
        muted = PlayerPrefs.GetString(MUTED_KEY, "true") == "true"; //convert string to bool
        score = PlayerPrefs.GetInt(SCORE_KEY, 123);
        playername = PlayerPrefs.GetString(PLAYERNAME_KEY, "Noname");
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat(PROGRESS_KEY, progress);
        PlayerPrefs.SetString(MUTED_KEY, muted ? "true" : "false"); //convert bool to string
        PlayerPrefs.SetInt(SCORE_KEY, score);
        PlayerPrefs.SetString(PLAYERNAME_KEY, playername);
    }
}