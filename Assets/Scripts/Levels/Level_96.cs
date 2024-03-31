using UnityEngine;
using UnityEngine.UI;

public class Level_96 : BaseLevel
{
    [SerializeField] private Button coverBtn;
    [SerializeField] private Button goBtn;
    [SerializeField] private Button restartBtn;

    [SerializeField] private float speedCard = 0.5f;

    [SerializeField] private GameObject panelRestart;
    private bool isCover;
    private bool isPassLevel;
    private bool carCanMove;

    [SerializeField] private GameObject carObj;
    [SerializeField] private GameObject brigdeLeftObj;
    [SerializeField] private GameObject brigdeRightObj;
    [SerializeField] private GameObject posCheckStart;
    [SerializeField] private GameObject posCheckEnd;
    [SerializeField] private GameObject posEndCar;

    protected override void Start()
    {
        base.Start();
        goBtn.onClick.RemoveAllListeners();
        goBtn.onClick.AddListener(GoHandle);

        restartBtn.onClick.RemoveAllListeners();
        restartBtn.onClick.AddListener(RestartHanle);
    }

    protected override void Update()
    {
        base.Update();
        MoveCar();
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
        base.RightAnswer();
        carCanMove = false;
        isPassLevel = true;
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void IsPointerDownCover()
    {
        isCover = true;
    }

    public void IsPointerUpCover()
    {
        isCover = false;
    }

    private void GoHandle()
    {
        carCanMove = true;
    }

    private void MoveCar()
    {
        if (!carCanMove)
            return;

        if (isPassLevel)
            return;

        carObj.transform.Translate(Vector3.right * -speedCard * Time.deltaTime);
        if(carObj.transform.position.x >= posCheckStart.transform.position.x)
        {
            //Đã đi vào điểm check
            if(carObj.transform.position.x < posCheckEnd.transform.position.x)
            {
                //Nếu vẫn chưa đi qua điểm đích
                if(!isCover)
                {
                    //Không che tay vào chỗ gãy của cầu
                    //=>thua
                    WrongAnswer();
                    carCanMove = false;
                    BreakHandle();
                    panelRestart.SetActive(true);
                }
            }
            if (carObj.transform.position.x > posEndCar.transform.position.x)
            {
                RightAnswer();
            }
        }
    }

    private void BreakHandle()
    {
        brigdeLeftObj.transform.rotation = Quaternion.Euler(0, 0, 10);
        brigdeRightObj.transform.rotation = Quaternion.Euler(0, 0, -10);
        //carObj.transform.rotation = Quaternion.Euler(0, 0, -10);
    }

    public void RestartHanle()
    {
        GameController.Instance.ResetLevel();
    }
}
