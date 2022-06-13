using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace EnglishByPictures
{
#if UNITY_EDITOR

    public class ReadFileNames : MonoBehaviour
    {
        [ContextMenu("ReadImageNames")]
        public async Task ReadImageNames()
        {
            var path = Application.dataPath;
            DirectoryInfo dir = new DirectoryInfo(path + "/Sprites/Resources/");
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
                await File.WriteAllLinesAsync(path + "/Text/Text.txt", lines);
            }
            
            print("Text added");
        }
    }
#endif
}