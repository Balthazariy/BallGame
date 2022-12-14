using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Chebureck.Utilities
{
    public class DebugMenu : MonoBehaviour
    {
        public static DebugMenu Instance { get; private set; }

        private bool _isActive;

        public Transform gameplayGroup;

        public GameObject togglePrefab;

        public GameObject buttonPrefab;

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
        }

        private void SpawnToggleFunction(Transform group, UnityAction<bool> callback, string description)
        {
            GameObject toggle = Instantiate(togglePrefab, group, false);
            toggle.GetComponent<Toggle>().onValueChanged.AddListener(callback);
            toggle.transform.Find("Label").GetComponent<Text>().text = description;
        }

        private void SpawnButtonFunction(Transform group, UnityAction callback, string description)
        {
            GameObject button = Instantiate(buttonPrefab, group, false);
            button.GetComponent<Button>().onClick.AddListener(callback);
            button.transform.Find("Text").GetComponent<Text>().text = description;
        }
    }
}