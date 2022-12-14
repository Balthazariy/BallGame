using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Chebureck.Utilities
{
    public class DoubleSpriteButton : Button
    {
        public List<ButtonStateSprite> states = new List<ButtonStateSprite>();

        public bool setNativeSize = true;

        public bool controlColor = true;

        public void UpdateUI()
        {
            SetButtonStatus(false);
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (!interactable)
                return;

            SetButtonStatus(true);
        }

        public void Update()
        {
            if (states != null)
            {
                foreach (ButtonStateSprite buttonState in states)
                {
                    if (buttonState.image != null && buttonState.image)
                    {
                        if (buttonState.spriteDisabled == null)
                        {
                            if (controlColor)
                            {
                                buttonState.image.color = interactable ? buttonState.normalColor : buttonState.disabledColor;
                            }
                        }
                        else
                        {
                            buttonState.image.sprite = interactable ? buttonState.spriteNormal : buttonState.spriteDisabled;
                        }
                    }

                    //if (buttonState.text != null && buttonState.text)
                    //{
                    //    if (controlColor)
                    //    {
                    //        buttonState.text.color = interactable ? buttonState.normalColor : buttonState.disabledColor;
                    //    }
                    //}
                }
            }
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (!interactable)
                return;

            SetButtonStatus(false);
        }

        private void SetButtonStatus(bool pressed)
        {
            if (states != null)
            {
                foreach (ButtonStateSprite buttonState in states)
                {
                    if (buttonState.image != null && buttonState.image)
                    {
                        buttonState.image.sprite = null;
                        buttonState.image.sprite = pressed ? buttonState.spritePressed : buttonState.spriteNormal;

                        if (setNativeSize)
                        {
                            buttonState.image.SetNativeSize();
                        }
                    }

                    if (buttonState.text != null && buttonState.text)
                    {
                        buttonState.text.color = pressed ? buttonState.pressedColor : buttonState.normalColor;
                    }
                }
            }
        }

        [Serializable]
        public class ButtonStateSprite
        {
            public Sprite spritePressed;
            public Sprite spriteNormal;
            public Sprite spriteDisabled;
            public Image image;
            public TextMeshProUGUI text;
            public Color disabledColor = Color.white;
            public Color normalColor = Color.white;
            public Color pressedColor = Color.white;
        }
    }
}