using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace EnglishByPictures
{
    public static class Utils
    {
        public static string ToUpperFirstChar(string word) => char.ToUpper(word[0]) + word[1..];
        public static int GetRandomNumber(List<string> words) => Random.Range(0, words.Count);
        public static int ConvertEnumToInt<T>(string currentCulture) => (int)Enum.Parse(typeof(T), currentCulture);
        public static string ConvertEnumToString<T>(int value) => Enum.GetName(typeof(T), value);
        public static Array GetEnumValues<T>() => Enum.GetValues(typeof(T));
        public static List<string> Shuffle(this List<string> list) => list.OrderBy(a => Guid.NewGuid()).ToList();
    }
}