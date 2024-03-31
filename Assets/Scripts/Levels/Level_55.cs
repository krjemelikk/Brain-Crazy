using UnityEngine;
using UnityEngine.UI;
using EventDispatcher;
using System;

public class Level_55 : BaseLevel
{
    [Header("Answers")]
    public Button theBox;
    public RectTransform EndKeyMove;
    public RectTransform Key;
    private Vector3 remeberFristPosKey;

    public Sprite boxClose;
    public Sprite boxOpen;

    private Action<object> deleteKeyAction;

    protected override void Start()
    {
        base.Start();
        theBox.onClick.AddListener(() => WrongAnswer());
        var keyTrans = GameObject.FindGameObjectWithTag("ImageKeyHintTop").transform.position;
        if(keyTrans != null) Key.transform.position = keyTrans;

        deleteKeyAction = (sender) => { Key.gameObject.transform.position = remeberFristPosKey; Key.gameObject.SetActive(false); };
        //this.RegisterListener(EventID.GO_SHOP, deleteKeyAction);
        this.RegisterListener(EventID.GO_SETTING, deleteKeyAction);
        this.RegisterListener(EventID.GO_SELECT_LEVEL, deleteKeyAction);

        Key.gameObject.SetActive(true);
        remeberFristPosKey = Key.gameObject.transform.position;

        theBox.GetComponent<Image>().sprite = boxClose;
    }

    protected override void Update()
    {
        base.Update();
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

    public void EndDrag(RectTransform tran)
    {
        var distance = Vector3.Distance(tran.transform.position, EndKeyMove.transform.position);
        Debug.Log(distance);
        if (distance <= 0.25f)
        {
            Key.gameObject.SetActive(false);
            theBox.GetComponent<Image>().sprite = boxOpen;
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        //this.RemoveListener(EventID.GO_SHOP, deleteKeyAction);
        this.RemoveListener(EventID.GO_SETTING, deleteKeyAction);
        this.RemoveListener(EventID.GO_SELECT_LEVEL, deleteKeyAction);
    }
}