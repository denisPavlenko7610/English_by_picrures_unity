using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace EnglishByPictures
{
    public static class Utils
    {
        public static string ToUpperFirstChar(string word) => char.ToUpper(word[0]) + word[1..];
        public static int GetRandomNumber(List<string> words) => Random.Range(0, words.Count);
        public static int ConvertEnumToInt(string currentCulture) => (int)Enum.Parse(typeof(Languages), currentCulture);
        public static string ConvertEnumToString(int value) => Enum.GetName(typeof(Languages), value);

    }
}