using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_237 : BaseLevel
{
    public DragUI dragUI1;
    public Transform tfCheckLock;
    private bool isSwap = false;
    private bool isShake;

    private bool isDone1;

    protected override void Start()
    {
        base.Start();
    }

    private Vector3 shakeDir;

    protected override void Update()
    {
        base.Update();

        RotationClock();

        if (isSwap && !isShake)
        {
            shakeDir = Input.acceleration;

            if (shakeDir.sqrMagnitude >= 10f)
            {
                dragUI1.transform.DOMoveY(50f, 1f).OnComplete(() =>
                {
                    dragUI1.GetComponent<Image>().raycastTarget = true;
                });
                isShake = true;
            }
        }

        if (isSwap && isShake && !isDone1 && dragUI1.isCanActive)
        {
            if (Vector2.Distance(dragUI1.transform.position, tfCheckLock.position) <= 0.2f)
            {
                dragUI1.transform.position = tfCheckLock.transform.position;
                dragUI1.SetActiveDrag(false);
                RightAnswer();

                isDone1 = true;
            }
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

    #region Quay
    private Vector2 pointA;
    private Vector2 pointB;
    private bool touchStart = false;

    [SerializeField] private GameObject player;

    private bool isTouchkimdai;

    public void TouchKimDai()
    {
        isTouchkimdai = true;
    }

    private void RotationClock()
    {
        if (isSwap)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            pointA = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }

        if (Input.GetMouseButton(0))
        {
            touchStart = true;
            pointB = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        }
        else
        {
            touchStart = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isTouchkimdai = false;
        }

        if (!isTouchkimdai)
            return;

        if (touchStart)
        {
            Vector2 offset = (pointB - pointA).normalized;
            Vector2 direction = Vector2.ClampMagnitude(offset, 1.0f).normalized;
            if (pointB == pointA)
                return;
            Helper.LookAtToDirection(direction, player, 500);
            Debug.Log("Rotate " + player.transform.eulerAngles.z);
            if (player.transform.eulerAngles.z <= 200 && player.transform.eulerAngles.z >= 180)
            {
                isSwap = true;
            }
        }

    }
    #endregion
}
