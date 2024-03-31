using System.Collections;
using UnityEngine;

public class Level_150 : BaseLevel
{
    [SerializeField] private Level_150_Cell[] cells;
    [SerializeField] private int[] idAnswers;

    public int currentIDCell;
   private int numCellClicked;

   [HideInInspector] public bool isHinting;

    public GameObject tabTut;

    protected override void Start()
    {
        base.Start();
        for (int i = 0; i < cells.Length; i++)
        {
            int index = i;
            cells[index].Init(this, index);
        }
        currentIDCell = 0;
       // cells[0].ActiveCell(true);
    }

   public void CheckAnswer()
    {
        //numCellClicked++;
        //if(numCellClicked >= 11)
        //{
        //    WrongAnswer();
        //    GameController.Instance.ResetLevel();
        //    return;

        //}
        if (currentIDCell == cells.Length - 1)
        {
            //Đã lên được sân thượng
            RightAnswer();
        }
    }

    public override void UseHint()
    {
        isHinting = true;
        StartCoroutine(UseHintHandle());
    }

    public IEnumerator UseHintHandle()
    {
        int index = 0;
        for (int i = 0; i < idAnswers.Length; i++)
        {
            if (cells[idAnswers[i]].isChosse)
                continue;

            cells[idAnswers[i]].iconCell.color = new Color(130f / 255, 1, 90f / 255);
            cells[idAnswers[i]].textNumHint.gameObject.SetActive(true);
            cells[idAnswers[i]].textNumHint.text = (index + 1).ToString();
            index += 1;
            yield return new WaitForSeconds(0.3f);
        }

        isHinting = false;
    }
}
