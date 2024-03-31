using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level_99 : BaseLevel
{
    public enum StateZombie
    {
        Patrol = 0,
        WatchRadio = 1,
        LookPlayer = 2,
        Attack = 3,
        Idle = 4,
        None = 100
    }

    [Header("Zombie")]
    [SerializeField] private SkeletonGraphic[] zombies;
    [SerializeField] private float speedPatrolZombie;
    [SerializeField] private float speedMoveToPlayerZombie;
    [SerializeField] private float speedMoveToRadioZombie;
    [SerializeField] private Transform[] checkPointZombie;
    [SerializeField] private Transform posPlayer;
    [SerializeField] private Transform posRadio;
    private int currentCheckPointZombie;
    private StateZombie stateZombie;
    private float timerIdle;
    private float timerAttack;
    private float randomTimeIdle;
    [SerializeField] private float minRandomTimeIdle;
    [SerializeField] private float maxRandomTimeIdle;
    private bool zombieEndState;

    [Header("Player")]
    [SerializeField] private SkeletonGraphic player;
    [SerializeField] private float speedOpenGate;
    private float percentOpenGate;
    [SerializeField] private bool isOpenGate;
    private float randomPercentGate;
    private bool isClickedRadio;

    [Header("Object")]
    [SerializeField] private GameObject gateObj;
    [SerializeField] private GameObject onRadioObj;
    [SerializeField] private GameObject radioMusicObj;
    [SerializeField] private GameObject tabTutObj;
    [SerializeField] private GameObject lockObj;

    [Header("UI")]
    [SerializeField] private Image progessBar;
    [SerializeField] private Image valueBar;
    [SerializeField] private Text valueBarTxt;
    [SerializeField] private GameObject panelRestartLevel;

    private bool isRight;
    private bool isTuted;

    protected override void Start()
    {
        base.Start();
        isRight = false;

        currentCheckPointZombie = 0;
        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i].AnimationState.SetAnimation(0, "run", true);
        }
        stateZombie = StateZombie.Patrol;
        panelRestartLevel.SetActive(false);
        isOpenGate = false;

        tabTutObj.SetActive(true);

        player.AnimationState.SetAnimation(0, "sit", true);
    }

    protected override void Update()
    {
        if (isRight)
            return;

        base.Update();
        StateZombieHandle();
        OpenGateHandle();
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
        isRight = true;
    }

    public override void UseHint()
    {
        base.UseHint();
    }

    public void RestartHanle()
    {
        GameController.Instance.ResetLevel();
    }

    public void OpenRadio()
    {
        if (isClickedRadio)
            return;
        if (stateZombie == StateZombie.Attack)
            return;

        onRadioObj.SetActive(true);
        stateZombie = StateZombie.WatchRadio;
        for (int k = 0; k < zombies.Length; k++)
        {
            zombies[k].gameObject.transform.localScale = new Vector3(-0.5f, zombies[k].transform.localScale.y, zombies[k].transform.localScale.z);
            zombies[k].AnimationState.SetAnimation(0, "run", true);
        }

        radioMusicObj.SetActive(true);

        isClickedRadio = true;
    }

    #region Player
    private void OpenGateHandle()
    {
        if (!isOpenGate)
            return;
        if (isRight)
            return;
        if (stateZombie == StateZombie.LookPlayer || stateZombie == StateZombie.Attack)
            return;

        percentOpenGate += speedOpenGate * Time.deltaTime;

        isTuted = true;

        tabTutObj.SetActive(false);

        progessBar.gameObject.SetActive(true);
        valueBar.fillAmount = percentOpenGate;
        valueBarTxt.text = (Helper.DecimalRounding(percentOpenGate * 100, 2)).ToString() + " %";

        if (stateZombie != StateZombie.WatchRadio)
        {
            if (percentOpenGate >= randomPercentGate)
            {
                for (int i = 0; i < zombies.Length; i++)
                {
                    zombies[i].transform.localScale = new Vector3(0.5f, zombies[i].transform.localScale.y, zombies[i].transform.localScale.z);
                }

                stateZombie = StateZombie.LookPlayer;
                return;
            }
        }
        else
        {
            if (percentOpenGate >= 1)
            {
                progessBar.gameObject.SetActive(false);
                lockObj.gameObject.SetActive(false);
                PlayerOpenedGate();
                isRight = true;
            }
        }
    }

    public void OpenGate()
    {
        isOpenGate = true;
        player.AnimationState.SetAnimation(0, "open", true);
        randomPercentGate = Random.Range(0.7f, 0.8f);
    }

    public void OnPointerUp()
    {
        isOpenGate = false;
        player.AnimationState.SetAnimation(0, "sit", true);
        //player.AnimationState.ClearTracks();
    }

    public void PlayerOpenedGate()
    {
        gateObj.transform.DOLocalMoveY(-286f, 0.5f).OnComplete(() =>
        {
            player.AnimationState.SetAnimation(0, "run2", true);
            player.gameObject.transform.DOLocalMoveX(500, 0.5f).OnComplete(() => { RightAnswer(); });
        });
    }
    #endregion

    #region Zombie
    public void StateZombieHandle()
    {
        if (zombieEndState)
            return;
        switch (stateZombie)
        {
            case StateZombie.Patrol:
                ZombiePatrol();
                break;
            case StateZombie.LookPlayer:
                ZombieLookPlayer();
                break;
            case StateZombie.Attack:
                ZombieAttack();
                break;
            case StateZombie.WatchRadio:
                ZombieWatchRadio();
                break;
            case StateZombie.Idle:
                ZombieIdle();
                break;
        }
    }

    private void ZombieIdle()
    {
        ZombieFindPlayer();
        timerIdle += Time.deltaTime;

        if (timerIdle >= randomTimeIdle)
        {
            currentCheckPointZombie++;
            if (currentCheckPointZombie >= checkPointZombie.Length)
                currentCheckPointZombie = 0;

            for (int i = 0; i < zombies.Length; i++)
            {
                zombies[i].AnimationState.SetAnimation(0, "run", true);
            }

            stateZombie = StateZombie.Patrol;
        }
    }

    private void ZombiePatrol()
    {
        // Debug.Log("currentCheckPointZombie " + currentCheckPointZombie);
        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i].transform.position = Vector3.MoveTowards(zombies[i].transform.position, checkPointZombie[currentCheckPointZombie].position, speedPatrolZombie * Time.deltaTime);
            if (zombies[i].transform.position.x <= checkPointZombie[currentCheckPointZombie].position.x)
            {
                zombies[i].transform.localScale = new Vector3(0.5f, zombies[i].transform.localScale.y, zombies[i].transform.localScale.z);
                if (!isTuted)
                {
                    tabTutObj.SetActive(false);
                }
            }
            else
            {
                zombies[i].transform.localScale = new Vector3(-0.5f, zombies[i].transform.localScale.y, zombies[i].transform.localScale.z);
                if (!isTuted)
                {
                    tabTutObj.SetActive(true);
                }
            }

            if (Vector2.Distance(zombies[i].transform.position, checkPointZombie[currentCheckPointZombie].position) <= 0.2f)
            {
                stateZombie = StateZombie.Idle;
                timerIdle = 0;
                randomTimeIdle = Random.Range(minRandomTimeIdle, maxRandomTimeIdle);
                for (int k = 0; k < zombies.Length; k++)
                {
                    zombies[k].AnimationState.SetAnimation(0, "idle", true);
                }
                return;
            }
        }

        ZombieFindPlayer();
    }

    private void ZombieAttack()
    {
        timerAttack += Time.deltaTime;
        if (timerAttack >= 1.5f)
        {
            WrongAnswer();
            panelRestartLevel.SetActive(true);
            stateZombie = StateZombie.None;
        }
    }

    private void ZombieLookPlayer()
    {
        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i].transform.position = Vector3.MoveTowards(zombies[i].transform.position, posPlayer.position, speedMoveToPlayerZombie / 2f * Time.deltaTime);
            if (Vector2.Distance(zombies[i].transform.position, posPlayer.position) < 0.3f)
            {
                stateZombie = StateZombie.Attack;
                timerAttack = 0;
                for (int k = 0; k < zombies.Length; k++)
                {
                    zombies[k].AnimationState.SetAnimation(0, "attack", true);
                }
                player.AnimationState.SetAnimation(0, "get_hit", true);
                return;
            }
        }
    }

    private void ZombieWatchRadio()
    {
        for (int i = 0; i < zombies.Length; i++)
        {
            zombies[i].transform.position = Vector3.MoveTowards(zombies[i].transform.position, posRadio.position, speedMoveToRadioZombie * Time.deltaTime);
            if (Vector2.Distance(zombies[i].transform.position, posRadio.position) < 0.3f)
            {
                zombieEndState = true;
                for (int k = 0; k < zombies.Length; k++)
                {
                    zombies[k].AnimationState.SetAnimation(0, "attack", true);
                }
                return;
            }
        }
    }

    private void ZombieFindPlayer()
    {
        for (int i = 0; i < zombies.Length; i++)
        {
            if (zombies[i].transform.localScale.x > 0)//Đang quay mặt về player
            {
                if (isOpenGate)//Nếu player đang mở cổng
                {
                    for (int k = 0; k < zombies.Length; k++)
                        zombies[k].AnimationState.SetAnimation(0, "run", true);
                    stateZombie = StateZombie.LookPlayer;
                    return;
                }
            }
        }
    }
    #endregion
}
