using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level_49 : BaseLevel
{
    [Header("Answers")]
    public Transform theCockroach;

    private bool isEnd;
    private RectTransform currentTransformCockroach;

    public Transform posLimit_TR;
    public Transform posLimit_TL;
    public Transform posLimit_BR;
    public Transform posLimit_BL;
    public Vector3 posCenter;

    public Image cockroachLife;
    public Image cockroachDie;

    protected override void Start()
    {
        base.Start();

        posCenter = new Vector3();
        posCenter.x = (posLimit_BR.position.x + posLimit_BL.position.x) / 2;
        posCenter.y = (posLimit_TR.position.y + posLimit_BL.position.y) / 2;
    }

    private bool canCheckUpdate = true;
    int indexTouch_1 = -1;
    protected override void Update()
    {
        base.Update();

        if (!canCheckUpdate) return;
        canCheckUpdate = false;
        if (Input.touchCount <= 1) indexTouch_1 = -1;
        for (int i = 0; i < Input.touchCount; i++)
        {
            //var distance = Vector3.Distance(Input.GetTouch(i).position, theCockroach.position);
            var pointA = Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position);
            var distance = Vector2.Distance((Vector2)pointA, (Vector2)theCockroach.position);
            Debug.Log("distance " + i + "--" + distance);
            Debug.Log("indexTouch_1 " + indexTouch_1);
            if (indexTouch_1 != -1)
            {
                if (i >= 1)
                {
                    if (distance <= 0.15f)
                    {
                        theCockroach.DOKill();
                        cockroachLife.enabled = false;
                        cockroachDie.enabled = true;
                        RightAnswer();
                    }
                }

            }
            else
            {
                if (distance <= 0.5f)
                {
                    indexTouch_1 = i;
                    CockroachRun();
                }
            }
        }
        canCheckUpdate = true;
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
        if (isEnd) return;

        isEnd = true;
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    private void CockroachRun()
    {
        if (canCheckUpdate) return;

        float speed = 3;

        Vector3 posMove = RandomPosMove(theCockroach.position);
        posMove.z = 0;


        var distance = Vector2.Distance(posMove, theCockroach.position);

        LookAt(posMove);
        theCockroach.transform.DOKill();
        theCockroach.transform.DOLocalRotateQuaternion(targetRotation, 0.05f);
        theCockroach.DOMove(posMove, distance / speed);
    }

    private Quaternion targetRotation = Quaternion.identity;
    private void LookAt(Vector3 targetPos)
    {
        var dir = targetPos - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle -= 90;
        targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private Vector3 RandomPosMove(Vector3 position)
    {
        if (position.x <= posCenter.x && position.y >= posCenter.y)
        {
            float randomAngle = Random.Range(300, 330);
            Vector3 directionRandom = Helper.GetDirectionFromAngle_2(Vector3.down, randomAngle, position);
            Vector3 randomPosMove = Helper.GetPointDistanceFromObject(Random.Range(3, 5), directionRandom, position);

            return ClaimPos(randomPosMove);//A
        }

        if (position.x >= posCenter.x && position.y >= posCenter.y)
        {
            float randomAngle = Random.Range(30, 60);
            Vector3 directionRandom = Helper.GetDirectionFromAngle_2(Vector3.down, randomAngle, position);
            Vector3 randomPosMove = Helper.GetPointDistanceFromObject(Random.Range(3, 5), directionRandom, position);

            return ClaimPos(randomPosMove);//B
        }

        if (position.x <= posCenter.x && position.y <= posCenter.y)
        {
            float randomAngle = Random.Range(210, 240);
            Vector3 directionRandom = Helper.GetDirectionFromAngle_2(Vector3.down, randomAngle, position);
            Vector3 randomPosMove = Helper.GetPointDistanceFromObject(Random.Range(3, 5), directionRandom, position);
            
            return ClaimPos(randomPosMove);//C
        }

        if (position.x >= posCenter.x && position.y <= posCenter.y)
        {
            float randomAngle = Random.Range(120, 150);
            Vector3 directionRandom = Helper.GetDirectionFromAngle_2(Vector3.down, randomAngle, position);
            Vector3 randomPosMove = Helper.GetPointDistanceFromObject(Random.Range(3, 5), directionRandom, position);

            return ClaimPos(randomPosMove);//D
        }


        return Vector3.down;
    }

    private Vector3 ClaimPos(Vector3 pos)
    {
        if (pos.x < posLimit_TL.position.x)
            pos.x = posLimit_TL.position.x + 0.5f;
        if (pos.x > posLimit_TR.position.x)
            pos.x = posLimit_TR.position.x - 0.5f;

        if (pos.y < posLimit_BL.position.y)
            pos.y = posLimit_BL.position.y + 0.5f;
        if (pos.y > posLimit_TL.position.y)
            pos.y = posLimit_TL.position.y - 0.5f;

        return pos;
    }
}