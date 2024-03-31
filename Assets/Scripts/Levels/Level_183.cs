using UnityEngine;

public class Level_183 : BaseLevel
{
    public Level_183_RedLight[] redLight;
    public Transform posRedLight;
    public Transform Map;

    public Level_183_Car[] cars;

    [HideInInspector] public bool isEnd;
    [HideInInspector] public bool isRedLight;

    public Transform posStartCar_L1;
    public Transform posStartCar_L2;
    public Transform posStartCar_L3;

    public Transform posEndCar_L1;
    public Transform posEndCar_L2;
    public Transform posEndCar_L3;

    public Transform posStopCar_L1;
    public Transform posStopCar_L2;
    public Transform posStopCar_L3;

    public DragUI dog;
    public Transform endPoint;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < redLight.Length; i++)
        {
            redLight[i].Init(this);
        }

        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].Init(this);
        }
    }

    protected override void Update()
    {
        if (isEnd)
            return;
        base.Update();

        if(isRedLight)
        {
            if (Vector2.Distance(dog.transform.position, endPoint.position) <= 0.2f)
            {
                RightAnswer();
                isEnd = true;
            }
        }
    }

    public void OnEndDragDog()
    {
        if (isRedLight)
        {

        }
    }
}
