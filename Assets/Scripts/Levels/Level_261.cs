using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class Level_261 : BaseLevel
{
    public DragUI dragUI1;
    public DragUI dragUI2;

    public Image imgTiger_1;
    public Sprite spTiger_1_L;
    public Sprite spTiger_1_R;
    public Image imgTiger_2;
    public Sprite spTiger_2_L;
    public Sprite spTiger_2_R;

    public Transform tfNgon;

    private bool isShake;
    private bool isEnd = false;
    private bool isDone = false;

    private bool isLeft;
    private float time = 0f;

    protected override void Start()
    {
        base.Start();
        dragUI1.SetActiveDragNew(false);
        dragUI2.SetActiveDragNew(false);
    }

    protected override void Update()
    {
        base.Update();

        CheckShakeTrigger();

        time += Time.deltaTime;
        if (time >= 2f)
        {
            isLeft = !isLeft;
            imgTiger_1.sprite = isLeft ? spTiger_1_L : spTiger_1_R;
            imgTiger_2.sprite = isLeft ? spTiger_2_L : spTiger_2_R;

            time = 0f;
        }

        if (!isLeft)
        {
            if (dragUI1.isDraging || dragUI2.isDraging)
                WrongAnswer();
        }

        if (isDone)
        {
            if (Vector2.Distance(dragUI1.transform.position, tfNgon.position) <= 0.5f && dragUI1.isCanActive)
            {
                dragUI1.isDraging = false;
                dragUI1.SetActiveDragNew(false);
                dragUI1.gameObject.SetActive(false);
                if (!dragUI1.isCanActive && !dragUI2.isCanActive)
                    RightAnswer();
            }

            if (Vector2.Distance(dragUI2.transform.position, tfNgon.position) <= 0.5f && dragUI2.isCanActive)
            {
                dragUI2.isDraging = false;
                dragUI2.SetActiveDragNew(false);
                dragUI2.gameObject.SetActive(false);
                if (!dragUI1.isCanActive && !dragUI2.isCanActive)
                    RightAnswer();
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

    private Vector3 shakeDir;
    private void CheckShakeTrigger()
    {
        if (isEnd)
            return;

        shakeDir = Input.acceleration;

        if (shakeDir.sqrMagnitude >= 10f)
        {
            Debug.Log(StringHelper.StringColor("Shakeeeeeeeeee", ColorString.red));
            if (!isShake) isShake = true;

            if (isShake)
            {
                tfNgon.DOLocalMoveY(-146f, 1f).OnComplete(() =>
                {
                    dragUI1.SetActiveDragNew(true);
                    dragUI2.SetActiveDragNew(true);
                    isDone = true;
                });
                isEnd = true;
            }
        }
    }

    [Button]
    public void Rung()
    {
        tfNgon.DOLocalMoveY(-146f, 1f).OnComplete(() =>
        {
            dragUI1.SetActiveDragNew(true);
            dragUI2.SetActiveDragNew(true);
            isDone = true;
        });
        isEnd = true;
    }
}
