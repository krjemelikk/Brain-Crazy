using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_103 : BaseLevel
{
    [SerializeField] private Image mouse_Img;
    [SerializeField] private Sprite mouse_Idle;
    [SerializeField] private Sprite mouse_Fight;
    [SerializeField] private Transform posHandMouse;

    [SerializeField] private Transform handMouse_Idle;
    [SerializeField] private Transform handMouse_Fight;

    [SerializeField] private Image cat_Img;
    [SerializeField] private Sprite cat_Idle;
    [SerializeField] private Sprite cat_Fight;

    [SerializeField] private Transform TrickyObj;
    [SerializeField] private GameObject tabTut;

    [SerializeField] private DragUI[] trickyDrag;
    [SerializeField] private Vector3[] posStartTrickyDrag;

    private bool isClickedFight;
    private bool isMouseTrick;

    protected override void Start()
    {
        base.Start();

        posStartTrickyDrag = new Vector3[trickyDrag.Length];
        for (int i = 0; i < posStartTrickyDrag.Length; i++)
        {
            posStartTrickyDrag[i] = trickyDrag[i].gameObject.transform.position;
        }
    }

    public void ChoiceTricky(Transform curr)
    {
        TrickyObj = curr;
    }

    public void OnEndDragYou()
    {
        if (isClickedFight)
            return;

        if (TrickyObj != null)
        {
            if (Vector2.Distance(TrickyObj.transform.position, posHandMouse.position) < 0.5f)
            {
                isMouseTrick = true;

                for (int i = 0; i < trickyDrag.Length; i++)
                {
                    trickyDrag[i].isCanActive = false;
                }

                TrickyObj.gameObject.SetActive(false);

                handMouse_Idle.gameObject.SetActive(true);
            }
            else
            {
                for (int i = 0; i < trickyDrag.Length; i++)
                {
                    trickyDrag[i].gameObject.transform.position = posStartTrickyDrag[i];
                }
            }
        }
    }

    public void OnClickFight()
    {
        if (isClickedFight)
            return;

        tabTut.gameObject.SetActive(false);

        isClickedFight = true;
        if (!isMouseTrick)
        {
            mouse_Img.transform.DOMoveX(mouse_Img.transform.position.x + 0.1f, 0.5f).OnComplete(() => 
            {
                mouse_Img.sprite = mouse_Fight;
                mouse_Img.SetNativeSize();

                mouse_Img.transform.DOMoveX(mouse_Img.transform.position.x - 0.5f, 0.5f).OnComplete(() =>
                {
                    StartCoroutine(Helper.StartAction(() =>
                    {
                        cat_Img.sprite = cat_Fight;
                        cat_Img.SetNativeSize();
                        mouse_Img.sprite = mouse_Idle;
                        mouse_Img.SetNativeSize();

                        mouse_Img.gameObject.transform.DOMoveX(mouse_Img.gameObject.transform.position.x + 10, 3);

                        WrongAnswer();
                        StartCoroutine(Helper.StartAction(() => { GameController.Instance.ResetLevel(); }, 0.5f));

                    }, 0.15f));
                });
            });
            
        }
        else
        {
            mouse_Img.transform.DOMoveX(mouse_Img.transform.position.x + 0.2f, 0.2f).OnComplete(() =>
            {
                handMouse_Idle.gameObject.SetActive(false);
                handMouse_Fight.gameObject.SetActive(true);
                mouse_Img.sprite = mouse_Fight;
                mouse_Img.SetNativeSize();

                mouse_Img.transform.DOMoveX(mouse_Img.transform.position.x - 0.15f, 0.09f).OnComplete(() => 
                {
                    cat_Img.gameObject.transform.DOMoveX(mouse_Img.gameObject.transform.position.x - 10, 3);
                    RightAnswer();
                });
            });
              
            
            
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        StopAllCoroutines();
        mouse_Img.DOKill();
        cat_Img.DOKill();
    }
}
