using Chebureck.Managers;
using Chebureck.Settings;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chebureck.Screens
{
    public class SettingsPage : MonoBehaviour
    {
        public static SettingsPage Instance { get; private set; }

        [SerializeField] private Button _closeButton;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _soundToggle;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            _closeButton.onClick.AddListener(CloseButtonOnClickhandler);
            _musicToggle.onValueChanged.AddListener(MusicToggleOnValueChangedHandler);
            _soundToggle.onValueChanged.AddListener(SoundToggleOnValueChangedHandler);
        }

        private void UpdateUIStates()
        {
            _musicToggle.isOn = SoundManager.Instance.MusicVolume == 1;
            _soundToggle.isOn = SoundManager.Instance.SoundVolume == 1;
        }

        #region Toggle Handlers
        private void MusicToggleOnValueChangedHandler(bool isOn)
        {
            int musicVolume = _musicToggle.isOn ? 1 : 0;

            SoundManager.Instance.PlayClickSound();
            SoundManager.Instance.MusicVolume = musicVolume;

            SaveSettingsToCache(musicVolume, (int)SoundManager.Instance.SoundVolume);
        }

        private void SoundToggleOnValueChangedHandler(bool isOn)
        {
            int soundVolume = _soundToggle.isOn ? 1 : 0;

            SoundManager.Instance.PlayClickSound();
            SoundManager.Instance.SoundVolume = soundVolume;

            SaveSettingsToCache((int)SoundManager.Instance.MusicVolume, soundVolume);
        }
        #endregion

        #region Button Handlers
        private void CloseButtonOnClickhandler()
        {
            SoundManager.Instance.PlayClickSound();

            Hide();

            MainPage.Instance.Show();
        }
        #endregion

        private void SaveSettingsToCache(int musicVolume, int soundVolume)
        {
            var cache = DataManager.Instance.CachedUserData;

            cache.volumeMusic = musicVolume;
            cache.volumeSound = soundVolume;

            DataManager.Instance.SaveCache(CacheTypeEnumerators.UserLocalData);
        }

        public void Show()
        {
            UpdateUIStates(); 

            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}