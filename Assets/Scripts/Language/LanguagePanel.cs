using System;
using UnityEngine;
using UnityEngine.UI;
using YaSDK.Source.Enum;
using YaSDK.Source.SDK;

//===============================================================
//Developer:  CuongCT
//Company:    ONESOFT
//Date:       2017
//================================================================
public class LanguagePanel : MonoBehaviour
{
   [SerializeField] private GridLayoutGroup parentGroup;
   [SerializeField] private LanguageItem sampleFlagButton;
   private LanguageItem[] _flagItems;
   private Action _changeLanguageAction, _closeAction;
   [SerializeField] private Button closeButton;

   #region Unity Method

   private void Start()
   {
      closeButton.onClick.AddListener(OnClickDismiss);
   }

   private void OnEnable()
   {
      //if (_flagItems != null) {
      //    for (var i = 0; i < _flagItems.Length; i++) {
      //        _flagItems[i].transform.DOScale(Vector3.one * 0.15f, 0.3f).SetDelay(Random.Range(0f, 0.7f))
      //            .From(false);
      //    }
      //}        
   }

   #endregion

   #region Public Methods

   public void Init(LanguageSpriteDictionary languageList, Action changeLanguageAction = null,
      Action closeAction = null, Action<LanguageItem> onInit = null)
   {
      LanguageItem languageFlag = null;
      _flagItems = new LanguageItem[languageList.Value.Count];
      int i = 0;
      foreach (var language in languageList.Value)
      {
         var newFlag = Instantiate(sampleFlagButton, parentGroup.transform, false);
         newFlag.Init(language, item =>
         {
            OnSelectFlagItem(item);
            OnClickLanguage(language.ToString());
         });
         newFlag.ActiveSelected(
            string.Compare(Localization.language, language.ToString(), StringComparison.Ordinal) == 0);

         newFlag.name = language.ToString();
         _flagItems[i] = newFlag;
         i++;

         if (IsUserLanguage(language))
            languageFlag = newFlag;
      }

      _changeLanguageAction = changeLanguageAction;
      _closeAction = closeAction;
      onInit?.Invoke(languageFlag);
   }

   public void OnClickLanguage(string language)
   {
      Localization.language = language;
      var uiLnaguage = FindObjectsOfType<LocalizeBehaviour>();
      for (var i = 0; i < uiLnaguage.Length; i++)
      {
         uiLnaguage[i].OnLocalize();
      }

      if (_changeLanguageAction != null)
         _changeLanguageAction();
      //GameUtils.RaiseMessage(new HomeMessages.LanguageChangeMessage());
      OnClickDismiss();
   }

   public void OnClickDismiss()
   {
      if (_closeAction != null)
         _closeAction();
   }

   #endregion

   #region Private Methods

   private void OnSelectFlagItem(LanguageItem item)
   {
      foreach (LanguageItem flagItem in _flagItems)
      {
         flagItem.ActiveSelected(flagItem == item);
      }
   }

   private bool IsUserLanguage(SystemLanguage language)
   {
      return
         YandexSDKData.Instance.EnvironmentData.Language.ToString() == language.ToString();
   }

   #endregion
}