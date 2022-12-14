using Chebureck.Managers;
using Chebureck.Utilities;
using UnityEngine;

namespace Chebureck.Scenes
{
    public class LoadingScene : MonoBehaviour
    {
        public static LoadingScene Instance { get; private set; }

        [SerializeField]
        private Animator _sceneAnimator;

        [SerializeField]
        private OnBehaviourHandler _onBehaviourHandler;

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;

            _onBehaviourHandler.OnAnimationStringEvent += OnAnimationStringEventHandler;
            CommonSceneManager.Instance.OnAsycnOperationCompleteEvent += OnAsyncOperationCompleteEventHandler;
        }

        private void OnAsyncOperationCompleteEventHandler()
        {
            if (_sceneAnimator != null)
                InternalTools.DoActionDelayed(()=> _sceneAnimator.SetBool("IsSceneLoaded", true), 2.5f);
        }

        private void OnAnimationStringEventHandler(string value)
        {
            switch (value)
            {
                case "InAction":
                    CommonSceneManager.Instance.LoadSceneByName(CommonSceneManager.Instance.AimSceneNameAfterLoading);
                    break;
                case "Out":
                    CommonSceneManager.Instance.OpenLoadedScene();
                    _sceneAnimator.SetBool("IsSceneLoaded", false);
                    break;
            }
        }
    }
}