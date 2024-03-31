using UnityEngine;
using UnityEngine.UI;

public class Level_126 : BaseLevel
{
    [SerializeField] private Image smallMan_Img;
    [SerializeField] private Sprite smallMan_Idle;
    [SerializeField] private Sprite smallMan_Fight;
    [SerializeField] private Transform posYouSmallMan;

    [SerializeField] private Image bigMan_Img;
    [SerializeField] private Sprite bigMan_Idle;
    [SerializeField] private Sprite bigMan_Fight;
    [SerializeField] private Transform posYouBigMan;

    [SerializeField] private DragUI youTransform;
    [SerializeField] private GameObject tabTut;

    private bool isClickedFight;
    private bool isChoiceSmallMan;

    protected override void Start()
    {
        base.Start();
        isChoiceSmallMan = true;
    }

    public void OnEndDragYou()
    {
        if (Vector2.Distance(youTransform.transform.position, posYouSmallMan.position) < 0.5f)
        {
            youTransform.transform.position = posYouSmallMan.position;
            isChoiceSmallMan = true;
        }

        if (Vector2.Distance(youTransform.transform.position, posYouBigMan.position) < 0.5f)
        {
            youTransform.transform.position = posYouBigMan.position;
            isChoiceSmallMan = false;
        }
    }

    public void OnClickFight()
    {
        if (isClickedFight)
            return;

        tabTut.gameObject.SetActive(false);
        youTransform.isCanActive = false;
        isClickedFight = true;
        if (isChoiceSmallMan)
        {
            smallMan_Img.sprite = smallMan_Fight;
            smallMan_Img.SetNativeSize();

            StartCoroutine(Helper.StartAction(() =>
            {
                bigMan_Img.sprite = bigMan_Fight;
                bigMan_Img.SetNativeSize();
                smallMan_Img.sprite = smallMan_Idle;
                smallMan_Img.SetNativeSize();
                smallMan_Img.color = new Color(1, 0, 0, 1);

                WrongAnswer();
                StartCoroutine(Helper.StartAction(() => { GameController.Instance.ResetLevel(); }, 0.5f));
               
            }, 0.5f));
        }
        else
        {

            bigMan_Img.sprite = bigMan_Fight;
            smallMan_Img.sprite = smallMan_Idle;
            smallMan_Img.color = new Color(1, 0, 0, 1);
            bigMan_Img.SetNativeSize();
            smallMan_Img.SetNativeSize();

            RightAnswer();
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        StopAllCoroutines();
    }
}
