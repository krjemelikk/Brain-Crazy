using UnityEngine;
using DG.Tweening;

public class Level_155 : BaseLevel
{
    [SerializeField] private GameObject DoorObj;
    [SerializeField] private DragUI keyObj;
    [SerializeField] private GameObject treeObj;
    private bool isEnd;
    private int numShakeTree;

    protected override void Start()
    {
        base.Start();
        keyObj.transform.gameObject. SetActive(false);
        keyObj.isCanActive = false;
    }

    public void ShakeTree()
    {
        treeObj.transform.DOKill();
        treeObj.transform.rotation = Quaternion.Euler(0, 0, 0);
        treeObj.transform.DOPunchRotation(new Vector3(0,0,20), 0.3f);

        numShakeTree++;
        if(numShakeTree >= 3)
        {
            keyObj.transform.gameObject.SetActive(true);
            keyObj.transform.DOMoveY(-1.3f, 0.5f).OnComplete(() => { keyObj.isCanActive = true; });
        }
    }

    protected override void Update()
    {
        if (isEnd)
            return;

        base.Update();

        if (Vector2.Distance(DoorObj.transform.position, keyObj.transform.position) < 0.3f)
        {
            RightAnswer();
            isEnd = true;
        }
    }
}
