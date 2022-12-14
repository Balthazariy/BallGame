using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chebureck.Objects.Player
{
    public class CameraFollowPlayer : MonoBehaviour
    {
        private Vector3 _offset;
        [SerializeField]
        private Transform _target;
        [SerializeField]
        private float _smoothTime;

        private void Awake()
        {
            _offset = transform.position - _target.position; 
        }

        private void FixedUpdate()
        {
            Vector3 targetPosition = _target.position + _offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, _smoothTime);
            transform.position = smoothedPosition;
        }
    }
}