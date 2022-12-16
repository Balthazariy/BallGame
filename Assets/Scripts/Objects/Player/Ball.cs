using Chebureck.Managers;
using UnityEngine;

namespace Chebureck.Objects.Player
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Rigidbody2D _rigidbody;

        private const float _force = 100;

        private Vector3 _startPosition,
                        _endPosition;

        private void GetStartPosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = GetInputMouseWorldPointPosition();
            }
        }

        private void GetEndPositionAndBallPhysics()
        {
            if (Input.GetMouseButtonUp(0))
            {
                _endPosition = GetInputMouseWorldPointPosition();

                SetBallKinematic(false);

                AddForceToBall();

                //GameplayManager.Instance.SetBallPushed(true);
            }
        }

        private void Update()
        {
            if (!GameplayManager.Instance.IsGameStarted) return;
            //if (GameplayManager.Instance.IsBallPushed) return;

            GetStartPosition();
        }

        private void FixedUpdate()
        {
            if (!GameplayManager.Instance.IsGameStarted) return;
            //if (GameplayManager.Instance.IsBallPushed) return;

            GetEndPositionAndBallPhysics();
        }

        private void AddForceToBall()
        {
            _rigidbody.AddForce(CalculateForce() * _force, ForceMode2D.Force);
        }

        private void SetBallKinematic(bool isKinematic) => _rigidbody.isKinematic = isKinematic;

        private Vector2 CalculateForce() => (_startPosition - _endPosition);

        private Vector2 GetInputMouseWorldPointPosition() => _camera.ScreenToWorldPoint(Input.mousePosition);

        private void ResetballPosition()
        {
            _rigidbody.isKinematic = true;

            _startPosition.z = 0;
            transform.position = _startPosition;

            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;

            //GameplayManager.Instance.SetBallPushed(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.name == "DownBorder")
            {
                Debug.Log(123);
                ResetballPosition();
            }
        }
    }
}