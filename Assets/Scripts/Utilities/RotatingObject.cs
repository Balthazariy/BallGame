using UnityEngine;

namespace Chebureck.Utilities
{
    public class RotatingObject : MonoBehaviour
    {
        public Vector3 rotation = Vector3.zero;

        public bool rotate;

        private void FixedUpdate()
        {
            if (rotate)
            {
                transform.localEulerAngles += rotation * Time.fixedDeltaTime;
            }
        }
    }
}