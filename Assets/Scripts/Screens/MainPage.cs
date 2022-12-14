using Chebureck.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Chebureck.Screens
{
    public class MainPage : MonoBehaviour
    {
        public static MainPage Instance { get; private set; }

        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            _playButton.onClick.AddListener(PlayButtonOnClickHandler);
            _settingsButton.onClick.AddListener(SettingButtonOnClickHandler);

            SoundManager.Instance.PlaySound(Settings.SoundEnumerators.Background);

            Show();

            SettingsPage.Instance.Hide();
        }

        #region Button Handlers
        private void PlayButtonOnClickHandler()
        {
            SoundManager.Instance.PlayClickSound();
            GameplayManager.Instance.StartGameplay();

            CommonSceneManager.Instance.LoadSceneByName(Settings.SceneNameEnumerators.LoadingScreenScene, true, Settings.SceneNameEnumerators.GameplayScene);
            CommonSceneManager.Instance.OnAsycnOperationCompleteEvent += OnAsycnOperationCompleteEventEvent;
        }

        private void SettingButtonOnClickHandler()
        {
            SoundManager.Instance.PlayClickSound();
            Hide();

            SettingsPage.Instance.Show();
        }
        #endregion

        private void OnAsycnOperationCompleteEventEvent()
        {
            CommonSceneManager.Instance.OnAsycnOperationCompleteEvent -= OnAsycnOperationCompleteEventEvent;

            CommonSceneManager.Instance.OpenLoadedScene();
        }

        public void Show()
        {
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