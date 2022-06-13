using System.IO;
using UnityEngine;

namespace EnglishByPictures
{
    public class DestroyLearnedWords : MonoBehaviour
    {
#if UNITY_EDITOR
        [ContextMenu("DestroySavedFile")]
        public void DestroySavedFile()
        {
            var pathToLearnedWords = Application.dataPath + "/Text/LearnedWords.dat";
            if (File.Exists(pathToLearnedWords))
            {
                File.Delete(pathToLearnedWords);
                print("File deleted");
            }
        }
#endif
    }
}