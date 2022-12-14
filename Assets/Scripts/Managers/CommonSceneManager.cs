using Chebureck.Settings;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Chebureck.Managers
{
    public class CommonSceneManager : MonoBehaviour
    {
        public static CommonSceneManager Instance { get; private set; }

        private AsyncOperation _currentAsycnOperation;

        [HideInInspector]
        public event Action OnAsycnOperationCompleteEvent;
        [HideInInspector]
        public event Action<SceneNameEnumerators> OnSceneLoadedEvent;

        [HideInInspector]
        public SceneNameEnumerators AimSceneNameAfterLoading;
        private SceneNameEnumerators _currentSceneName = SceneNameEnumerators.Unknown;

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;

            LoadSceneByName(SceneNameEnumerators.SplashScreenScene);
        }

        public void OpenLoadedScene(bool isAutoOpen = true)
        {
            if (_currentAsycnOperation == null) throw new Exception("Try to open scene that don't loaded");

            if (isAutoOpen)
                _currentAsycnOperation.allowSceneActivation = true;

            // todo expand auto open scene logic
            OnSceneLoadedEvent?.Invoke(_currentSceneName);
            _currentAsycnOperation = null;
        }

        public void LoadSceneByName(SceneNameEnumerators sceneName, bool isLoading = false, SceneNameEnumerators aimSceneName = SceneNameEnumerators.MainMenuScene)
        {
            StartCoroutine(LoadScene(sceneName.ToString()));
            _currentSceneName = sceneName;
            if (isLoading)
                AimSceneNameAfterLoading = aimSceneName;
        }

        IEnumerator LoadScene(string sceneName)
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            _currentAsycnOperation = asyncOperation;
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                if (asyncOperation.progress >= 0.9f)
                {
                    OnAsycnOperationCompleteEvent?.Invoke();
                    break;
                }

                yield return null;
            }
        }
    }
}