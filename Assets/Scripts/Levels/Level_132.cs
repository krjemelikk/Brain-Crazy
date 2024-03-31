using UnityEngine;
using UnityEngine.UI;

public class Level_132 : BaseLevel
{
    [SerializeField] private Image HP_Img;
    [SerializeField] private Image monster;
    [SerializeField] private Sprite monsterDieSpr;

    Vector3 posDownMouse;
    Vector3 rememberPosDownMouse;
    private int currentClear;
    private bool isChoiceEraser;
    private float timer;
    private bool isIceOver;
    private bool isDone;

    protected override void Update()
    {
        base.Update();
        ClearHP();
    }

    public void OnDragEraser(bool isEraser)
    {
        isChoiceEraser = isEraser;
    }

    public void OnEndDrag()
    {
        if (isDone)
            return;

        WrongAnswer();
        isChoiceEraser = false;
    }

    public void ClearHP()
    {
        if (!isChoiceEraser)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            posDownMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            posDownMouse.z = 0;
            rememberPosDownMouse = posDownMouse;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 posUpMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("Dis " + Vector2.Distance(rememberPosDownMouse, posUpMouse));
            if (Vector2.Distance(rememberPosDownMouse, posUpMouse) > 0.1f)
            {
                posUpMouse.z = 0;
                //Debug.DrawLine(posDownMouse, posUpMouse, Color.black);

                Raycasting(posDownMouse, posUpMouse);
                rememberPosDownMouse = posUpMouse;
            }
        }
    }

    void Raycasting(Vector3 posStart, Vector3 posEnd)
    {
        if (!isDone)
        {
            Vector2 mouseDirection = posEnd - posStart;
            float dis = Vector2.Distance(posStart, posEnd);
            Ray2D ray = new Ray2D(posStart, mouseDirection);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, dis);

            if (hit.collider != null && hit.collider.gameObject.name == "HP")
            {
                timer += Time.deltaTime;
                if (timer >= 0.2f)
                {
                    currentClear++;
                    if (currentClear >= 5)
                    {
                        isIceOver = true;
                        HP_Img.gameObject.SetActive(false);
                        monster.sprite = monsterDieSpr;
                        StartCoroutine(Helper.StartAction(() => { RightAnswer(); }, 0.4f));
                        isDone = true;
                        return;
                    }

                    HP_Img.color = new Color(1, 1, 1, HP_Img.color.a - 0.2f);
                    //dirtyImg.sprite = dirtySprites[currentClear];
                    timer = 0;
                }
            }
        }
    }
}
