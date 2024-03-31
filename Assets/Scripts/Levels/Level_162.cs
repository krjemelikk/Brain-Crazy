using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Level_162 : BaseLevel
{
    public Image chestImg;
    public Sprite iconChestOpen;

    [Header("Answers")]
    public Button btOK;
    public InputField inputField;

    private int resultAnswer;
    private bool isEnd;

    private Vector3 dir;

    public RectTransform EndKeyMove;
    public RectTransform TransformKey;

    protected override void Start()
    {
        base.Start();
        btOK.onClick.AddListener(() => CheckAnswer());
        resultAnswer = 12;
    }

    protected override void Update()
    {
        base.Update();
        Acceleration();
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
            chestImg.sprite = iconChestOpen;
            TransformKey.gameObject.SetActive(true);
            TransformKey.DOLocalMove(EndKeyMove.localPosition, 0.5f).SetEase(Ease.Linear);
        }
    }

    private void CheckAnswer()
    {
        if (!isEnd)
        {
            WrongAnswer();
            return;
        }

        int _result = 0;
        if (string.IsNullOrEmpty(inputField.text) || !int.TryParse(inputField.text, System.Globalization.NumberStyles.Integer, null, out _result))
        {
            WrongAnswer();
            return;
        }

        if (_result == resultAnswer) RightAnswer();
        else WrongAnswer();
    }
}
