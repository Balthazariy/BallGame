using Chebureck.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chebureck.Screens
{
    public class GameOverPage : MonoBehaviour
    {
        public static GameOverPage Instance { get; private set; }

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _scoreValueText;

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            _closeButton.onClick.AddListener(CloseButtonOnClickHandler);
            _restartButton.onClick.AddListener(RestartButtonOnClickHadler);

            SetScoreValueText(0);
        }

        #region Button Handlers
        private void CloseButtonOnClickHandler()
        {
            SoundManager.Instance.PlayClickSound();
            CommonSceneManager.Instance.LoadSceneByName(Settings.SceneNameEnumerators.LoadingScreenScene, true, Settings.SceneNameEnumerators.MainMenuScene);
            CommonSceneManager.Instance.OnAsycnOperationCompleteEvent += OnAsycnOperationCompleteEventEvent;
        }

        private void RestartButtonOnClickHadler()
        {
            SoundManager.Instance.PlayClickSound();
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

        public void SetScoreValueText(int score) => _scoreValueText.text = score.ToString();
    }
}