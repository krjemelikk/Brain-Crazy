using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class Torch
{
    [HideInInspector] public bool isFired;
    public Transform posSetFire;//Vị trí châm lửa
    public GameObject fire;
}

public class Level_42 : BaseLevel
{
    public Torch[] torchs;
    public GameObject fireObj;

    private bool isEnd;
    private bool isFired;
    private int indexFired;

    public void OnDragFire()
    {
        if (isFired)
            return;

        for (int i = 0; i < torchs.Length; i++)
        {
            int index = i;
            if (torchs[index].isFired)
                continue;
            if (Vector2.Distance(torchs[index].posSetFire.position, fireObj.transform.position) <= 0.2f)
            {
                torchs[index].isFired = true;
                torchs[index].fire.SetActive(true);
                fireObj.SetActive(false);
                isFired = true;
                indexFired = i;
                return;
            }
        }
    }

    private void Acceleration()
    {
        if (isEnd) return;
        if (!isFired) return;

        Vector3 dir = Vector3.zero;
        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;
        dir.z = Input.acceleration.z;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        ControllFire(indexFired, dir);
    }

    public void ControllFire(int indexTorch, Vector3 dir)
    {
        switch(indexTorch)
        {
            case 0:
                if (torchs[indexTorch].isFired)
                {
                    if (dir.x >= 0.5f)
                    {
                        isEnd = true;
                        torchs[0].fire.transform.DOKill();
                        torchs[0].fire.transform.DORotate(new Vector3(0, 0, -90), 0.5f).OnComplete(() =>
                        {
                            torchs[1].fire.SetActive(true);
                            torchs[2].fire.SetActive(true);
                            torchs[0].fire.transform.DORotate(new Vector3(0, 0, 0), 0.5f).OnComplete(() => { WrongAnswer(); GameController.Instance.ResetLevel(); });
                        });
                    }
                    else if (dir.x <= -0.5f)
                    {
                        torchs[0].fire.transform.DOKill();
                        torchs[0].fire.transform.DORotate(new Vector3(0, 0, 90), 0.5f);
                    }
                    else
                    {
                        torchs[0].fire.transform.DOKill();
                        torchs[0].fire.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                    }
                }
                break;
            case 1:
                if (torchs[1].isFired)
                {
                    if (dir.x <= -0.5f)
                    {
                        isEnd = true;
                        torchs[1].fire.transform.DOKill();
                        torchs[1].fire.transform.DORotate(new Vector3(0, 0, 90), 0.5f).OnComplete(() =>
                        {
                            torchs[0].fire.SetActive(true);
                            torchs[2].fire.SetActive(false);
                            torchs[1].fire.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                            RightAnswer();
                        });
                    }
                    else if (dir.x >= 0.5f)
                    {
                        torchs[1].fire.transform.DOKill();
                        torchs[1].fire.transform.DORotate(new Vector3(0, 0,-90), 0.5f).OnComplete(() =>
                        {
                            torchs[0].fire.SetActive(false);
                            torchs[2].fire.SetActive(true);
                            torchs[1].fire.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                            RightAnswer();
                        });
                    }
                }
                break;
            case 2:
                if (torchs[2].isFired)
                {
                    if (dir.x <= -0.5f)
                    {
                        isEnd = true;
                        torchs[2].fire.transform.DOKill();
                        torchs[2].fire.transform.DORotate(new Vector3(0, 0, 90), 0.5f).OnComplete(() =>
                        {
                            torchs[0].fire.SetActive(true);
                            torchs[1].fire.SetActive(true);
                            torchs[2].fire.transform.DORotate(new Vector3(0, 0, 0), 0.5f).OnComplete(()=> { WrongAnswer(); GameController.Instance.ResetLevel(); });
                        });
                    }
                    else if (dir.x >= 0.5f)
                    {
                        torchs[2].fire.transform.DOKill();
                        torchs[2].fire.transform.DORotate(new Vector3(0, 0, -90), 0.5f);
                    }
                    else
                    {
                        torchs[2].fire.transform.DOKill();
                        torchs[2].fire.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                    }
                }
                break;
        }
        
    }

    protected override void Update()
    {
        base.Update();

        Acceleration();
    }
}
