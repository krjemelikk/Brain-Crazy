using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_128 : BaseLevel
{
    public GameObject matchesScale;
    public float maxMatchesScale = 1.3f;

    public ZoomObject zoomObject;

    public Transform tf1;
    public Transform tf2;
    public Transform tf3;

    private Vector3 v1;
    private Vector3 v2;
    private Vector3 v3;

    public DragUI matches;

    public Image imgMatches;
    public Sprite spMatchesDone;

    public GameObject matchObj;
    public GameObject fireCandle;
    public GameObject fireMatch;

    public BoxCollider2D boxMatches;
    public Level_128_Matches Matches;

    protected override void Start()
    {
        base.Start();
        v1 = tf1.position;
        v2 = tf2.position;
        v3 = tf3.position;
        maxMatchesScale = 0.45f;
        StartCoroutine(Helper.StartAction(() =>
        {
            Matches.ActiveMatches();
            zoomObject.isCanZoom = false;
            matches.isCanActive = false;
            matches.GetComponent<Image>().raycastTarget = false;
            imgMatches.sprite = spMatchesDone;
            imgMatches.transform.DOScale(1.5f, 1f).OnComplete(()=> 
            {
                matchObj.SetActive(true);
            });
        }, () => matchesScale.transform.localScale.x >= maxMatchesScale));
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
        tf1.position = v1;
        tf2.position = v2;
        tf3.position = v3;
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void GetMatches()
    {
        fireMatch.SetActive(true);
        boxMatches.enabled = false;
    }

    public void GetFire()
    {
        fireCandle.SetActive(true);

        RightAnswer();
    }
}