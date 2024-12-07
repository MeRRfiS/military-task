using System.IO;
using System;
using UnityEngine;

namespace Military.Scripts.Data
{
    public class SaveData
    {
        private const string MapDirectory = "/Maps/";

        public void SaveToFile(string fileName, object objectToWrite)
        {
            var directoryPath = Application.persistentDataPath + MapDirectory;
            var filePath = Path.Combine(directoryPath, fileName);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var json = JsonUtility.ToJson(objectToWrite);
            Debug.Log($"{json}");

            using (var file = File.Open(filePath, FileMode.OpenOrCreate))
            {
                file.Dispose();
                File.WriteAllText(filePath, json);
            }
        }

        public void LoadDataFromFile(string fileName, object objectToOverwrite)
        {
            var filePath = GetFilePath(fileName);
            Debug.Log($"filePath \"{filePath}\"");

            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"File \"{filePath}\" not found!");
                return;
            }
            FileStream file = File.Open(filePath, FileMode.Open);

            file.Dispose();
            var json = File.ReadAllText(filePath);
            Debug.Log($"{json}");
            JsonUtility.FromJsonOverwrite(json, objectToOverwrite);
        }

        public void DeleteFile(string fileName)
        {
            var filePath = GetFilePath(fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private string GetFilePath(string fileName)
        {
            var directoryPath = Application.persistentDataPath + MapDirectory;
            var filePath = Path.Combine(directoryPath, fileName);

            return filePath;
        }
    }
}
