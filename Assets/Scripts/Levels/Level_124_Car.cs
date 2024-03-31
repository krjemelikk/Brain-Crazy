using UnityEngine;

public class Level_124_Car : MonoBehaviour
{
    public Level_124 levelCurrent;
    public bool isLeft;
    public RectTransform[] dirCar = new RectTransform[2];

    public void OnclickCar()
    {
        if (!isLeft)
        {
            levelCurrent.WrongAnswer();
        }
        else
        {
            levelCurrent.CompleteCar(this);
        }
    }

}
