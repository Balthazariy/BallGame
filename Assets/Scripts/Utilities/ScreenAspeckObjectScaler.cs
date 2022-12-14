using UnityEngine;

namespace Chebureck.Utilities
{
    public class ScreenAspeckObjectScaler : MonoBehaviour
    {
        public Vector2Int initialScreenSize = new Vector2Int(1920, 1080);

        public Vector2Int screenSize = Vector2Int.zero;

        public Vector2Int refreshedScreenSize = Vector2Int.zero;

        public Vector2 initialScale = Vector2.one;

        private void Awake()
        {
            refreshedScreenSize = initialScreenSize;
        }

        private void Update()
        {
            screenSize.x = Screen.width;
            screenSize.y = Screen.height;

            if (refreshedScreenSize.x == screenSize.x &&
                refreshedScreenSize.y == screenSize.y)
                return;

            refreshedScreenSize = screenSize;

            float aspectInitialW = initialScreenSize.x / (float)initialScreenSize.y;
            float aspectCurrentW = Mathf.Clamp(screenSize.x / (float)screenSize.y, 0.5f, 1.77f);
            float aspect = aspectCurrentW / aspectInitialW;

            Vector3 scale = transform.localScale;
            scale.x = initialScale.x * aspect;
            scale.y = initialScale.x * aspect;
            transform.localScale = scale;
        }
    }
}