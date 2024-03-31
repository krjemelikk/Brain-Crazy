using UnityEngine;

public class Level_202_Button : MonoBehaviour
{
    public Level_202 levelCurrent;

    public int index;
    public int id;

    public void CheckButton()
    {
        levelCurrent.Onclick(index, id);
    }
}
