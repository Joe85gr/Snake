using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Models;
using UnityEngine;

namespace Services
{
    public static class SaveService
    {
        private const string SaveFileName = "data.save";

        public static void Save(string player, float score)
        {
            var savedData = Load();
            savedData.Scores.Add((player, score));

            if (savedData.Scores.Count > 10)
            {
                var scores = savedData.Scores.OrderByDescending(s => s.Score).Take(10);
                savedData.Scores = scores.ToList();
            }

            var formatter = new BinaryFormatter();
            var savePath = GetSavePath();

            using var stream = new FileStream(savePath, FileMode.Create);
            formatter.Serialize(stream, savedData);
        }

        public static SaveData Load()
        {
            var savePath = GetSavePath();

            if (File.Exists(savePath) == false) return new SaveData();
        
            var formatter = new BinaryFormatter();

            using var stream = new FileStream(savePath, FileMode.Open);
            var data = (SaveData)formatter.Deserialize(stream);

            return data;
        }

        private static string GetSavePath()
        {
            Debug.Log($"{Application.persistentDataPath}/{SaveFileName}");
            return $"{Application.persistentDataPath}/{SaveFileName}" ;
        }
    }
}
