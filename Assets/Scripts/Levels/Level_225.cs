using UnityEngine;
using UnityEngine.UI;

public class Level_225 : BaseLevel
{
    public DragUI dragUI1;
    public DragUI dragUI2;
    public DragUI dragUI3;

    [SerializeField]
    private bool isDone1;
    [SerializeField]
    private bool isDone2;
    [SerializeField]
    private bool isDone3;
    [SerializeField]
    private bool isDone4;

    private float timeDown;

    public Image viewMiTom;

    public Sprite spMiTom1;
    public Sprite spMiTom2;
    public Sprite spMiTom3;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (!isDone1 && dragUI2.isCanActive)
        {
            if (Vector2.Distance(dragUI1.transform.position, dragUI2.transform.position) <= 0.25f)
            {
                //dragUI1.SetActiveDrag(false);
                dragUI2.SetActiveDrag(false);
                dragUI2.gameObject.SetActive(false);
                viewMiTom.sprite = spMiTom1;
                isDone1 = true;
            }
        }

        if (isDone1 && !isDone2 && dragUI3.isCanActive)
        {
            if (Vector2.Distance(dragUI1.transform.position, dragUI3.transform.position) <= 0.25f)
            {
                //dragUI1.SetActiveDrag(false);
                dragUI3.SetActiveDrag(false);
                dragUI3.gameObject.SetActive(false);
                viewMiTom.sprite = spMiTom2;

                //RightAnswer();

                isDone2 = true;
            }
        }

        if (isDone3 && !isDone4)
        {
            timeDown += Time.deltaTime;
            if (timeDown >= 3)
            {
                RightAnswer();
                isDone4 = true;
            }
        }
    }

    public void OnLevelDown()
    {
        if (isDone1 && isDone2)
        {
            timeDown = 0f;
            isDone3 = true;
        }
    }

    public void OnLevelUp()
    {
        isDone3 = false;
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
        viewMiTom.sprite = spMiTom3;
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }
}
