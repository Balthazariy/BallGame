using Chebureck.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chebureck.Utilities
{
    public class Logger : MonoBehaviour
    {
        public static void Log(string message, LogTypeEnumerators type)
        {
            switch (type)
            {
                case LogTypeEnumerators.Info:
                    Debug.Log("<color=#52b788>[INFO]</color> " + message);
                    break;
                case LogTypeEnumerators.Warning:
                    Debug.Log("<color=#fdffb6>[WARNING]</color> " + message);
                    break;
                case LogTypeEnumerators.Error:
                    Debug.Log("<color=#f72585>[ERROR]</color> " + message);
                    break;
                case LogTypeEnumerators.Debug:
                    Debug.Log("<color=#8ecae6>[DEBUG]</color> " + message);
                    break;
            }
        }
    }
}