using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

public class RateBox : BaseBox
{
    [SerializeField] private Button rateBtn;
    [SerializeField] private Button lateBtn;

    private static GameObject instance;
    private Action _yesAction;

    

    [SerializeField] private GameObject animTut;
    private bool isTut;
    private float timer;
    [SerializeField] private GameObject[] starsTutObj;
    [SerializeField] private float timeStar;
    private int indexStarTut;
    public DOTweenAnimation animHand;
    private Vector3 posStartHand = Vector3.zero;
    private int currentNumTut;

    public static RateBox Setup()
    {
        if (instance == null)
        {
            instance = Instantiate(Resources.Load(PathPrefabs.RATE_BOX) as GameObject);
        }
        instance.SetActive(true);
        return instance.GetComponent<RateBox>();
    }

  

    public override void Show()
    {
        animTut.SetActive(false);
        isTut = false;

        actionBoxAppearDone = () => 
        {
            if (!RemoteConfigController.GetBoolConfig(StringHelper.ConfigFirebase.ON_OFF_TUT_RATE, false))
            {
                return;
            }

            if (posStartHand == Vector3.zero)
                posStartHand = animHand.transform.position;

            isTut = true;
            animTut.SetActive(true);
            ResetTutHandle();
        };
        
        for (int i = 0; i < 5; i++)
        {
            starsActive[i].gameObject.SetActive(false);
        }
        base.Show();
    }

    public void Rate()
    {
        StarChoisseHandle();
    }

    public void LateAction()
    {
        backObj.DoOff();
    }

    private void StarChoisseHandle()
    {
        if (rate >= 1 && rate <= 3)
        {
            float startTime = Time.timeSinceLevelLoad;

            backObj.DoOff();

            if (RemoteConfigController.GetBoolConfig(StringHelper.ConfigFirebase.ON_OFF_RATE_LOW, true))
                ConfirmBox.Setup().AddMessageYesHasCloseBtn(Localization.Get("s_Noti"), Localization.Get("s_make_game_better"), () => { });
            else
                Application.OpenURL(Config.OPEN_LINK_RATE);
            //open the facebook app
            //Application.OpenURL(Config.FanpageLinkApp);

            //if (Time.timeSinceLevelLoad - startTime <= 1f)
            //{
            //    //fail. Open safari.
            //    Application.OpenURL(Config.FanpageLinkWeb);
            //}
            //IsRated = 1;
            //Application.OpenURL(Config.OPEN_LINK_RATE);
        }
        else if (rate >= 4)
        {
            backObj.DoOff();
            Application.OpenURL(Config.OPEN_LINK_RATE);

            if(rate >= 5)
            {
                DataManager.IsRated_5_Star = true;
            }
        }
    }

    private void Update()
    {
        if(isTut)
        {
            timer += Time.deltaTime;
            
            if (timer >= timeStar)
            {
                if (indexStarTut >= 5)
                {
                    ResetTut();
                    isTut = false;
                    return;
                }
                // Debug.Log("1344");
                starsTutObj[indexStarTut].gameObject.SetActive(true);
                indexStarTut += 1;
                timer = 0;
                
            }
        }
    }

    public void ResetTut()
    {
        currentNumTut += 1;
        if (currentNumTut >= 1)
        {
            EndTut();
            StopAllCoroutines();
            return;
        } 

        StartCoroutine(Helper.StartAction(() => 
        {
            ResetTutHandle();
        }, timeStar));
       
    }

    public void ResetTutHandle()
    {
        for (int i = 0; i < 5; i++)
        {
            starsTutObj[i].gameObject.SetActive(false);
        }
        timer = 0;
        indexStarTut = 0;
        animHand.transform.position = posStartHand;
        animHand.transform.DOKill();
        animHand.transform.DOLocalMoveX(290, 1.5f);
         //animHand.DORewind();
         isTut = true;
    }

    public void EndTut()
    {
        isTut = false;
        animTut.SetActive(false);
       // ResetTut();
    }


    [Header("UI")]
    public Image[] starsActive;
    public Text textDialog;
    [HideInInspector] public int rate;

    public void OnClickStar(int idStar)
    {
        //Ấn vào Star
        EndTut();

        //Tắt các Star đằng trước
        int numStart = starsActive.Length;
        if (idStar < numStart - 1)
        {
            if (starsActive[idStar + 1].enabled)
            {
                for (int i = idStar + 1; i < starsActive.Length; i++)
                {
                    starsActive[i].gameObject.SetActive(false);
                }

            }
        }

        //Bật các Star đằng sau và chính nó
        for (int i = 0; i < idStar + 1; i++)
        {
            starsActive[i].gameObject.SetActive(true);
        }

        rate = idStar + 1;
    }

}
