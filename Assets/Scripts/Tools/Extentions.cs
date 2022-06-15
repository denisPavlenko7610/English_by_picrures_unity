using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnglishByPictures
{
    public static class Extentions
    {
        public static List<string> Shuffle(this IEnumerable<string> list) 
            => list.OrderBy(a => Guid.NewGuid()).ToList();

        public static void Rename(this FileInfo fileInfo, string newName) =>
            fileInfo.MoveTo(fileInfo.Directory.FullName + "\\" + newName);
    }
}