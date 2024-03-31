using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_217 : BaseLevel
{
    [SerializeField] private List<Level_217_Ground> grounds;
  private List<Level_217_Ground> m_grounds;
    private int currentIDGround;

    [SerializeField] private RectTransform dogBtn;
    private bool isCanAppear;
    private float timer;

    private bool isEnd;

    [SerializeField] private Image iconDog;
    [SerializeField] private Sprite dogDieSpr;

    protected override void Start()
    {
        base.Start();
        timer = 0.55f;
        isCanAppear = true;

        m_grounds = new List<Level_217_Ground>(grounds);
        m_grounds = Helper.DisruptiveList(m_grounds);
    }

    private Level_217_Ground GetGround()
    {
        if(currentIDGround >= m_grounds.Count)
        {
            currentIDGround = 0;
            m_grounds = Helper.DisruptiveList(m_grounds);
        }

        var ground = m_grounds[currentIDGround];
        currentIDGround += 1;
        return ground;
    }

    private void DogAppear()
    {
        Level_217_Ground ground = GetGround();
        isCanAppear = false;

        dogBtn.transform.parent = ground.contentDog;
        dogBtn.anchoredPosition = new Vector2(ground.contentDog.GetComponent<RectTransform>().anchoredPosition.x, -200f);
        dogBtn.DOAnchorPosY(0, 0.3f)
            .OnComplete(
            () => 
            {
                dogBtn.DOAnchorPosY(-200f, 0.3f);
                isCanAppear = true;
                timer = 0;
            });
    }

    protected override void Update()
    {
        if (isEnd)
            return;

        base.Update();
        if(isCanAppear)
        {
            timer += Time.deltaTime;

            if(timer >= 0.55f)
            {
                DogAppear();
            }
        }
    }

    public void DraGround(int idGround)
    {
        for (int i = 0; i < grounds.Count; i++)
        {
            if (i == idGround || !grounds[i].canvasGroup.interactable || !grounds[idGround].canvasGroup.interactable)
                continue;

           // if(i == 1)
           // Debug.Log("idGround " + idGround + " i " + i + " Dis " +  Vector2.Distance(grounds[i].transform.position, grounds[idGround].transform.position));

            if(Vector2.Distance(grounds[i].transform.position, grounds[idGround].transform.position) <= 0.2f)
            {
                grounds[idGround].dragUI.isCanActive = false;
                grounds[idGround].transform.position = grounds[i].transform.position;
                //grounds[idGround].transform.parent = grounds[i].transform;
                grounds[idGround].canvasGroup.alpha = 0;
                grounds[idGround].canvasGroup.interactable = false;
                grounds[idGround].canvasGroup.blocksRaycasts = false;
                //grounds[idGround].gameObject.SetActive(false);
                grounds[i].Scale();
                // grounds.RemoveAt(i);

                m_grounds.Clear();
                for (int j = 0; j < grounds.Count; j++)
                {
                    if (grounds[j].canvasGroup.interactable)
                    {
                        m_grounds.Add(grounds[j]);
                    }
                }
                m_grounds = Helper.DisruptiveList(m_grounds);
                currentIDGround = 0;
                break;
            }
        }
    }

    public void CheckAnswer()
    {
        if(m_grounds.Count <= 1)
        {
            RightAnswer();
            dogBtn.DOKill();
            dogBtn.anchoredPosition = new Vector2(dogBtn.anchoredPosition.x, 0);
            iconDog.sprite = dogDieSpr;
            isEnd = true;
        }
        else
        {
            WrongAnswer();
        }
       

    }
}
