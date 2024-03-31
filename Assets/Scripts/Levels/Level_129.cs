using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_129 : BaseLevel
{
    public Transform tf1;
    public Transform tf2;

    public Transform tf4;
    public Transform tf5;
    public Transform tf6;
    public Transform tf7;

    private Vector3 v1;
    private Vector3 v2;

    private Vector3 v4;
    private Vector3 v5;
    private Vector3 v6;
    private Vector3 v7;

    public BoxCollider2D box1;
    public BoxCollider2D box2;

    public Image imgPlayer;
    public Sprite spPlayerSmile;

    protected override void Start()
    {
        base.Start();

        v1 = tf1.position;
        v2 = tf2.position;

        v4 = tf4.position;
        v5 = tf5.position;
        v6 = tf6.position;
        v7 = tf7.position;
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
        tf4.position = v4;
        tf5.position = v5;
        tf6.position = v6;
        tf7.position = v7;
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }


    public void CheckDis()
    {
        if (Vector3.Distance(tf1.position, v1) > 1f)
        {
            box1.enabled = true;
            tf1.GetComponent<Image>().raycastTarget = false;
            tf1.SetParent(transform);
        }

        if (Vector3.Distance(tf2.position, v2) > 1f)
        {
            box2.enabled = true;
            tf2.GetComponent<Image>().raycastTarget = false;
            tf2.SetParent(transform);
        }
    }

    public void PlayerSmile()
    {
        tf1.GetComponent<Image>().raycastTarget = false;
        tf2.GetComponent<Image>().raycastTarget = false;
        tf4.GetComponent<Image>().raycastTarget = false;
        tf5.GetComponent<Image>().raycastTarget = false;
        tf6.GetComponent<Image>().raycastTarget = false;
        tf7.GetComponent<Image>().raycastTarget = false;

        imgPlayer.sprite = spPlayerSmile;
        imgPlayer.transform.DOLocalRotate(new Vector3(0f, 0f, 90f), 1f).OnComplete(()=> 
        {
            RightAnswer();
        });
    }
}