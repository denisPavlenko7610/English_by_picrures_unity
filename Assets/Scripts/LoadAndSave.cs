using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace EnglishByPictures
{
    public class LoadAndSave
    {
        string themeKey = "Theme";

        public List<string> LoadWords(string path)
        {
            List<string> words = new();
            using var sr = new StreamReader(path + "Text.txt");
            while (sr.ReadLine() is { } line)
                words.Add(line);

            return words;
        }

        public void SaveLearnedWords(string path, List<string> learnedWords)
        {
            var fs = new FileStream(path + "LearnedWords.dat", FileMode.Create);
            var bf = new BinaryFormatter();
            bf.Serialize(fs, learnedWords);
            fs.Close();
        }

        public List<string> LoadLearnedWords(string path)
        {
            List<string> learnedWords = new();
            var pathToLearnedWords = path + "LearnedWords.dat";
            if (!File.Exists(pathToLearnedWords))
                return learnedWords;

            using Stream stream = File.Open(pathToLearnedWords, FileMode.Open);
            var bformatter = new BinaryFormatter();
            learnedWords = (List<string>)bformatter.Deserialize(stream);
            return learnedWords;
        }

        public Theme LoadThemeSettings()
        {
            Theme theme = Theme.Black;
            if (!PlayerPrefs.HasKey(themeKey))
                return theme;

            var themeInSettings = PlayerPrefs.GetString(themeKey);
            if (themeInSettings == Theme.Black.ToString())
                return theme = Theme.Black;
            
            return theme = Theme.White;
        }

        public void SaveThemeSettings(Theme theme)
        {
            PlayerPrefs.SetString(themeKey, theme.ToString());
        }
    }
}