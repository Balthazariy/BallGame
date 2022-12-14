using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Chebureck.Utilities
{
    public class ShadowedText : MonoBehaviour
    {
        [Header("Text Variables")]
        [SerializeField]
        private TextMeshProUGUI _mainText;
        [SerializeField]
        private TextMeshProUGUI _shadowedText;

        [Header("Color Variables")]
        [SerializeField]
        private Color _mainTextColor;
        [SerializeField]
        private Color _shadowedTextColor;

        private void Awake()
        {
            // todo expand class logic
        }

        private void Start()
        {
            
        }
    }
}