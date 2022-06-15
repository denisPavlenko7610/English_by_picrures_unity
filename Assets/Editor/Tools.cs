using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace EnglishByPictures
{
    public class Tools : EditorWindow
    {
        [MenuItem("Tools/Create words")]
        public static async void GetText()
        {
            var pathToSave = Application.dataPath + "/Resources/Text/Text.txt";
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

                lines.Add(Utils.ToUpperFirstChar(text));
                await File.WriteAllLinesAsync(pathToSave, lines);
            }

            Debug.Log("Path: " + pathToSave);
            Debug.Log("Text added");
        }

        [MenuItem("Tools/Rename Images")]
        public static void RenameImages()
        {
            DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/Sprites/Resources/");
            FileInfo[] info = dir.GetFiles("*.*");
            var pattern = @"(^\d*-)";
            foreach (FileInfo fileName in info)
            {
                if (fileName.Name.Contains("meta"))
                    continue;
                
                if (!Regex.IsMatch(fileName.Name, pattern))
                    continue;
                
                var text = fileName.Name;
                text = Regex.Replace(text, pattern, "");
                fileName.Rename(text);
                Debug.Log("new name: "+ text);
            }
            
            Debug.Log("Renamed successfully");
        }

        [MenuItem("Tools/Destroy saved file")]
        public static void DestroyFile()
        {
            var pathToLearnedWords = Application.dataPath + "/Text/LearnedWords.dat";
            if (File.Exists(pathToLearnedWords))
            {
                File.Delete(pathToLearnedWords);
                Debug.Log("File deleted");
            }
        }
    }
}