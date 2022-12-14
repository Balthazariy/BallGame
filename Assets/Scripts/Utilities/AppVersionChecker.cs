using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chebureck.Utilities
{
    public class AppVersionChecker : MonoBehaviour
    {
        [SerializeField]
        private TMPro.TextMeshProUGUI _versionText;

        private void Awake()
        {
            _versionText.text = "VERSION - " + Application.version.ToString();
        }
    }
}