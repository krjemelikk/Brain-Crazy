using UnityEngine;
using UnityEngine.UI;
using EventDispatcher;

public class LevelButton : MonoBehaviour
{
    [SerializeField] public int ID;
    [SerializeField] private Text txtName;
    [SerializeField] private Image imgState;
    [SerializeField] private Sprite[] sprState;
    [SerializeField] private Button clickOnLevelBtn;

    [SerializeField] private GameObject activeObj;
    [SerializeField] private GameObject commingObj;

    private void Start()
    {
        UpdateText();
        GetComponent<Button>().onClick.AddListener(OnClick);
        this.RegisterListener(EventID.CHANGE_LANGUAGE, (param) => UpdateText());

        //activeObj.SetActive(true);
        //commingObj.SetActive(false);
    }

    private void OnEnable()
    {
        CheckState();
    }

    public void OnClick()
    {
        if(!DataManager.GetLevelUnlocked(ID))
        {
            return;
        }
        
        GameController.Instance.StartLevel(ID);
    }

    private void CheckState()
    {
        if(!DataManager.GetLevelUnlocked(ID))
        {
            //imgState.sprite = sprState[0];
            imgState.gameObject.SetActive(false);
        }
        else
        {
            imgState.gameObject.SetActive(true);
            if (DataManager.GetLevelPassed(ID))
            {
                imgState.sprite = sprState[2];
            }
            else
            {
                imgState.sprite = sprState[1];
            }
        }
    }

    private void UpdateText()
    {
        txtName.text = $"{Localization.Get("lb_level")} {ID}";
    }

    public void CommingSoonLevel()
    {
        activeObj.SetActive(false);
        commingObj.SetActive(true);

        clickOnLevelBtn.interactable = false;
    }
}
