using UnityEngine;
using UnityEngine.UI;

public class Level_197 : BaseLevel
{
    [SerializeField] private Button blueDog;
    [SerializeField] private Button yellowDog;
    [SerializeField] private Sprite greenDogSpr;

    private bool isDone;

    protected override void Start()
    {
        base.Start();

        //blueDog.onClick.RemoveAllListeners();
        //blueDog.onClick.AddListener(OnClickDogDone);

        //yellowDog.onClick.RemoveAllListeners();
        //yellowDog.onClick.AddListener(OnClickDogDone);
    }

    public void OnEndDragDog(bool isBlueDog)
    {
        if (isDone)
            return;

        if (Vector2.Distance(blueDog.gameObject.transform.position, yellowDog.gameObject.transform.position) <= 0.2f)
        {
            if (isBlueDog)
            {
                blueDog.gameObject.SetActive(false);
                yellowDog.GetComponent<Image>().sprite = greenDogSpr;
            }
            else
            {
                yellowDog.gameObject.SetActive(false);
                blueDog.GetComponent<Image>().sprite = greenDogSpr;
            }

            isDone = true;
        }
    }

    public void OnClickDogDone()
    {
        if(isDone)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
}
