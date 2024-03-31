using UnityEngine;

public class LanguageBox : BaseBox
{
    [SerializeField] private LanguagePanel languagePanel;
    [SerializeField] private LanguageSpriteDictionary flagDict;
    
    private static GameObject instance;

    public static LanguageBox Setup()
    {
        if (instance == null)
        {
            // Create popup and attach it to UI
            instance = Instantiate(Resources.Load<GameObject>(PathPrefabs.LANGUAGE_BOX));
        }
        instance.SetActive(true);
        return instance.GetComponent<LanguageBox>();
    }

    private void Start() {
        languagePanel.Init(flagDict,null,OnClickCloseButton);        
    }

    protected override void OnStart() {
        languagePanel.gameObject.SetActive(true);
    }
}