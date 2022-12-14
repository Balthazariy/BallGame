using Chebureck.Managers;
using Chebureck.Utilities;
using UnityEngine;

namespace Chebureck.Scenes
{
    public class SplashScene : MonoBehaviour
    {
        [SerializeField]
        private OnBehaviourHandler _onBehaviourHandler;

        private void Awake()
        {
            _onBehaviourHandler.OnAnimationStringEvent += OnAnimationStringEventHandler;
        }

        private void OnAnimationStringEventHandler(string value)
        {
            switch (value)
            {
                case "Start":
                    CommonSceneManager.Instance.LoadSceneByName(Settings.SceneNameEnumerators.LoadingScreenScene, true, Settings.SceneNameEnumerators.MainMenuScene);
                    break;
                case "End":
                    CommonSceneManager.Instance.OpenLoadedScene();
                    break;
            }

        }
    }
}