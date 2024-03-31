using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_135 : BaseLevel
{
    public bool isPlayerDie;

    [SerializeField] private int numObjectPass;

    [SerializeField] private Level_135_Player player;
    [SerializeField] private GameObject restatPanel;

    private float timer;
    private float randomTimeSpawn;

    private bool isDone;

    public Transform mapObj;
    public bool isMoveLeft;
    public bool isMoveRight;
    public Transform imgPlayer;
    public Animator anim;

    public List<Image> lsGifts;
    public Sprite spGift;

    public Transform giftPlayer;

    protected override void Start()
    {
        base.Start();
        randomTimeSpawn = 0.7f;
        player.Init(this);
    }

    protected override void Update()
    {
        base.Update();

        if (isMoveLeft)
        {
            mapObj.Translate(Vector3.right * 2f * Time.deltaTime);
        }

        if (isMoveRight)
        {
            mapObj.Translate(Vector3.left * 2f * Time.deltaTime);
        }
    }

    public void PlayerDie()
    {
        isPlayerDie = true;
        restatPanel.SetActive(true);
    }
    
    public void RestartLevel()
    {
        countOnclick = 0;
        GameController.Instance.ResetLevel();
    }

    public void CheckDoneLevel()
    {
        lsGifts[numObjectPass].sprite = spGift;
        numObjectPass++;
        if(numObjectPass >= 5)
        {
            RightAnswer();
        }
    }

    private int countOnclick = 0;
    public void GetCoin()
    {
        if (numObjectPass >= 4)
        {
            countOnclick++;
            Debug.Log(countOnclick);

            if (countOnclick == 3)
            {
                anim.SetBool("isCoin", true);
                giftPlayer.DOLocalMoveX(-125f, 0.5f).OnComplete(() =>
                {
                    CheckDoneLevel();
                });
            }
        }
    }

    public void BtnLeft()
    {
        if (isMoveRight)
            return;
        isMoveLeft = true;
        imgPlayer.localEulerAngles = new Vector3(0, 180, 0);
        anim.SetBool("isRun", true);
    }

    public void BtnRight()
    {
        if (isMoveLeft)
            return;
        isMoveRight = true;
        imgPlayer.localEulerAngles = new Vector3(0, 0, 0);
        anim.SetBool("isRun", true);

    }

    public void BtnNotMoveLeft()
    {
        isMoveLeft = false;
        anim.SetBool("isRun", false);

    }

    public void BtnNotMoveRight()
    {
        isMoveRight = false;
        anim.SetBool("isRun", false);

    }
}
