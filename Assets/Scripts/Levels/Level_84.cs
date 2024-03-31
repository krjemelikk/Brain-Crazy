using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventDispatcher;

public class Level_84 : BaseLevel
{
    [SerializeField] private PosSpawnMeteorite[] posSpawnMeteorites;
    [SerializeField] private MeteoriteObj meteoritePrefab;
    private List<MeteoriteObj> poolMeteorite;
    [SerializeField] private Transform parentMeteorites;
    private bool isSpawnMeteorites;

    public RectTransform ProtectedTxt;
    public Dictionary<SystemLanguage, LocalizeAnswer> protectedsTxt;
    private Transform currentTransform;

    [SerializeField] private Collider2D earthObj;
    [SerializeField] private Transform shieldObj;

    [SerializeField] private GameObject panelRestartLevel;

    private Action<object> actionPlayerDie;

    private bool isStartedGame;
    [SerializeField] private GameObject handObj;

    [SerializeField] private Transform parrentHit;

    protected override void Start()
    {
        base.Start();
        actionPlayerDie = (sender) => { Lose(); };
        this.RegisterListener(EventID.METEORITE_COLLISION_EARTH, actionPlayerDie);
        shieldObj.gameObject.SetActive(false);
        isStartedGame = false;
        earthObj.enabled = true;
    }

    protected override void Update()
    {
        base.Update();

    }

    public void StartControllerEarth()
    {
        handObj.gameObject.SetActive(false);
        if (!isStartedGame)
            OnClickStart();
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
        base.WrongAnswer();

    }

    public override void RightAnswer()
    {
        base.RightAnswer();
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        this.RemoveListener(EventID.METEORITE_COLLISION_EARTH, actionPlayerDie);
    }

    #region Handle Protected
    private void CheckAnswer()
    {
        if (Vector2.Distance(earthObj.transform.position, currentTransform.transform.position) <= 0.2f)
        {
            earthObj.enabled = false;
            shieldObj.gameObject.SetActive(true);
            currentTransform.transform.parent = earthObj.transform;
            currentTransform.GetComponent<DragUI>().SetActiveDrag(false);
            if (!isStartedGame)
                OnClickStart();
            Win();
        }
    }

    public void BeginDrag(Transform tran)
    {
        currentTransform = tran;
    }

    public void EndDrag(RectTransform tran)
    {
        CheckAnswer();
    }
    #endregion

    public void OnClickStart()
    {
        isSpawnMeteorites = true;
        StartCoroutine(SpawnMeteorite());
    }

    public void Lose()
    {
        WrongAnswer();
        StartCoroutine(Helper.StartAction(() =>
        {
            isSpawnMeteorites = false;
            StopAllCoroutines();
            panelRestartLevel.SetActive(true);
        }, 1f));

    }

    public void Win()
    {
        StartCoroutine(Helper.StartAction(() =>
        {
            RightAnswer();
            isSpawnMeteorites = false;
            StopAllCoroutines();
        }, 3));
    }

    public void RestartHanle()
    {
        GameController.Instance.ResetLevel();
    }

    protected override void UpdateText()
    {
        if (txtName != null) txtName.text = $"{Localization.Get("lb_level")} {ID}";
        foreach (var item in protectedsTxt)
        {
            item.Value.Question.SetActive(false);
            if (string.Compare(Localization.language, item.Key.ToString(), StringComparison.Ordinal) == 0)
            {
                item.Value.Question.SetActive(true);
                ProtectedTxt = item.Value.Answer.transform.parent.GetComponent<RectTransform>();
                //if (txtQuestion != null) txtQuestion.text = Localization.Get(KeyQuestion);
            }
        }

    }

    #region Spawn Thiên thạch
    public IEnumerator SpawnMeteorite()
    {
        float timeDelay = 0.5f;
        isStartedGame = true; ;
        while (isSpawnMeteorites)
        {
            MeteoriteObj meteorite = GetMeteorite();
            meteorite.gameObject.SetActive(true);

            int indexRandom = UnityEngine.Random.Range(0, posSpawnMeteorites.Length);
            meteorite.SetMove(posSpawnMeteorites[indexRandom].typePosSpawn, posSpawnMeteorites[indexRandom].transform.position, parrentHit);
            timeDelay -= 0.05f;
            if (timeDelay < 0.05f)
                timeDelay = 0.05f;
            yield return new WaitForSeconds(timeDelay);
        }
    }

    private MeteoriteObj GetMeteorite()
    {
        if (poolMeteorite == null)
            poolMeteorite = new List<MeteoriteObj>();

        foreach (var item in poolMeteorite)
        {
            if (!item.gameObject.activeSelf)
                return item;
        }

        MeteoriteObj meteorite = Instantiate<MeteoriteObj>(meteoritePrefab, parentMeteorites);
        poolMeteorite.Add(meteorite);
        return meteorite;
    }
    #endregion
}