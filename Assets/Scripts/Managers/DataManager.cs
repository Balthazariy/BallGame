using Chebureck.Models;
using Chebureck.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Chebureck.Managers
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance { get; private set; }

        public event Action CachedDataLoadedEvent;

        private Dictionary<CacheTypeEnumerators, string> _cacheDataPathes;

        public CachedUserData CachedUserData { get; private set; }

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;
        }

        public void Start()
        {
            FillCacheDataPathes();

            if (!Directory.Exists(AppConstants.PATH_TO_GAMES_CACHE))
                Directory.CreateDirectory(AppConstants.PATH_TO_GAMES_CACHE);

            StartLoadCache();
        }

        public void StartLoadCache()
        {
            for (int i = 0; i < Enum.GetNames(typeof(CacheTypeEnumerators)).Length; i++)
                LoadCachedData((CacheTypeEnumerators)i);

            CachedDataLoadedEvent?.Invoke();
        }

        public void SaveAllCache()
        {
            int count = Enum.GetNames(typeof(CacheTypeEnumerators)).Length;
            for (int i = 0; i < count; i++)
                SaveCache((CacheTypeEnumerators)i);
        }

        public void SaveCache(CacheTypeEnumerators type)
        {
            switch (type)
            {
                case CacheTypeEnumerators.UserLocalData:
                    {
                        if (!File.Exists(_cacheDataPathes[type]))
                            File.Create(_cacheDataPathes[type]).Close();

                        File.WriteAllText(_cacheDataPathes[type], SerializeData(CachedUserData));
                    }
                    break;
                default: break;
            }
        }

        private void LoadCachedData(CacheTypeEnumerators type)
        {
            switch (type)
            {
                case CacheTypeEnumerators.UserLocalData:
                    {
                        if (!File.Exists(_cacheDataPathes[type]))
                        {
                            CachedUserData = new CachedUserData()
                            {
                                isFirstRun = true,
                                appLanguage = LanguageEnumerators.English,
                                volumeMusic = 1,
                                volumeSound = 1,
                            };

                            SaveCache(type);
                        }
                        else
                        {
                            CachedUserData = DeserializeData<CachedUserData>(File.ReadAllText(_cacheDataPathes[type]));
                        }
                    }
                    break;
                default: break;
            }
        }

        private void FillCacheDataPathes()
        {
            _cacheDataPathes = new Dictionary<CacheTypeEnumerators, string>();
            _cacheDataPathes.Add(CacheTypeEnumerators.UserLocalData, Application.persistentDataPath + AppConstants.LOCAL_USER_DATA_FILE_PATH);
        }

        public string SerializeData(object data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T DeserializeData<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public void ResetData(CacheTypeEnumerators type)
        {
            switch (type)
            {
                case CacheTypeEnumerators.UserLocalData:
                    {
                        CachedUserData = new CachedUserData()
                        {
                            isFirstRun = true,
                            appLanguage = LanguageEnumerators.English,
                            volumeMusic = 1,
                            volumeSound = 1,
                        };

                        SaveCache(type);
                        CachedDataLoadedEvent?.Invoke();
                    }
                    break;
                default: break;
            }
        }
    }
}