using UnityEngine;
using UnityEngine.UI;

public class Level_81 : BaseLevel
{
    private bool isEnd;
    private GameObject number9;
    [SerializeField] private GameObject English;
    [SerializeField] private GameObject Vietnamese;
    [SerializeField] private GameObject Russian;
    [SerializeField] private GameObject Spanish;
    [SerializeField] private GameObject French;
    [SerializeField] private GameObject Japanese;
    [SerializeField] private GameObject Korean;
    [SerializeField] private GameObject Thai;
    [SerializeField] private GameObject Arabic;
    [SerializeField] private GameObject Chinese;
    [SerializeField] private GameObject Portuguese;
    [SerializeField] private GameObject German;
    [SerializeField] private GameObject Italian;
    [SerializeField] private GameObject Indonesian;
    [SerializeField] private GameObject Turkish;
    public Button[] btWrongs;
    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < btWrongs.Length; i++)
        {
            btWrongs[i].onClick.AddListener(WrongAnswer);
        }
        switch (Localization.language)
        {
            case "English":
                English.SetActive(true);
                break;
            case "Vietnamese":
                Vietnamese.SetActive(true);
                break;
            case "Russian":
                Russian.SetActive(true);
                break;
            case "Spanish":
                Spanish.SetActive(true);
                break;
            case "French":
                French.SetActive(true);
                break;
            case "Japanese":
                Japanese.SetActive(true);
                break;
            case "Korean":
                Korean.SetActive(true);
                break;
            case "Thai":
                Thai.SetActive(true);
                break;
            case "Arabic":
                Arabic.SetActive(true);
                break;
            case "Chinese":
                Chinese.SetActive(true);
                break;
            case "Portuguese":
                Portuguese.SetActive(true);
                break;
            case "German":
                German.SetActive(true);
                break;
            case "Italian":
                Italian.SetActive(true);
                break;
            case "Indonesian":
                Indonesian.SetActive(true);
                break;
            case "Turkish":
                Turkish.SetActive(true);
                break;
            default:
                English.SetActive(true);
                break;
        }

        number9 = GameObject.Find("Level_81_Number_9");
    }

    protected override void Update()
    {
        base.Update();
        RotationClock();
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
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void CheckAnswer()
    {
        if (isEnd)
            return;
        number9.transform.eulerAngles = new Vector3(number9.transform.eulerAngles.x, number9.transform.eulerAngles.y, 180);
        RightAnswer();
    }

    public void EndDragWrong(RectTransform tran)
    {
        if (tran.transform.localPosition != localPositionWrong)
        {
            WrongAnswer();
            tran.transform.localPosition = localPositionWrong;
        }
    }

    Vector3 localPositionWrong;

    public void BeginDragWrong(RectTransform tran)
    {
        localPositionWrong = tran.transform.localPosition;
    }

    #region Quay
    private Vector2 pointA;
    private Vector2 pointB;
    private bool touchStart = false;

    private bool isTouchkimdai;

    public void TouchKimDai()
    {
        isTouchkimdai = true;
    }

    private void RotationClock()
    {
        if (isEnd)
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
            Helper.LookAtToDirection(direction, number9, 500);
            Debug.Log("Rotate " + number9.transform.eulerAngles.z);
            if (number9.transform.eulerAngles.z <= 200 && number9.transform.eulerAngles.z >= 160)
            {
                CheckAnswer();                
            }
        }

    }
    #endregion
}
