using Chebureck.Managers;
using UnityEngine;

namespace Chebureck.Objects.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private float _speed = 5f;
        [SerializeField]
        private float _turnSpeed = 360;

        private Vector3 _input;

        private void GetInput()
        {
            _input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }

        private void PlayerMove()
        {
            _rigidbody.MovePosition(transform.position + Vector3.ClampMagnitude(transform.forward * _input.magnitude, 1f) * _speed * Time.deltaTime);
        }

        private void PlayerRotation()
        {
            if (_input != Vector3.zero)
            {
                Vector3 relative = (transform.position + _input) - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relative, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _turnSpeed * Time.deltaTime);
            }
        }

        private void Update()
        {
            if (!GameplayManager.Instance.IsGameStarted) return;

            GetInput();
            PlayerRotation();
        }

        private void FixedUpdate()
        {
            if (!GameplayManager.Instance.IsGameStarted) return;

            PlayerMove();
        }


    }
}