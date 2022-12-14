using Chebureck.Screens;
using UnityEngine;

namespace Chebureck.Settings
{
    public class GeneralAppSettings : MonoBehaviour
    {
        public static GeneralAppSettings Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;

            QualitySettings.vSyncCount = 0;

            UpdateTargetFrameRate(60);
        }

        public void UpdateTargetFrameRate(int value) => Application.targetFrameRate = value; 
    }
}