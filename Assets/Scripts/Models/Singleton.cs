using UnityEngine;

namespace Chebureck.Models
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static bool _isShuttingDown = false;
        private static object _lock = new object();
        private static T _instance;

        protected abstract bool IsDontDestroyOnLoad { get; }

        public static bool IsInitialized
        {
            get
            {
                return _instance != null;
            }
        }

        public static bool IsAccessible
        {
            get
            {
                return !_isShuttingDown;
            }
        }

        /// <summary>
        /// Use this in OnDestroy and OnApplicationClose
        /// </summary>
        public static T SafeInstance
        {
            get
            {
                if (_isShuttingDown)
                    return null;

                return Instance;
            }
        }

        /// <summary>
        /// Use this to access the instance of the singleton.
        /// </summary>
        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (_instance == null)
                        {
                            var singletonObject = new GameObject();
                            _instance = singletonObject.AddComponent<T>();

                            Debug.Log($"Adding Singleton of type ({_instance.GetType()})");

                            string className = typeof(T).ToString();
                            if (className.Contains("."))
                                singletonObject.name = className.Substring(className.LastIndexOf(".") + 1) + " (Singleton)";
                            else
                                singletonObject.name = className + " (Singleton)";

                            if (_instance.IsDontDestroyOnLoad)
                                DontDestroyOnLoad(singletonObject);
                        }
                    }

                    return _instance;
                }
            }
        }

        private void OnApplicationQuit()
        {
            _isShuttingDown = true;
        }

        protected virtual void Start()
        {
            _isShuttingDown = false;

            if (Instance.IsDontDestroyOnLoad)
                DontDestroyOnLoad(Instance.gameObject);
        }

        protected virtual void OnDestroy()
        {
            _isShuttingDown = true;
            _instance = null;
        }
    }
}