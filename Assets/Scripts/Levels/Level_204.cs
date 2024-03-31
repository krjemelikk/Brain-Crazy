using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Level_204 : BaseLevel
{
    [SerializeField] private Level_204_Knife knifePrefab;
    private List<Level_204_Knife> lstKnifePool;

    private int currentScore;
    [SerializeField] private int maxScore;

    [SerializeField] private Text scoreTxt;
    [HideInInspector] public bool isEnd;
    [SerializeField] private Transform posStartKnife;

    private Level_204_Knife knifeObj;

    public Transform wood;

    protected override void Start()
    {
        base.Start();
        maxScore = 10;
        currentScore = 0;

        scoreTxt.text = currentScore + "/" + maxScore;
        knifeObj =  SpawnKnife();
    }

    protected override void Update()
    {
        RotateWood();

        if (isEnd)
            return;
        base.Update();
        if (EventSystem.current.currentSelectedGameObject)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            if (knifeObj != null)
            {
                knifeObj.MoveKnife();
                StartCoroutine(Helper.StartAction(() => { knifeObj = SpawnKnife(); }, 0.5f));
                knifeObj = null;
            }
        }
    }

    private void RotateWood()
    {
        wood.transform.Rotate(new Vector3(0, 0, 1));
    }

    public Level_204_Knife SpawnKnife()
    {
        Level_204_Knife knife = GetKnife();
        knife.Init(this);
        knife.transform.position = posStartKnife.position;
        return knife;
    }


    private Level_204_Knife GetKnife()
    {
        if (lstKnifePool == null)
            lstKnifePool = new List<Level_204_Knife>();

        for (int i = 0; i < lstKnifePool.Count; i++)
        {
            if (!lstKnifePool[i].gameObject.activeSelf)
            {
                lstKnifePool[i].gameObject.SetActive(true);
                return lstKnifePool[i];
            }
        }

        Level_204_Knife knife = Instantiate(knifePrefab, posStartKnife);
        lstKnifePool.Add(knife);
        return knife;
    }

    public void AddScore()
    {
        currentScore++;
        scoreTxt.text = currentScore + "/" + maxScore;

        if (currentScore >= maxScore)
        {
            RightAnswer();
            isEnd = true;
        }
    }
}
