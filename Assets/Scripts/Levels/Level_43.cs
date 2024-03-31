using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public class Level_43 : BaseLevel
{
    [Header("Answers")]
    public Button theCar;
    public RectTransform transformMan;
    public RectTransform transformCar;
    public RectTransform PosStart;
    public RectTransform EndCar;
    public RectTransform EndMan;
    public RectTransform Tree_1;
    public RectTransform Tree_2;
    public GameObject panelRestart;
    public float speed;
    private bool moveLeft;
    private bool moveRight;
    private bool isEnd;
    private RectTransform currentTransform;

    public SkeletonGraphic humanAnim;
    private Vector3 posStartMan;
    private bool isAnim;

    protected override void Start()
    {
        base.Start();
        theCar.onClick.AddListener(OnClickCar);
        currentTransform = transformCar;
        isEnd = false;
        Tree_1.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Tree_2.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -5));
        panelRestart.SetActive(false);
        PosStart.transform.position = transformCar.position;
        posStartMan = transformMan.position;
        transformCar.rotation = Quaternion.Euler(0, 0, 0);
        isAnim = false;
    }

    protected override void Update()
    {
        base.Update();

        if (isEnd) return;

        if (moveLeft && !moveRight)
        {
            if (currentTransform == transformMan)
            {
                // currentTransform.Translate(Vector3.left * speed * Time.deltaTime);
                currentTransform.position = Vector3.MoveTowards(currentTransform.position, EndMan.position - new Vector3(0.5f, 0f, 0f), speed * Time.deltaTime);
                currentTransform.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (currentTransform == transformCar)
            {
                currentTransform.Translate(Vector3.left * speed * Time.deltaTime);
                transformMan.Translate(Vector3.left * speed * Time.deltaTime);
            }
        }
        else if (moveRight && !moveLeft)
        {
            if (currentTransform == transformMan)
            {
                if (currentTransform.localPosition.x >= PosStart.localPosition.x)
                    return;

                //currentTransform.Translate(Vector3.right * speed * Time.deltaTime);
                currentTransform.position = Vector3.MoveTowards(currentTransform.position, PosStart.position, speed * Time.deltaTime);
                currentTransform.transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (currentTransform == transformCar)
            {
                if (currentTransform.localPosition.x >= PosStart.localPosition.x)
                    return;

                currentTransform.Translate(Vector3.right * speed * Time.deltaTime);
                transformMan.Translate(Vector3.right * speed * Time.deltaTime);
            }
        }

        CheckAnswer();
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
        Tree_1.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -20));
        Tree_2.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 20));
        transformCar.rotation = Quaternion.Euler(0, 0, 30);
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

    private void CheckAnswer()
    {
        if (isEnd) return;

        if (transformCar.localPosition.x <= EndCar.localPosition.x)
        {
            WrongAnswer();
            panelRestart.SetActive(true);
            isEnd = true;
            return;
        }

        if (transformMan.localPosition.x <= EndMan.localPosition.x + 0.15f || currentTransform.localPosition.x <= EndMan.localPosition.x + 0.15f)
        {
            RightAnswer();
            isEnd = true;
        }
    }

    public void RestartLevel()
    {
        transformMan.SetSiblingIndex(1);
        currentTransform = transformCar;
        isEnd = false;
        moveLeft = false;
        moveRight = false;
        transformCar.localPosition = PosStart.localPosition;
        transformMan.position = posStartMan;
        transformCar.rotation = Quaternion.Euler(0, 0, 0);
        Tree_1.transform.localRotation = Quaternion.Euler(Vector3.zero);
        Tree_2.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, -5));
        panelRestart.SetActive(false);
    }
    private void OnClickCar()
    {
        transformMan.SetAsLastSibling();
        currentTransform = transformMan;
        currentTransform.transform.position = new Vector3(currentTransform.transform.position.x,
            currentTransform.transform.position.y - 0.1f, currentTransform.transform.position.z);
        PosStart.transform.position = currentTransform.transform.position;
        humanAnim.AnimationState.ClearTrack(0);
    }

    public void MoveLeft()
    {
        moveLeft = true;
        moveRight = false;

        if (currentTransform == transformMan)
            if (!isAnim)
            {
                humanAnim.AnimationState.SetAnimation(0, "animation", true);
                isAnim = true;
            }
    }

    public void MoveRight()
    {
        moveLeft = false;
        moveRight = true;

        if (currentTransform == transformMan)
            if (!isAnim)
            {
                humanAnim.AnimationState.SetAnimation(0, "animation", true);
                isAnim = true;
            }
    }

    public void Idle()
    {
        if (currentTransform == transformMan)
            isAnim = false;
        humanAnim.AnimationState.ClearTrack(0);
        moveLeft = false;
        moveRight = false;
    }
}