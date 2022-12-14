using System.IO;
using UnityEngine;

namespace Chebureck.Managers
{
    public class LoadObjectsManager : MonoBehaviour
    {
        public static LoadObjectsManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null) Destroy(this.gameObject);

            if (Instance == null)
                Instance = this;
        }

        public T GetObjectByPath<T>(string path) where T : UnityEngine.Object
        {
            return LoadFromResources<T>(path);
        }

        public T LoadFromResources<T>(string path) where T : UnityEngine.Object
        {
            return Resources.Load<T>(ParsePath(path));
        }

        public T[] GetObjectsByPath<T>(string path) where T : UnityEngine.Object
        {
            return Resources.LoadAll<T>(ParsePath(path));
        }

        public void DeleteDirectoryRecursive(string directory)
        {
            if (!Directory.Exists(directory))
                return;

            string[] files = Directory.GetFiles(directory);
            string[] dirs = Directory.GetDirectories(directory);

            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }

            foreach (string dir in dirs)
            {
                DeleteDirectoryRecursive(dir);
            }

            Directory.Delete(directory, false);
        }

        private string ParsePath(string path)
        {
            string[] parsed = path.Split('/');
            path = string.Empty;

            for (int i = 0; i < parsed.Length; i++)
            {
                path += parsed[i][0].ToString().ToUpper() + parsed[i].Substring(1, parsed[i].Length - 1);

                if (i < parsed.Length - 1)
                {
                    path += '/';
                }
            }
            return path;
        }
    }
}