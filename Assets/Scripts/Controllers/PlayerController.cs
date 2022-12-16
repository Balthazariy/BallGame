using Chebureck.Managers;
using Chebureck.Objects.Player;
using System;
using UnityEngine;

namespace Chebureck.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        public event Action<Vector2> GetStartMousePositionEvent;
        public event Action<Vector2> GetEndMousePositionEvent;

        private Ball _ball;
        private Camera _camera;

        public bool BallLaunched { get; private set; }

        public Vector2 ForceDirection { get; private set; }

        private Vector2 _startPosition,
                        _endPosition;

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            GameplayManager.Instance.GameplayStartEvent += GameplayStartEventHandler;
        }

        private void GameplayStartEventHandler()
        {
            _ball = GameObject.Find("Player").GetComponent<Ball>();
            _camera = GameObject.Find("GameplayCamera").GetComponent<Camera>();
        }

        private Vector2 GetInputMouseWorldPointPosition() => _camera.ScreenToWorldPoint(Input.mousePosition);

        private void Update()
        {
            if (BallLaunched) return;

            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = GetInputMouseWorldPointPosition();
                GetStartMousePositionEvent?.Invoke(_startPosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                _endPosition = GetInputMouseWorldPointPosition();
                ForceDirection = _startPosition - _endPosition;
                BallLaunched = true;
                GetEndMousePositionEvent?.Invoke(_endPosition);
            }
        }

        private void ResetBall()
        {
            BallLaunched = false;
        }
    }
}