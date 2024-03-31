using UnityEngine;
using UnityEngine.UI;

public class Level_224 : BaseLevel
{
    public Transform tfCheckDone;

    public DragUI dragUI1;
    public DragUI dragUI2;
    public DragUI dragUI3;

    private bool isDone1; 
    private bool isDone2;
    private bool isDone3;
    private bool isDone4;

    public GameObject obj1;
    public GameObject obj2;
    public GameObject objDone;

    public Image viewCoffe;
    public Sprite sp1;
    public Sprite sp2;

    protected override void Start()
    {
        base.Start();
    }

    public void OnclickPower()
    {
        obj1.SetActive(false);
        obj2.SetActive(false);
        objDone.SetActive(true);
        isDone1 = true;
    }

    protected override void Update()
    {
        base.Update();
        if (!isDone2 && isDone1)
        {
            if (Vector2.Distance(dragUI1.transform.position, tfCheckDone.position) <= 0.25f)
            {
                dragUI1.SetActiveDrag(false);
                dragUI1.transform.SetParent(tfCheckDone);
                dragUI1.transform.localPosition = Vector3.zero;
                isDone2 = true;
            }
        }

        if(isDone1 && isDone2)
        {
            if (Vector2.Distance(dragUI2.transform.position, tfCheckDone.position) <= 0.25f)
            {
                dragUI2.SetActiveDrag(false);
                dragUI2.gameObject.SetActive(false);
                viewCoffe.sprite = sp1;
                if (!isDone3 && isDone4)
                {
                    viewCoffe.sprite = sp2;
                    RightAnswer();
                }
                isDone3 = true;
            }

            if (Vector2.Distance(dragUI3.transform.position, tfCheckDone.position) <= 0.25f)
            {
                dragUI3.SetActiveDrag(false);
                dragUI3.gameObject.SetActive(false);
                viewCoffe.sprite = sp1;
                if (isDone3 && !isDone4)
                {
                    viewCoffe.sprite = sp2;
                    RightAnswer();
                }
                isDone4 = true;
            }
        }
    }

    public void CheckAnswer2()
    {
        if (isDone2 && isDone1) return;
        WrongAnswer();
    }

    public void CheckAnswer1()
    {
        if (isDone1) return;
        WrongAnswer();
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
        GameController.Instance.ResetLevel();
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }
}
