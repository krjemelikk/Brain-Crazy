using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class Level_92 : BaseLevel
{
    [Header("Object")]
    public EventTrigger daGiac;
    public EventTrigger hinhThang;
    public EventTrigger hinhChuNhat;

    [Header("Farme")]
    public GameObject daGiacFarme;
    public GameObject hinhThangFarme;
    public GameObject hinhChuNhatFarme;

    private Vector3 daGiacRemeberPos;
    private Vector3 hinhThangRemeberPos;
    private Vector3 hinhChuNhatRemeberPos;

    public float distanceDaGiac;
    public float distanceHinhThang;
    public float distanceHinhChuNhat;

    private int rightFarme;


    protected override void Start()
    {
        base.Start();

        daGiacRemeberPos = daGiac.transform.position;
        hinhThangRemeberPos = hinhThang.transform.position;
        hinhChuNhatRemeberPos = hinhChuNhat.transform.position;

        rightFarme = 0;
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

    public void OnEndDragObject(int hinh)
    {
        switch (hinh)
        {
            case 1:
                if (Vector2.Distance(daGiac.transform.position, daGiacFarme.transform.position) <= distanceDaGiac)
                {
                    daGiac.enabled = false;
                    daGiac.transform.DOKill();
                    daGiac.transform.DOMove(daGiacFarme.transform.position, 0.5f);
                    rightFarme++;
                    if (rightFarme >= 3)
                        RightAnswer();
                }
                else
                {
                    daGiac.transform.DOKill();
                    daGiac.transform.DOMove(daGiacRemeberPos, 0.5f);
                }
                break;
            case 2:
                if (Vector2.Distance(hinhThang.transform.position, hinhThangFarme.transform.position) <= distanceHinhThang)
                {
                    hinhThang.enabled = false;
                    hinhThang.transform.DOKill();
                    hinhThang.transform.DOMove(hinhThangFarme.transform.position, 0.5f);
                    rightFarme++;
                    if (rightFarme >= 3)
                        RightAnswer();
                }
                else
                {
                    hinhThang.transform.DOKill();
                    hinhThang.transform.DOMove(hinhThangRemeberPos, 0.5f);
                }
                break;
            case 3:
                if (Vector2.Distance(hinhChuNhat.transform.position, hinhChuNhatFarme.transform.position) <= distanceHinhChuNhat)
                {
                    hinhChuNhat.enabled = false;
                    hinhChuNhat.transform.DOKill();
                    hinhChuNhat.transform.DOMove(hinhChuNhatFarme.transform.position, 0.5f);
                    rightFarme++;
                    if (rightFarme >= 3)
                        RightAnswer();
                }
                else
                {
                    hinhChuNhat.transform.DOKill();
                    hinhChuNhat.transform.DOMove(hinhChuNhatRemeberPos, 0.5f);
                }
                break;
        }
    }
}
