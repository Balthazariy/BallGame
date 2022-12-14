using System;
using UnityEngine;

namespace Chebureck.Utilities
{
    public class RendererGroup
    {
        public event Action CompleteEvent;

        private GameObject _rootObject;

        private SpriteRenderer[] _renderers;
        private TMPro.TextMeshPro[] _rendererTexts;

        private bool _fading;

        private float _startTime;

        private float _duration;

        private float _time;

        private float _startValue;

        private float _endValue;

        private float _value;

        public RendererGroup(GameObject rootObject)
        {
            _rootObject = rootObject;

            _renderers = _rootObject.GetComponentsInChildren<SpriteRenderer>(true);
            _rendererTexts = _rootObject.GetComponentsInChildren<TMPro.TextMeshPro>(true);
        }

        public void Dispose()
        {
            _fading = false;
            _renderers = null;
        }

        public void Update()
        {
            if (_fading)
            {
                _time = (Time.time - _startTime) / _duration;
                _value = Mathf.Lerp(_value, _endValue, _time);

                RefreshRenderers();

                if (_time >= 1f)
                {
                    _fading = false;
                    CompleteEvent?.Invoke();
                }
            }
        }

        public void Fade(float from, float to, float duration)
        {
            _startValue = from;
            _endValue = to;
            _duration = duration;
            _value = _startValue;

            _fading = true;
        }

        public void FadeForce(float to)
        {
            _value = to;
            RefreshRenderers();

            CompleteEvent?.Invoke();
        }

        private void RefreshRenderers()
        {
            Color color;
            for (int i = 0; i < _renderers.Length; i++)
            {
                if (_renderers[i] != null)
                {
                    color = _renderers[i].color;
                    color.a = _value;
                    _renderers[i].color = color;
                }
            }

            for (int i = 0; i < _rendererTexts.Length; i++)
            {
                if (_rendererTexts[i] != null)
                {
                    color = _rendererTexts[i].color;
                    color.a = _value;
                    _rendererTexts[i].color = color;
                }
            }
        }
    }
}