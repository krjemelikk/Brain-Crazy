using UnityEngine;
using UnityEngine.UI;
public class Level_150_Cell : MonoBehaviour
{
 
    public int[] idConnect;
    public int ID;
    [HideInInspector] public Image iconCell;
    private Button btnCell;

    private Level_150 controller;
    [HideInInspector] public bool isChosse;

    [HideInInspector] public Text textNumHint;

    public void Init(Level_150 controller, int id)
    {
        this.ID = id;
        this.controller = controller;

        iconCell = this.GetComponent<Image>();
        btnCell = this.GetComponent<Button>();

        if (ID == 0)
            btnCell.onClick.AddListener(() => ActiveCell(true));
        else
            btnCell.onClick.AddListener(() => ActiveCell());

        textNumHint = this.transform.GetChild(0).gameObject.GetComponent<Text>();
        textNumHint.gameObject.SetActive(false);
    }

    public void ActiveCell(bool activeNow = false)
    {
        if (isChosse)
            return;
        if (controller.isHinting)
            return;
        controller.tabTut.gameObject.SetActive(false);

        if (activeNow)
        {
            isChosse = true;
            iconCell.color = new Color(1, 224f / 255, 0);
        }
        else
        {
            bool isHasConnect = false;
            //Check Condition
            for (int i = 0; i < idConnect.Length; i++)
            {
                if (controller.currentIDCell == idConnect[i])
                {
                    //Has Connect
                    isHasConnect = true;
                    iconCell.color = new Color(1, 224f / 255, 0);
                    isChosse = true;
                    controller.currentIDCell = ID;
                    controller.CheckAnswer();
                    break;
                }
            }

            if (!isHasConnect)
            {
                controller.WrongAnswer();
                GameController.Instance.ResetLevel();
            }
        }
    }

}
