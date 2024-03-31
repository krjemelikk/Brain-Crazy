using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level_70 : BaseLevel
{
    [Header("Answers")]
    public Button theMonster;
    public Button[] theWrongs;
    public Transform[] theWeapons;
    public Image imgMonster;
    public Image imgLight;
    public Sprite monster_Normal;
    public Sprite monster_Die;
    public DOTweenAnimation animKill;

    private int countWeapon;
    private bool isEnd;

    protected override void Start()
    {
        base.Start();
        theMonster.onClick.AddListener(() => WrongAnswer());
        for (int i = 0; i < theWrongs.Length; i++)
        {
            theWrongs[i].onClick.AddListener(() => WrongAnswer());
        }

        theMonster.GetComponent<Image>().sprite = monster_Normal;
        countWeapon = 0;
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
        isEnd = true;
        imgLight.gameObject.SetActive(true);
        animKill.DOPlay();
        StartCoroutine(MonsterDie());
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void CheckAnswer()
    {
        if(countWeapon >= 3)
        {
            RightAnswer();
        }
    }

    public void EndDrag(RectTransform tran)
    {
        if (isEnd) return;

        int _ID = -1;
        if (int.TryParse(tran.gameObject.name,out _ID))
        {
            var distance = Vector2.Distance(tran.position, theWrongs[0].transform.position);
            Debug.Log(tran.gameObject.name + " / " + distance);

            if(distance <= 0.6f)
            {
                tran.gameObject.SetActive(false);
                theWeapons[_ID].gameObject.SetActive(true);
                countWeapon++;
            }
            else
            {
                WrongAnswer();
                tran.transform.localPosition = localPositionWrong;
            }
        }

        CheckAnswer();
    }

    Vector3 localPositionWrong;

    public void BeginDrag(RectTransform tran)
    {
        localPositionWrong = tran.transform.localPosition;
    }

    IEnumerator MonsterDie()
    {
        yield return new WaitForSeconds(0.75f);

        theMonster.GetComponent<Image>().sprite = monster_Die;
        base.RightAnswer();
    }
}