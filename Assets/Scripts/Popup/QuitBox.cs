using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class QuitBox : BaseBox
{
    private static GameObject instance;
    
    [SerializeField] private Button yesBtn;
    [SerializeField] private Button noBtn;

  
    protected override void OnStart()
    {
        base.OnStart();
        backObj.timeAnimClose = 0.5f;
    }

    public void Start()
    {
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(YesHandle);

        noBtn.onClick.RemoveAllListeners();
        noBtn.onClick.AddListener(NoHandle);
    }

    protected override void ActionDoOff()
    {
        base.ActionDoOff();

        mainPanel.localScale = Vector3.one;
        mainPanel.transform.DOScale(Vector3.zero, 0.5f).SetUpdate(true).SetEase(Ease.InBack);

    }

    public static QuitBox Setup()
    {
        if (instance == null)
        {
            // Create popup and attach it to UI
            instance = Instantiate(Resources.Load(PathPrefabs.QUIT_BOX) as GameObject);
        }
        instance.SetActive(true);
        return instance.GetComponent<QuitBox>();
    }

 
    public void YesHandle()
    {
        Application.Quit();
    }

    public void NoHandle()
    {
        backObj.DoOff();
    }
}
