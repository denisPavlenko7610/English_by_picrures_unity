using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EnglishByPictures
{
    public class Tools : EditorWindow
    {
        [MenuItem("Tools/Get text from images")]
        public static async void GetText()
        {
            DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Sprites/Resources/");
            FileInfo[] info = dir.GetFiles("*.*");
            
            List<string> lines = new();
            foreach (FileInfo fileName in info)
            {
                if (fileName.Name.Contains("meta"))
                    continue;

                string text = "";
                var fileExtPos = fileName.Name.LastIndexOf(".");
                if (fileExtPos >= 0)
                    text = fileName.Name.Substring(0, fileExtPos);

                if (text == "")
                    continue;

                lines.Add(text);
                await File.WriteAllLinesAsync(Application.persistentDataPath + "/Text.txt", lines);
            }

            Debug.Log("Text added");
            Debug.Log("Path: " + Application.persistentDataPath + "/Text.txt");
        }

        [MenuItem("Tools/Destroy saved file")]
        public static void DestroyFile()
        {
            var pathToLearnedWords =  Application.dataPath + "/Text/LearnedWords.dat";
            if (File.Exists(pathToLearnedWords))
            {
                File.Delete(pathToLearnedWords);
                Debug.Log("File deleted");
            }
        }
    }
}