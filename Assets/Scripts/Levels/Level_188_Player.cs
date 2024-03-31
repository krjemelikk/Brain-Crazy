using UnityEngine;
using UnityEngine.UI;

public class Level_188_Player : MonoBehaviour
{
    public Level_188 level_Current;

    private Button btn;
    public int ID;

    private void Start()
    {
        btn = this.GetComponent<Button>();
        btn.onClick.AddListener(()=>Check());
    }

    public void Check()
    {
        level_Current.CheckRightAnswer(ID);
        btn.enabled = false;

    }
}
