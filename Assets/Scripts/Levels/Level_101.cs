using System.Collections.Generic;
using UnityEngine;

public class Level_101 : BaseLevel
{
    public bool isPlayerDie;

    private int numObjectPass;
    private int numObjectSpawn;
    [SerializeField] private List<Level_101_Object> objectPrefab;

    [SerializeField] private Level_101_Player player;
    [SerializeField] private GameObject restatPanel;

    private float timer;
    private float randomTimeSpawn;

    private bool isDone;

    protected override void Start()
    {
        base.Start();
        randomTimeSpawn = 0.7f;
        player.Init(this);
    }

    protected override void Update()
    {
        base.Update();
        SpawnObject();
    }

    public void PlayerDie()
    {
        isPlayerDie = true;
        restatPanel.SetActive(true);
    }
    
    public void RestartLevel()
    {
        GameController.Instance.ResetLevel();
    }

    private void SpawnObject()
    {
        if (isDone)
            return;

        if (numObjectSpawn >= objectPrefab.Count)
            return;

        timer += Time.deltaTime;
        
        if(timer >= randomTimeSpawn)
        {
            objectPrefab[numObjectSpawn].gameObject.SetActive(true);
            objectPrefab[numObjectSpawn].Init(this);
            randomTimeSpawn = 1.1f;
            numObjectSpawn++;
            timer = 0;
        }
    }

    public void CheckDoneLevel()
    {
        numObjectPass++;
        if(numObjectPass >= objectPrefab.Count - 1)
        {
            RightAnswer();
            isDone = true;
        }
    }
}
