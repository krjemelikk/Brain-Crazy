using UnityEngine;

public class Level_85 : BaseLevel
{
    private bool isEnd = false;
    private Vector3 dir;

    [SerializeField] private GameObject[] batsFake;
    [SerializeField] private GameObject batReal;

    protected override void Update()
    {
        base.Update();
        Acceleration();
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
            RightHandle();
#endif
    }

    private void Acceleration()
    {
        if (isEnd) return;

        dir.x = Input.acceleration.x;
        dir.y = Input.acceleration.y;
        dir.z = Input.acceleration.z;

        if (dir.sqrMagnitude > 1)
            dir.Normalize();

        if (dir.y >= 0.9f)
        {
            isEnd = true;
            RightHandle();
        }
    }

    public void ChoiceWrong()
    {
        WrongAnswer();
    }

    public void ChoiceBatReal()
    {
        RightAnswer();
    }

    private void RightHandle()
    {
        batReal.SetActive(true);
        batsFake[0].SetActive(false);
    }
}
