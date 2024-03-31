using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_82 : BaseLevel
{
    public RectTransform theCar;
    public float speed;
    public RectTransform StopText;

    public Dictionary<SystemLanguage, LocalizeAnswer> StopTexts;

    private bool isEnd = true;
    protected override void Start()
    {
        base.Start();
        StartCoroutine(StartMove());
    }

    protected override void Update()
    {
        base.Update();

        if (isEnd)
            return;

        theCar.Translate(Vector3.right * speed * Time.deltaTime);

        if (theCar.localPosition.x >= GameController.Instance.HomeScene.BoundRight.localPosition.x + 150)
        {
            theCar.localPosition = new Vector3(GameController.Instance.HomeScene.BoundLeft.localPosition.x - 150, theCar.localPosition.y, theCar.localPosition.z);
        }

        CheckAnswer();
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
        if (isEnd) return;

        base.WrongAnswer();
    }

    public override void RightAnswer()
    {
        isEnd = true;
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void CheckAnswer()
    {
        var distance = Vector2.Distance(theCar.position, StopText.transform.position);
        //Debug.Log(distance);
        if (distance <= 0.5f && StopText.localPosition.y >= -300 && StopText.localPosition.y <= -180)
        {
            RightAnswer();
        }
    }

    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(1f);

        isEnd = false;
    }

    public void EndDrag(RectTransform tran)
    {
        if (isEnd) return;
        CheckAnswer();
    }

    protected override void UpdateText()
    {
        foreach (var item in StopTexts)
        {
            item.Value.Question.SetActive(false);
            Debug.Log("Compare " + string.Compare(Localization.language, item.Key.ToString(), StringComparison.Ordinal)
                + " Localization.language " + Localization.language + " item.Key " + item.Key.ToString());

            if (string.Compare(Localization.language, item.Key.ToString(), StringComparison.Ordinal) == 0)
            {
                item.Value.Question.SetActive(true);
                StopText = item.Value.Answer.transform.parent.GetComponent<RectTransform>();
                //if (txtQuestion != null) txtQuestion.text = Localization.Get(KeyQuestion);
                // if (txtName != null) txtName.text = $"{Localization.Get("lb_level")} {IDQuestion}";
                if (txtName != null) txtName.text = $"{Localization.Get("lb_level")} {ID}";
            }
        }

    }
}
