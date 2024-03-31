using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ImageHintQuestion
{
    public int idLevel;
    public Sprite hintSpr;
    public float scale;
}

public class HintPanel : BaseBox
{
    private static HintPanel instance;

    public static HintPanel Setup(string strHint)
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load<HintPanel>(PathPrefabs.PANEL_HINT), GameController.Instance.HomeScene.tfPopupParent);
        }
        instance.Show();
        instance.InitData(strHint);
        return instance;
    }

    [SerializeField] private Text txtHintInfo;
    [SerializeField] private Image imgHintInfo;
    [SerializeField] private Button btOK;

    [SerializeField] private ImageHintQuestion[] imageHintQuestion;

    private string hintInfo;

    protected override void OnStart()
    {
        base.OnStart();

        btOK.onClick.AddListener(() => OnClickOK());
    }

    public void InitData(string _hintInfo)
    {
        hintInfo = _hintInfo;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (GameController.Instance.currentLevel.IDQuestion != 61 && GameController.Instance.currentLevel.IDQuestion != 60 && GameController.Instance.currentLevel.IDQuestion != 199)
        {
            imgHintInfo.gameObject.SetActive(false);
            txtHintInfo.gameObject.SetActive(true);
            txtHintInfo.text = hintInfo;
        }
        else
        {
            imgHintInfo.gameObject.SetActive(true);
            txtHintInfo.gameObject.SetActive(false);
            SetHintImg(GameController.Instance.currentLevel.IDQuestion);
        }
    }

    public void SetHintImg(int idLevel)
    {
        for (int i = 0; i < imageHintQuestion.Length; i++)
        {
            if (imageHintQuestion[i].idLevel == idLevel)
            {
                imgHintInfo.sprite = imageHintQuestion[i].hintSpr;
                imgHintInfo.gameObject.transform.localScale = Vector3.one * imageHintQuestion[i].scale;
            }
        }
    }

    private void OnClickOK()
    {
        CloseCurrentBox();
    }
}
