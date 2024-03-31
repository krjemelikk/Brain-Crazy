using UnityEngine;
using UnityEngine.UI;

public class Level_242_Button : MonoBehaviour
{
    public Level_242 levelCurrent;

    public int index;
    public int id;

    public Image viewBtn;

    public Sprite spDone;

    public void CheckButton()
    {
        viewBtn.sprite = spDone;
        levelCurrent.Onclick(index, id);
    }
}
