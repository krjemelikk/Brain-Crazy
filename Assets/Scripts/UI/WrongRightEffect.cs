using UnityEngine;

public class WrongRightEffect : MonoBehaviour
{
    public ParticleSystem wrongEffect;
    public ParticleSystem rightEffect;
    private Vector3 _effectPos;
    private static WrongRightEffect _instance;
    public static WrongRightEffect Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WrongRightEffect>();
                //if (_instance != null)
                //    DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    public void Wrong()
    {
        _effectPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _effectPos.z = 0;
        wrongEffect.Emit(new ParticleSystem.EmitParams() { position = _effectPos }, 1);
    }

    public void Right()
    {
        Vector3 effectPos = Vector3.zero;
        Vector3 inputPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        Debug.Log(inputPos + " " + GameController.Instance.HomeScene.BoundLeft.position + " " + GameController.Instance.HomeScene.BoundTop.position);
        if (inputPos.x <= GameController.Instance.HomeScene.BoundLeft.position.x
            || inputPos.x >= GameController.Instance.HomeScene.BoundRight.position.x
            || inputPos.y <= GameController.Instance.HomeScene.BoundBottom.position.y
            || inputPos.y >= GameController.Instance.HomeScene.BoundTop.position.y)
            _effectPos = Vector3.zero;
        else
            _effectPos = inputPos;
        _effectPos.z = 0;
        rightEffect.Emit(new ParticleSystem.EmitParams() { position = _effectPos }, 1);
    }
}
