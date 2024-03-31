using UnityEngine;
using UnityEngine.UI;

public class Level_33 : BaseLevel
{
    public Button btStart;
    public GameObject panelStart;
    public RectTransform Tree;
    public Transform dinos;
    public GameObject btnJump;

    public Sprite sprStart;
    public Sprite sprRestart;

    private Vector3 posStart;

    private bool isEnd;
    protected override void Start()
    {
        base.Start();
        posStart = dinos.position;
        btStart.onClick.AddListener(onClickStart);
    }

    protected override void Update()
    {
        base.Update();
        CheckCutTree();
    }

    public override void StartLevel()
    {
        base.StartLevel();
        btStart.GetComponent<Image>().sprite = sprStart;
        panelStart.SetActive(true);
        btnJump.SetActive(false);
    }

    public override void CompleteLevel()
    {
        base.CompleteLevel();
    }

    public override void WrongAnswer()
    {
        base.WrongAnswer();
        panelStart.SetActive(true);
        btStart.GetComponent<Image>().sprite = sprRestart;
        btnJump.SetActive(false);
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void onClickStart()
    {
        Time.timeScale = 1;
        panelStart.SetActive(false);
        dinos.position = posStart;
        btnJump.SetActive(true);
    }

    public void CheckWrongAnswer()
    {
        Time.timeScale = 0;
        WrongAnswer();
    }

    public void CheckRightAnswer()
    {
        Time.timeScale = 1;
        RightAnswer();
    }

    Vector3 posDownMouse;
    public void CheckCutTree()
    {
        if (Input.GetMouseButtonDown(0))
        {
            posDownMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posDownMouse.z = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector3 posUpMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            posUpMouse.z = 0;
            Debug.DrawLine(posDownMouse, posUpMouse, Color.black);

            Raycasting(posDownMouse, posUpMouse);
        }
    }

    void Raycasting(Vector3 posStart, Vector3 posEnd)
    {
        {
            Vector2 mouseDirection = posEnd - posStart;
            float dis = Vector2.Distance(posStart, posEnd);
            Ray2D ray = new Ray2D(posStart, mouseDirection);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, dis);

            if (hit.collider != null && hit.collider.gameObject.name == "Tree")
            {
                Tree.gameObject.SetActive(false);
            }
        }
    }
}
