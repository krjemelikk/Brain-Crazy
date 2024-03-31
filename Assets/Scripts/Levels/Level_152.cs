using UnityEngine;

public class Level_152 : BaseLevel
{
    [SerializeField] private GameObject brigde;
    [SerializeField] private Transform posCheckBrigde;
    private bool isEnd;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (isEnd)
            return;

        base.Update();

        if(brigde.transform.position.x  < posCheckBrigde.position.x)
        {
            RightAnswer();
            isEnd = true;
        }
    }
}
