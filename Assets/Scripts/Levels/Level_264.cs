using UnityEngine;
using UnityEngine.UI;

public class Level_264 : BaseLevel
{
    public DragUI dragUI1;
    public DragUI dragUI2;

    public Image imgThuyen;
    public Sprite spThuyen1;
    public Sprite spThuyen2;

    public Image imgXo;
    public Sprite spXo0;
    public Sprite spXo1;
    public Sprite spXo2;

    public Image imgPlayer;
    public Sprite spPlayer;

    public Transform tfThung;

    private bool isDone;
    private bool isDone1;

    private bool isXo;
    private bool isXoing;

    private int cout = 0;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        if (!isXoing && !isDone)
        {
            if (!isXo)
            {
                if (Vector2.Distance(dragUI1.transform.position, imgThuyen.transform.position) <= 0.25f)
                {
                    imgXo.sprite = spXo1;
                    imgXo.SetNativeSize();
                    imgThuyen.sprite = cout == 0 ? spThuyen1 : spThuyen2;
                    imgThuyen.SetNativeSize();
                    isXo = true;
                }
            }
            else
            {
                if (Vector2.Distance(dragUI1.transform.position, imgThuyen.transform.position) >= 0.5f)
                {
                    isXoing = true;
                    imgXo.sprite = spXo2;
                    imgXo.SetNativeSize();
                    Helper.StartActionNotUseCorutines(() =>
                    {
                        cout++;
                        imgXo.sprite = spXo0;
                        imgXo.SetNativeSize();
                        isXoing = false;
                        if (cout >= 2)
                            isDone = true;
                    }, 0.5f);
                    isXo = false;
                }
            }
        }

        if(isDone && !isDone1)
            if (Vector2.Distance(dragUI2.transform.position, tfThung.position) <= 0.25f)
            {
                dragUI2.SetActiveDragNew(false);
                dragUI2.transform.position = tfThung.position;
                imgPlayer.sprite = spPlayer;
                imgPlayer.SetNativeSize();
                RightAnswer();
                isDone1 = true;
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
