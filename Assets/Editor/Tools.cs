using System;
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
            var dir = new DirectoryInfo(Application.dataPath + "/Sprites/Resources/");
            var info = dir.GetFiles("*.*");

            List<string> lines = new();
            foreach (var fileName in info)
            {
                if (fileName.Name.Contains("meta"))
                    continue;

                var text = "";
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
            var hasError = false;
            var dir = new DirectoryInfo(Application.dataPath + "/Sprites/Resources/");
            var info = dir.GetFiles("*.*");
            const string pattern = @"(^\d*-)";
            foreach (var fileName in info)
            {
                if (fileName.Name.Contains("meta"))
                    continue;

                if (!Regex.IsMatch(fileName.Name, pattern))
                    continue;

                var text = fileName.Name;
                text = Regex.Replace(text, pattern, "");
                try
                {
                    fileName.Rename(text);
                }
                catch (IOException e)
                {
                    Debug.LogError(e);
                    hasError = true;
                    continue;
                }

                Debug.Log("new name: " + text);
            }
 
            if (!hasError)
                Debug.Log("Renamed successfully");
            else
                Debug.Log("Renamed with error");
        }

        [MenuItem("Tools/Find duplicates")]
        public static void FindDuplicates()
        {
            var isFound = false;
            var dir = new DirectoryInfo(Application.dataPath + "/Sprites/Resources/");
            var info = dir.GetFiles("*.*");
            const string pattern = @"\d+";

            foreach (var fileName in info)
            {
                if (fileName.Name.Contains("meta"))
                    continue;

                if (!Regex.IsMatch(fileName.Name, pattern))
                    continue;
                isFound = true;
                var text = fileName.Name;
                Debug.Log(text);
            }

            if (isFound)
                Debug.Log("Has duplicates");
            else
                Debug.Log("0 duplicates");
        }

        [MenuItem("Tools/Destroy saved file")]
        public static void DestroyFile()
        {
            var pathToLearnedWords = Application.persistentDataPath + "/LearnedWords.dat";
            if (File.Exists(pathToLearnedWords))
            {
                File.Delete(pathToLearnedWords);
                Debug.Log("File deleted");
            }
            else
            {
                Debug.Log("File is empty");
            }
        }
    }
}