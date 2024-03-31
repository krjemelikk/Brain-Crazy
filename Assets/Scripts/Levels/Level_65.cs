using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_65 : BaseLevel
{
    private int currentIDCardChoice = -1;
    [SerializeField] private Sprite iconUpsideDown;

    [SerializeField] private Image[] cardsImg;
    private Sprite[] rememeberSptCardsImg;

    private List<int> idCardMatch;

    private int numMatch;
    private bool isEnd;

    [SerializeField] private GameObject tabToPlayObj;
    [SerializeField] private Button tabToPlayBtn;

    protected override void Start()
    {
        base.Start();
        idCardMatch = new List<int>();
        currentIDCardChoice = -1;
        numMatch = 0;
        rememeberSptCardsImg = new Sprite[cardsImg.Length];
        for (int i = 0; i < cardsImg.Length; i++)
        {
            rememeberSptCardsImg[i] = cardsImg[i].sprite;
        }
    }

    public void Startlevel()
    {
        tabToPlayObj.SetActive(false);
        tabToPlayBtn.gameObject.SetActive(false);

        for (int i = 0; i < cardsImg.Length; i++)
        {
            int index = i;
            cardsImg[index].transform.DOKill();
            cardsImg[index].transform.localScale = Vector3.one;
            cardsImg[index].transform.DOScaleX(0.1f, 0.08f).SetUpdate(true).OnComplete(()=> 
            {
                cardsImg[index].transform.DOScaleX(1, 0.08f).SetUpdate(true).OnComplete(() =>
                {
                    cardsImg[index].sprite = iconUpsideDown;
                });
            });
            
        }
    }

    public void OnClickCard(string data)
    {
        if (isEnd)
            return;

        int idCard,  indexCardInArray;
        var dataSlip = data.Split('.');

        idCard = System.Int32.Parse(dataSlip[0]);
        indexCardInArray = System.Int32.Parse(dataSlip[1]);

        for (int i = 0; i < idCardMatch.Count; i++)
        {
            if(idCardMatch[i] == indexCardInArray)
            {
                return;
            }
        }

        idCardMatch.Add(indexCardInArray);
        if (currentIDCardChoice == -1)//Lật tấm bài đầu tiên
        {
            currentIDCardChoice = idCard;
            //Lật 1 tầm bài
            //cardsImg[indexCardInArray].sprite = rememeberSptCardsImg[indexCardInArray];
            cardsImg[indexCardInArray].transform.DOKill();
            cardsImg[indexCardInArray].transform.localScale = Vector3.one;
            cardsImg[indexCardInArray].transform.DOScaleX(0.1f, 0.08f).SetUpdate(true).OnComplete(() =>
            {
                cardsImg[indexCardInArray].transform.DOScaleX(1, 0.08f).SetUpdate(true).OnComplete(() =>
                {
                    cardsImg[indexCardInArray].sprite = rememeberSptCardsImg[indexCardInArray];
                });
            });
        }
        else
        {
            if(idCard == currentIDCardChoice)
            {
                //Lật đúng
                currentIDCardChoice = -1;
                //cardsImg[indexCardInArray].sprite = rememeberSptCardsImg[indexCardInArray];
                cardsImg[indexCardInArray].transform.DOKill();
                cardsImg[indexCardInArray].transform.localScale = Vector3.one;
                cardsImg[indexCardInArray].transform.DOScaleX(0.1f, 0.08f).SetUpdate(true).OnComplete(() =>
                {
                    cardsImg[indexCardInArray].transform.DOScaleX(1, 0.08f).SetUpdate(true).OnComplete(() =>
                    {
                        cardsImg[indexCardInArray].sprite = rememeberSptCardsImg[indexCardInArray];
                    });
                });
                WrongRightEffect.Instance.Right();
                numMatch++;
                if (numMatch >= 8)
                {
                    RightAnswer();
                }
            }
            else
            {
                //Lật sai
                cardsImg[indexCardInArray].transform.DOKill();
                cardsImg[indexCardInArray].transform.localScale = Vector3.one;
                cardsImg[indexCardInArray].transform.DOScaleX(0.1f, 0.08f).SetUpdate(true).OnComplete(() =>
                {
                    cardsImg[indexCardInArray].transform.DOScaleX(1, 0.08f).SetUpdate(true).OnComplete(() =>
                    {
                        cardsImg[indexCardInArray].sprite = rememeberSptCardsImg[indexCardInArray];
                        StartCoroutine(Helper.StartAction(() => {
                            WrongAnswer();
                            ResetLevel();
                        }, 0.2f));
                        
                    });
                });
                
            }
        }

    }

    private void ResetLevel()
    {
        currentIDCardChoice = -1;
        numMatch = 0;
        isEnd = false;
        for (int i = 0; i < cardsImg.Length; i++)
        {
            cardsImg[i].sprite = iconUpsideDown;
        }
        idCardMatch.Clear();
        for (int i = 0; i < cardsImg.Length; i++)
        {
            int index = i;
            cardsImg[index].transform.DOKill();
            cardsImg[index].transform.localScale = Vector3.one;
        }
        StopAllCoroutines();
    }
}
