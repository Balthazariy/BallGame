using Chebureck.Objects.ScriptableObjects;
using Chebureck.Settings;
using Chebureck.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chebureck.Managers
{
    public class LocalizationManager : MonoBehaviour
    {
        public static LocalizationManager Instance { get; private set; }

        public event Action<LanguageEnumerators> LanguageWasChangedEvent;

        private LocalizationData.LocalizationLanguageData _currentLocalizationLanguageData;

        public Dictionary<SystemLanguage, LanguageEnumerators> SupportedLanguages { get; private set; }
        public LanguageEnumerators CurrentLanguage { get; private set; }
        public LanguageEnumerators DefaultLanguage { get; private set; }
        public LocalizationData LocalizationData { get; private set; }

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            LocalizationData = LoadObjectsManager.Instance.GetObjectByPath<LocalizationData>("ScriptableObjects/LocalizationData");

            DefaultLanguage = LocalizationData.defaultLanguage;
            CurrentLanguage = LanguageEnumerators.Unknown;

            FillLanguages();

            DataManager.Instance.CachedDataLoadedEvent += CachedDataLoadedEventHandler;
        }

        public void SetLanguage(LanguageEnumerators language, bool forceUpdate = false)
        {
            if (language == CurrentLanguage && !forceUpdate)
                return;

            if (SupportedLanguages.ContainsValue(language))
            {
                CurrentLanguage = language;
                _currentLocalizationLanguageData = LocalizationData.languages.Find(item => item.language == CurrentLanguage);
            }

            DataManager.Instance.CachedUserData.appLanguage = CurrentLanguage;
            DataManager.Instance.SaveCache(CacheTypeEnumerators.UserLocalData);

            if (LanguageWasChangedEvent != null)
                LanguageWasChangedEvent(CurrentLanguage);
        }

        public string GetUITranslation(string key)
        {
            if (_currentLocalizationLanguageData == null)
                return key;

            var localizedText = _currentLocalizationLanguageData.localizedTexts.
                Find(item => InternalTools.ReplaceLineBreaks(item.key) == InternalTools.ReplaceLineBreaks(key));

            if (localizedText == null)
                return key;
            return localizedText.value;
        }

        private void ApplyLocalization()
        {
            if (!SupportedLanguages.ContainsKey(Application.systemLanguage))
            {
                if (DataManager.Instance.CachedUserData.appLanguage == LanguageEnumerators.Unknown)
                    SetLanguage(DefaultLanguage, true);
                else
                    SetLanguage(DataManager.Instance.CachedUserData.appLanguage, true);
            }
            else
            {
                if (DataManager.Instance.CachedUserData.appLanguage == LanguageEnumerators.Unknown)
                    SetLanguage(SupportedLanguages[Application.systemLanguage], true);
                else
                    SetLanguage(DataManager.Instance.CachedUserData.appLanguage, true);
            }
        }

        private void FillLanguages()
        {
            SupportedLanguages = new Dictionary<SystemLanguage, LanguageEnumerators>();

            var supportedLanguages = LocalizationData.languages.Select(item => item.language).ToArray();
            foreach (var item in supportedLanguages)
            {
                if (Enum.TryParse(item.ToString(), out SystemLanguage result))
                {
                    SupportedLanguages.Add(result, item);
                }
                else
                {
                    Utilities.Logger.Log($"Cannot parse unsupported localziation language: {item}", LogTypeEnumerators.Warning);
                }
            }
        }

        private void CachedDataLoadedEventHandler()
        {
            ApplyLocalization();
        }
    }
}