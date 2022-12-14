using Chebureck.Settings;
using DG.Tweening;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chebureck.Utilities
{
    public static class InternalTools
    {
        private static string LINE_BREAK = "%n%";

        public static Sequence DoActionDelayed(TweenCallback action, float delay = 0f)
        {
            if (action == null)
                return null;

            Sequence sequence = DOTween.Sequence();
            sequence.PrependInterval(delay);
            sequence.AppendCallback(action);

            return sequence;
        }

        public static void HapticVibration(int level = 0)
        {
#if !UNITY_EDITOR && (UNITY_ANDROID || UNITY_IOS)
            Handheld.Vibrate();
#endif
        }

        public static float ConvertToRadians(float angle)
        {
            return (Mathf.PI / 180) * angle;
        }

        public static float GetFloatSquaredNumber(float num, int n)
        {
            float num_n = 1;
            for (int i = 0; i < n; i++)
            {
                num_n *= num;
            }
            return num_n;
        }

        public static Vector2 GPSToUnityCoordinatesWithArangeInRectangleShape(Vector2 gpsP1, Vector2 gpsP2, Vector2 uiP1, Vector2 uiP4, Vector2 mainGPS)
        {
            Vector2 calculatedPosition = Vector2.zero;
            calculatedPosition.x = uiP1.x + ((uiP4.x - uiP1.x) * ((mainGPS.x - gpsP1.x) / (gpsP2.x - gpsP1.x)));
            calculatedPosition.y = uiP1.y + ((uiP4.y - uiP1.y) * ((mainGPS.y - gpsP1.y) / (gpsP2.y - gpsP1.y)));

            calculatedPosition.x = Mathf.Clamp(calculatedPosition.x, Mathf.Min(uiP1.x, uiP4.x), Mathf.Max(uiP1.x, uiP4.x));
            calculatedPosition.y = Mathf.Clamp(calculatedPosition.y, Mathf.Min(uiP1.y, uiP4.y), Mathf.Max(uiP1.y, uiP4.y));

            return calculatedPosition;
        }

        public static int GetIntSquaredNumber(int num, int n)
        {
            int num_n = 1;
            for (int i = 0; i < n; i++)
            {
                num_n *= num;
            }
            return num_n;
        }

        /// <summary>
        /// Multiplier must be 1.07 - 1.15
        /// </summary>
        public static float GetIncrementalFloatValue(float basicValue, float multiplier, int ownedCount)
        {
            return basicValue * GetFloatSquaredNumber(multiplier, ownedCount);
        }

        public static T EnumFromString<T>(string value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static void MoveToEndOfList<T>(IList<T> list, int index)
        {
            T item = list[index];
            list.RemoveAt(index);
            list.Add(item);
        }

        public static string ReplaceLineBreaks(string data)
        {
            if (data == null)
                return "";

            return data.Replace(LINE_BREAK, "\n");
        }

        public static void ShuffleList<T>(this IList<T> list)
        {
            System.Random rnd = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static List<T> GetRandomElements<T>(this IList<T> list, int count)
        {
            List<T> shuffledList = new List<T>(count);
            shuffledList.AddRange(list);

            if (list.Count <= count)
                return shuffledList;

            ShuffleList(shuffledList);
            return shuffledList.GetRange(0, count);
        }

        public static float LerpByStep(float current, float end, float step, bool applyDeltaTime = true)
        {
            if (applyDeltaTime)
            {
                step *= Time.deltaTime;
            }

            if (current > end)
            {
                step *= -1f;
            }

            return Mathf.Clamp(current + step, Mathf.Min(current, end), Mathf.Max(current, end));
        }

        public static T GetInstance<T>(string name, params object[] args)
        {
            return (T)Activator.CreateInstance(Type.GetType(name), args);
        }

        public static float GetAngleBetweenTwoVectors2(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
        {
            Vector2 playerToArrowDirection = a1 - a2;
            Vector2 playerToPOIDirection = b1 - b2;

            float angleBetweenPlayerAndArrow = Mathf.Atan2(playerToArrowDirection.y, playerToArrowDirection.x) * Mathf.Rad2Deg - 90;
            float angleBetweenPlayerAndPOI = Mathf.Atan2(playerToPOIDirection.y, playerToPOIDirection.x) * Mathf.Rad2Deg - 90;

            float result = angleBetweenPlayerAndArrow - angleBetweenPlayerAndPOI;

            if (result > 180f)
                result = (360 - result);
            if (result < -180)
                result = (360 + result);

            return result;
        }

        public static bool IsPointerOverUIObject()
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
            return results.Count > 0;
        }

        public static Rect GetScreenCoordinates(RectTransform uiElement, GameObject canvas)
        {
            RectTransform canvasTransf = canvas.GetComponent<RectTransform>();

            Vector2 canvasSize = new Vector2(canvasTransf.rect.width, canvasTransf.rect.height);
            float koefX = Screen.width / canvasSize.x;
            float koefY = Screen.height / canvasSize.y;
            Vector2 position = Vector2.Scale(uiElement.anchorMin, canvasSize);
            float directionX = uiElement.pivot.x * -1;
            float directionY = uiElement.pivot.y * -1;

            var result = new Rect();
            result.width = uiElement.sizeDelta.x * koefX;
            result.height = uiElement.sizeDelta.y * koefX;
            result.x = position.x * koefX + uiElement.anchoredPosition.x * koefX + result.width * directionX;
            result.y = position.y * koefY + uiElement.anchoredPosition.y * koefX + result.height * directionY;
            return result;
        }

        public static float DeviceDiagonalSizeInInches()
        {
            float screenWidth = Screen.width / Screen.dpi;
            float screenHeight = Screen.height / Screen.dpi;
            float diagonalInches = Mathf.Sqrt(Mathf.Pow(screenWidth, 2) + Mathf.Pow(screenHeight, 2));

            return diagonalInches;
        }

        public static bool IsTabletScreen()
        {
#if FORCE_TABLET_UI
            return true;
#elif FORCE_PHONE_UI
            return false;
#else
            return DeviceDiagonalSizeInInches() > 6.5f;
#endif
        }

        /// <summary>
        /// Based on Top left and Down right UI and GPS point place object in rectangle shape via GPS coordinates
        /// <br>uiP1 - top left UI point </br>
        /// <br>uiP4 - down right UI point </br>
        /// <br>gpsP1 - top left GPS point </br>
        /// <br>gpsP4 - down right GPS point </br>
        /// <br>playerNativeGPSPosition - position of player based on GPS system </br>
        /// </summary>
        /// <param name="uiP1">Top left UI point</param>
        /// <param name="uiP4">Down right UI point</param>
        /// <param name="gpsP1">Top left GPS point</param>
        /// <param name="gpsP4">Down right GPS point</param>
        /// <param name="playerNativeGPSPosition"></param>
        /// <returns></returns>
        public static Vector2 GetPositionInUIRectangleShapeBasedOnGPSPosition(Vector2 uiP1, Vector2 uiP4, Vector2 gpsP1, Vector2 gpsP4, Vector2 playerNativeGPSPosition)
        {
            Vector2 result;

            result.x = uiP1.x + ((uiP4.x - uiP1.x) * ((playerNativeGPSPosition.x - gpsP1.x) / (gpsP4.x - gpsP1.x)));
            result.y = uiP1.y + ((uiP4.y - uiP1.y) * ((playerNativeGPSPosition.y - gpsP1.y) / (gpsP4.y - gpsP1.y)));

            result.x *= -1f;
            result.y *= -1f;

            result.x = Mathf.Clamp(result.x, Mathf.Min(uiP1.x, uiP4.x), Mathf.Max(uiP1.x, uiP4.x));
            result.y = Mathf.Clamp(result.y, Mathf.Min(uiP1.y, uiP4.y), Mathf.Max(uiP1.y, uiP4.y));

            return result;
        }

        public static string GetUserCode(int coseSize)
        {
            string code = string.Empty;

            const string numbers = "0123456789";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 0; i < coseSize; i++)
            {
                if (UnityEngine.Random.Range(0, 100) < 50)
                {
                    code += numbers[UnityEngine.Random.Range(0, numbers.Length)];
                }
                else
                {
                    code += chars[UnityEngine.Random.Range(0, chars.Length)];
                }
            }

            return code;
        }

        public static string GetUserId()
        {
            return SystemInfo.deviceUniqueIdentifier;
        }

        public static string SerializeData(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public static T DeserializeData<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static string FormatNum(float num)
        {
            if (num == 0) return "0";

            int i = 0;

            while (i + 1 < NUMBER_ABBREVIATIONS.Length && num >= 1000f)
            {
                num /= 1000f;
                i++;
            }

            return num.ToString("#.##") + NUMBER_ABBREVIATIONS[i];
        }

        private static string[] NUMBER_ABBREVIATIONS = { "", "K", "M", "B", "T", "Qua", "Quin", "Sex", "Sept", "Oct", "Non", "Deci" };
    }
}