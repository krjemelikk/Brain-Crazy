using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_167 : BaseLevel
{
    [SerializeField] private Button man;
    [SerializeField] private Button keTrom;
    [SerializeField] private Button ongGia;
    [SerializeField] private Button baGia;
    [SerializeField] private Button duaTre;

    [SerializeField] private GameObject dogObj;

    [SerializeField] private Sprite keTromRun;
    [SerializeField] private Sprite manRun;
    [SerializeField] private Transform posManRun;

    private bool isEnd;
    private bool isChoiceDog;

    protected override void Start()
    {
        base.Start();

        man.onClick.AddListener(CheckAnswer);
        keTrom.onClick.AddListener(WrongAnswer);
        ongGia.onClick.AddListener(WrongAnswer);
        baGia.onClick.AddListener(WrongAnswer);
        duaTre.onClick.AddListener(WrongAnswer);
    }

    public void ChoiceDog(bool isChoice)
    {
        isChoiceDog = isChoice;
    }

    protected override void Update()
    {
        if (isEnd)
            return;

        base.Update();

        if(isChoiceDog)
        {
            if(Vector2.Distance(keTrom.transform.position, dogObj.transform.position) <= 0.2f)
            {
                dogObj.SetActive(false);
                keTrom.GetComponent<Image>().sprite = keTromRun;
                man.GetComponent<Image>().sprite = manRun;
                man.GetComponent<Image>().SetNativeSize();
                man.transform.DOMove(posManRun.position, 0.2f).SetUpdate(true);
                isEnd = true;
            }
        }
    }

    private void CheckAnswer()
    {
        if(isEnd)
        {
            RightAnswer();
        }
        else
        {
            WrongAnswer();
        }
    }
}
