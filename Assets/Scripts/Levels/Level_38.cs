using UnityEngine;

public class Level_38 : BaseLevel
{
    public GameObject fridgeScale;
    public float maxFridgeScale = 1f;
    public RectTransform theBox;
    public RectTransform theElephant;

    private bool isCanCheck = false;
    float maxX, minX, minY, maxY;
    private Vector3 posStartElephant;

    public ZoomObject zoomObject;

    protected override void Start()
    {
        base.Start();

        maxX = theBox.transform.position.x + theBox.rect.width / 2;
        minX = theBox.transform.position.x - theBox.rect.width / 2;
        minY = theBox.transform.position.y - theBox.rect.height / 2;
        maxY = theBox.transform.position.y + theBox.rect.height / 2;
        posStartElephant = theElephant.transform.position;
        StartCoroutine(Helper.StartAction(() =>
        {
            zoomObject.isCanZoom = false;
            CheckAnswer();
        }, () => fridgeScale.transform.localScale.x >= maxFridgeScale));
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
        theElephant.transform.position = posStartElephant;
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

    private void CheckAnswer()
    {
        isCanCheck = true;
    }

    public void OnEndDrag()
    {
        if (!isCanCheck)
        {
            WrongAnswer();
            return;
        }

        if (Vector2.Distance(fridgeScale.transform.position, theElephant.transform.position) <= 1)
        {
            theElephant.position = fridgeScale.transform.position;
            RightAnswer();
        }
        //if (theElephant.transform.position.x < maxX
        //    && theElephant.transform.position.x > minX
        //    && theElephant.transform.position.y > minY
        //    && theElephant.transform.position.y < maxY)
        //{
        //    theElephant.position = fridgeScale.transform.position;
        //    RightAnswer();
        //}
        //else
        //{
        //    WrongAnswer();
        //}
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}