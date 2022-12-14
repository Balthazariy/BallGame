using Chebureck.Controllers;
using Chebureck.Managers;
using Chebureck.Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Chebureck.Scenes
{
    public class GameplayScene : MonoBehaviour
    {
        public static GameplayScene Instance { get; private set; }

        [SerializeField]
        private Button _pauseButton;

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            _pauseButton.onClick.AddListener(PauseButtonOnClickHandler);
        }

        #region Button Handlers
        private void PauseButtonOnClickHandler()
        {
            SoundManager.Instance.PlayClickSound();
            PauseController.Instance.ActivePause();
            PauseScreen.Instance.Show();
        }
        #endregion
    }
}