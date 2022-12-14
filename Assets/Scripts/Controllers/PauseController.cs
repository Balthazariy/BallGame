using System;
using UnityEngine;

namespace Chebureck.Controllers
{
    public class PauseController : MonoBehaviour
    {
        public static PauseController Instance { get; private set; }

        public event Action<bool> IsGamePausedEvent;

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;
        }

        public void ActivePause()
        {
            Time.timeScale = 0;
            IsGamePausedEvent?.Invoke(true);
        }

        public void DiactivatePause()
        {
            Time.timeScale = 1;
            IsGamePausedEvent?.Invoke(false);
        }
    }
}