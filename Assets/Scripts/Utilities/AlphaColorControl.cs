using Chebureck.Settings;
using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chebureck.Utilities
{
    public class AlphaColorControl
    {
        private Dictionary<Image, float> _images;
        private Dictionary<TMP_Text, float> _texts;
        private Dictionary<SpriteRenderer, float> _spriteRenderers;
        private Sequence _colorSequence;

        private int _numberOfOperation;
        public bool IsActive { get; private set; }

        public AlphaColorControl(Transform selfObject, bool disable = false, ColorTypeEnumerators collection = ColorTypeEnumerators.All, int numberOfOperation = 10)
        {
            switch (collection)
            {
                case ColorTypeEnumerators.All:
                    CollectColors(selfObject);
                    CollectText(selfObject);
                    CollectSpriteRenderer(selfObject);
                    break;
                case ColorTypeEnumerators.TextOnly:
                    CollectColors(selfObject);
                    break;
                case ColorTypeEnumerators.SpriteRenderersOnly:
                    CollectSpriteRenderer(selfObject);
                    break;
                case ColorTypeEnumerators.ImageOnly:
                    CollectText(selfObject);
                    break;
            }
            _numberOfOperation = numberOfOperation;
            AlphaStateChangesAbruptly(!disable);

        }
        private void CollectColors(Transform selfObject)
        {
            _images = new Dictionary<Image, float>();
            Image[] images = selfObject.GetComponentsInChildren<Image>();
            foreach (var item in images)
            {
                _images.Add(item, item.color.a);
            }
        }
        private void CollectText(Transform selfObject)
        {
            _texts = new Dictionary<TMP_Text, float>();
            TMP_Text[] images = selfObject.GetComponentsInChildren<TMP_Text>();
            foreach (var item in images)
            {
                _texts.Add(item, item.color.a);
            }
        }
        private void CollectSpriteRenderer(Transform selfObject)
        {
            _spriteRenderers = new Dictionary<SpriteRenderer, float>();
            SpriteRenderer[] spriteRenderers = selfObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (var item in spriteRenderers)
            {
                _spriteRenderers.Add(item, item.color.a);
            }
        }
        private Color AlphaChanges(Color colorChanges, float defaultAlpha)
        {
            Color color = colorChanges;
            color.a += defaultAlpha / _numberOfOperation;

            return color;
        }
        private void AlphaStateChanges(bool isShow, Action callback)
        {
            if (isShow == IsActive)
            {
                callback?.Invoke();
                return;
            }

            if (_colorSequence != null)
                KillSequence(_colorSequence);

            int factor = isShow ? 1 : -1;
            int numberOperation = 0;
            _colorSequence = DOTween.Sequence();

            _colorSequence.AppendCallback(() =>
            {
                if (_images != null)
                {
                    foreach (var item in _images)
                    {
                        if (item.Key)
                            item.Key.color = AlphaChanges(item.Key.color, item.Value * factor);
                    }
                }
                if (_texts != null)
                {
                    foreach (var item in _texts)
                    {
                        if (item.Key)
                            item.Key.color = AlphaChanges(item.Key.color, item.Value * factor);
                    }
                }
                if (_spriteRenderers != null)
                {
                    foreach (var item in _spriteRenderers)
                    {
                        if (item.Key)
                            item.Key.color = AlphaChanges(item.Key.color, item.Value * factor);
                    }
                }
                numberOperation++;
                if (numberOperation >= _numberOfOperation)
                {
                    KillSequence(_colorSequence);
                    IsActive = isShow;
                    callback?.Invoke();
                }
            });

            _colorSequence.AppendInterval(0.1f);
            _colorSequence.SetLoops(-1);
            _colorSequence.Play();
        }
        private void AlphaStateChangesAbruptly(bool isShow)
        {
            KillSequence(_colorSequence);

            int factor = isShow ? 1 : 0;

            if (_images != null)
            {
                foreach (var item in _images)
                {
                    if (item.Key)
                    {
                        Color color = item.Key.color;
                        color.a = item.Value * factor;
                        item.Key.color = color;
                    }
                }
            }
            if (_texts != null)
            {
                foreach (var item in _texts)
                {
                    if (item.Key)
                    {
                        Color color = item.Key.color;
                        color.a = item.Value * factor;
                        item.Key.color = color;
                    }
                }
            }
            if (_spriteRenderers != null)
            {
                foreach (var item in _spriteRenderers)
                {
                    if (item.Key)
                    {
                        Color color = item.Key.color;
                        color.a = item.Value * factor;
                        item.Key.color = color;
                    }
                }
            }
            IsActive = isShow;
        }
        private void KillSequence(Sequence sequence)
        {
            sequence?.Kill();
            sequence = null;

        }
        public void HideAbruptly()
        {
            AlphaStateChangesAbruptly(false);
        }
        public void ShowAbruptly()
        {
            AlphaStateChangesAbruptly(true);
        }
        public void Hide(Action callback = null)
        {
            AlphaStateChanges(false, callback);
        }
        public void Show(Action callback = null)
        {
            AlphaStateChanges(true, callback);
        }
    }
}