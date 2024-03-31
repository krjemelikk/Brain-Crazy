using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Level_88 : BaseLevel
{
    public Button[] btNodes;
    public GameObject[] imgONodes;
    public GameObject[] imgXNodes;
    private RectTransform theOtext;
    [SerializeField] private GameObject English;
    [SerializeField] private GameObject Vietnamese;
    [SerializeField] private GameObject Russian;
    [SerializeField] private GameObject Spanish;
    [SerializeField] private GameObject French;
    [SerializeField] private GameObject Japanese;
    [SerializeField] private GameObject Korean;
    [SerializeField] private GameObject Thai;
    [SerializeField] private GameObject Arabic;
    [SerializeField] private GameObject Chinese;
    [SerializeField] private GameObject Portuguese;
    [SerializeField] private GameObject German;
    [SerializeField] private GameObject Italian;
    [SerializeField] private GameObject Indonesian;
    [SerializeField] private GameObject Turkish;
    private Vector3 posStartOText;
    private int indexOtext = -1;
    private bool isCanDrag = true;
    private bool playerTurn = true;
    protected override void Start()
    {
        base.Start();
        switch (Localization.language)
        {
            case "English":
                English.SetActive(true);
                break;
            case "Vietnamese":
                Vietnamese.SetActive(true);
                break;
            case "Russian":
                Russian.SetActive(true);
                break;
            case "Spanish":
                Spanish.SetActive(true);
                break;
            case "French":
                French.SetActive(true);
                break;
            case "Japanese":
                Japanese.SetActive(true);
                break;
            case "Korean":
                Korean.SetActive(true);
                break;
            case "Thai":
                Thai.SetActive(true);
                break;
            case "Arabic":
                Arabic.SetActive(true);
                break;
            case "Chinese":
                Chinese.SetActive(true);
                break;
            case "Portuguese":
                Portuguese.SetActive(true);
                break;
            case "German":
                German.SetActive(true);
                break;
            case "Italian":
                Italian.SetActive(true);
                break;
            case "Indonesian":
                Indonesian.SetActive(true);
                break;
            case "Turkish":
                Turkish.SetActive(true);
                break;
            default:
                English.SetActive(true);
                break;
        }

        theOtext = GameObject.Find("theOtext").GetComponent<RectTransform>();
        posStartOText = theOtext.localPosition;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void StartLevel()
    {
        base.StartLevel();
    }

    public override void CompleteLevel()
    {
        base.CompleteLevel();
    }

    public override void WrongAnswer()
    {
        int index = 0;
        for (int i = 0; i < imgONodes.Length; i++)
        {
            if (!imgONodes[i].activeInHierarchy)
            {
                index = i;
                break;
            }
        }
        imgXNodes[index].SetActive(true);
        base.WrongAnswer();      
        StartCoroutine(RestartLevel());
    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void SetNode(int index)
    {
        if (index == indexOtext) return;

        if (playerTurn)
        {
            imgONodes[index].SetActive(true);
            playerTurn = false;
            CheckAnswer();
        }       
    }

    private void CheckAnswer()
    {
        if (isCanDrag && !playerTurn)
        {
            WrongAnswer();           
        }

        if(!isCanDrag && !playerTurn)
        {
            RightAnswer();
        }
    } 

    public void EndDrag()
    {
        if (!isCanDrag) return;

        float minDistance = 0.35f;
        int minNode = -1;

        for (int i = 0; i < btNodes.Length; i++)
        {
            var distance = Vector2.Distance(theOtext.position, btNodes[i].transform.position);
            Debug.Log(i + " / " + distance);
            if (distance > 1f) continue;

            if (distance < minDistance)
            {
                minDistance = distance;
                minNode = i;
            }
        }
        Debug.Log("minNode " + minNode);
        if (minNode >= 0)
        {
            isCanDrag = false;
            indexOtext = minNode;
            theOtext.position = btNodes[minNode].transform.position;
            theOtext.gameObject.GetComponent<EventTrigger>().enabled = false;
        }
        else
        {
            theOtext.localPosition = posStartOText;
        }
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(1f);

        theOtext.localPosition = posStartOText;
        isCanDrag = true;
        playerTurn = true;
        indexOtext = -1;
        for (int i = 0; i < imgONodes.Length; i++)
        {
            imgONodes[i].SetActive(false);
        }
        for (int i = 0; i < imgXNodes.Length; i++)
        {
            imgXNodes[i].SetActive(false);
        }
        theOtext.gameObject.GetComponent<EventTrigger>().enabled = true;
    }
}
