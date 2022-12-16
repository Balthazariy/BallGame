using Chebureck.Controllers;
using System;
using UnityEngine;

namespace Chebureck.Managers
{
    public class GameplayManager : MonoBehaviour
    {
        public event Action GameplayStartEvent;
        public event Action GameplayEndEvent;

        public static GameplayManager Instance { get; private set; }

        public bool IsGameStarted { get; private set; }
        public bool IsGamePaused { get; private set; }

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            PauseController.Instance.IsGamePausedEvent += IsGamePausedEventHandler;
        }

        private void Update()
        {

        }

        private void FixedUpdate()
        {

        }

        public void StartGameplay()
        {
            IsGameStarted = true;

            GameplayStartEvent?.Invoke();
        }

        public void StopGameplay()
        {
            IsGameStarted = false;

            GameplayEndEvent?.Invoke();
        }

        private void IsGamePausedEventHandler(bool isPaused)
        {
            IsGamePaused = isPaused;
        }
    }
}