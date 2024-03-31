using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;

public class Level_187 : BaseLevel
{
    public Image imgLight;

    public Sprite[] arrSPLight = new Sprite[4];
    public Sprite defaultLight;

    public GameObject objCenter;

    [ReadOnly]
    [SerializeField]
    private bool _hold1;
    [ReadOnly]
    [SerializeField]
    private bool _hold2;
    [ReadOnly]
    [SerializeField]
    private bool _hold3;

    private bool isStart;
    private float timeLight = 0f;
    private int indexLight = 0;

    [SerializeField] private List<int> oderTab;
    private int currentIndexTab;

    private bool isEnd;

    public Text turnTxt;
    public Text[] GoTxt;
    //public GameObject panel;

    protected override void Start()
    {
        base.Start();
        _hold1 = _hold2 = _hold3 = false;
        turnTxt.text = "0/0";

        timeLight = 0.8f;
    }

    protected override void Update()
    {
        base.Update();
        if (isStart) return;
        timeLight += Time.deltaTime;
        if (timeLight >= 0.8f)
        {
            if (indexLight < oderTab.Count)
            {
                imgLight.sprite = arrSPLight[oderTab[indexLight] - 1];
                imgLight.SetNativeSize();
                indexLight++;

                turnTxt.text = "0/" + indexLight;
            }
            else
            {
                imgLight.sprite = defaultLight;
                isStart = true;
                for (int i = 0; i < GoTxt.Length; i++)
                {
                    GoTxt[i].gameObject.SetActive(true);
                }

                // panel.SetActive(false);
            }
            timeLight = 0;
        }
    }

    public override void StartLevel()
    {
        base.StartLevel();
    }

    public override void CompleteLevel()
    {
        base.CompleteLevel();
    }

    public override void WrongAnswer()
    {
        base.WrongAnswer();
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void OnPointerDownScale(Transform buttonTransform)
    {
        if (isEnd)
            return;

        if (!isStart)
            return;
        buttonTransform.transform.DOKill();
        buttonTransform.transform.localScale = Vector3.one;
        buttonTransform.transform.DOScale(1.1f, 0.3f).OnComplete(() => { buttonTransform.transform.DOScale(1f, 0.3f); });
    }

    public void OnPointDown(int indexHold)
    {
        if (isEnd)
            return;

        if (isStart)
        {
            for (int i = 0; i < GoTxt.Length; i++)
            {
                GoTxt[i].gameObject.SetActive(false);
            }
            if (oderTab[currentIndexTab] == indexHold)
            {
                imgLight.sprite = arrSPLight[indexHold - 1];
                imgLight.SetNativeSize();

                turnTxt.text = (currentIndexTab + 1) + "/" + oderTab.Count;

                if (currentIndexTab == oderTab.Count - 1)
                {
                    RightAnswer();
                    isEnd = true;
                    return;
                }

                currentIndexTab++;
            }
            else
            {
                WrongAnswer();
                currentIndexTab = 0;
                imgLight.sprite = defaultLight;
                turnTxt.text = "0/" + oderTab.Count;
                for (int i = 0; i < GoTxt.Length; i++)
                {
                    GoTxt[i].gameObject.SetActive(true);
                }
            }

        }
    }
}
