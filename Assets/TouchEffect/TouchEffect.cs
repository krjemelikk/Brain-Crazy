using UnityEngine;

public class TouchEffect : MonoBehaviour
{
	public ParticleSystem touchEffect;
    private Vector3 _effectPos;
    private static TouchEffect _instance;
    public static TouchEffect Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TouchEffect>();
                if (_instance != null)
                    DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    public void SetActive(bool isActive)
    {
		touchEffect.gameObject.SetActive(isActive);
    }
    // Update is called once per frame
    private void Update() {
        if (!Input.GetMouseButtonDown(0) || !(Time.timeScale > 0))
            return;
        if (Input.touchCount > 1)
            for (int i = 0; i < Input.touchCount; i++)
                EmitParticle(Input.GetTouch(i).position);
        else
            EmitParticle(Input.mousePosition);
    }

    private void EmitParticle(Vector3 position) {
        _effectPos = Camera.main.ScreenToWorldPoint(position);
        _effectPos.z = 0;
        touchEffect.Emit(new ParticleSystem.EmitParams() {position = _effectPos}, 1);
    }
}
