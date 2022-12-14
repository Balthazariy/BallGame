using Chebureck.Controllers;
using Chebureck.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Chebureck.Screens
{
    public class PauseScreen : MonoBehaviour
    {
        public static PauseScreen Instance { get; private set; }

        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Button _closeButton;

        [SerializeField] private CanvasGroup _canvasGroup;

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            CloseButtonOnClickHandler();

            _backToMenuButton.onClick.AddListener(BackToMenuButtonOnClickHandler);
            _closeButton.onClick.AddListener(CloseButtonOnClickHandler);
        }

        #region Button Handlers

        private void BackToMenuButtonOnClickHandler()
        {
            SoundManager.Instance.PlayClickSound();
            PauseController.Instance.DiactivatePause();
            GameplayManager.Instance.StopGameplay();

            CommonSceneManager.Instance.LoadSceneByName(Settings.SceneNameEnumerators.LoadingScreenScene, true, Settings.SceneNameEnumerators.MainMenuScene);
            CommonSceneManager.Instance.OnAsycnOperationCompleteEvent += OnAsycnOperationCompleteEventEvent;
        }

        private void CloseButtonOnClickHandler()
        {
            SoundManager.Instance.PlayClickSound();
            PauseController.Instance.DiactivatePause();

            Hide();
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