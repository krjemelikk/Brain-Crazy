using UnityEngine;

public class Level_183_Car : MonoBehaviour
{
    [SerializeField] private int idLane;
    private Transform posStartCar;
    private Transform posEndCar;
    private Transform posStopCar;

    private Level_183 level;

    [SerializeField] private float speedMove;

    public void Init(Level_183 level)
    {
        this.level = level;

        if (idLane == 1)
        {
            posStartCar = level.posStartCar_L1;
            posEndCar = level.posEndCar_L1;
            posStopCar = level.posStopCar_L1;
        }
        else if (idLane == 2)
        {
            posStartCar = level.posStartCar_L2;
            posEndCar = level.posEndCar_L2;
            posStopCar = level.posStopCar_L2;
        }
        else if(idLane == 3)
        {
            posStartCar = level.posStartCar_L3;
            posEndCar = level.posEndCar_L3;
            posStopCar = level.posStopCar_L3;
        }
    }

    public void Update()
    {
        MoveCar();
    }

    private void MoveCar()
    {
        if (level.isRedLight)
        {
            if (this.transform.position.x >= posStopCar.transform.position.x)
            {
                this.transform.position = new Vector3(posStopCar.transform.position.x, this.transform.position.y, this.transform.position.z);
                return;
            }
            this.transform.Translate(Vector2.right * speedMove * Time.deltaTime);
        }
        else
        {
            if (this.transform.position.x >= posEndCar.transform.position.x)
            {
                this.transform.position = new Vector3(posStartCar.transform.position.x, this.transform.position.y, this.transform.position.z);
            }
            this.transform.Translate(Vector2.right * speedMove * Time.deltaTime);
        }
    }
}
