using System.Text.RegularExpressions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace EnglishByPictures
{
    [RequireComponent(typeof(AudioSource), typeof(Button))]
    public class Speaker : MonoBehaviour
    {
        [SerializeField] AudioSource audioSource;
        [SerializeField] Button speakerButtonNative;
        [SerializeField] Button speakerButtonTranslate;
        [SerializeField] DropdownLanguages languages;
        [SerializeField] ChangeWords changeWords;

        private void OnEnable()
        {
            speakerButtonNative.onClick.AddListener(PlayNative);
            speakerButtonTranslate.onClick.AddListener(PlayTranslate);
        }

        private void OnDisable()
        {
            speakerButtonNative.onClick.RemoveListener(PlayNative);
            speakerButtonTranslate.onClick.RemoveListener(PlayTranslate);
        }

        private void OnValidate()
        {
            if (!audioSource)
                audioSource = GetComponent<AudioSource>();

            if (!languages)
                languages = FindObjectOfType<DropdownLanguages>();

            if (!changeWords)
                changeWords = GetComponent<ChangeWords>();
        }

        private void PlayNative()
        {
            Process("En", changeWords.CurrentWord);
        }

        private void PlayTranslate()
        {
            Process(languages.CurrentCulture, changeWords.TranslatedWord);
        }

        private async UniTask Process(string targetLang, string sourceText)
        {
            var rgx = new Regex ("\\s+");
            var text = rgx.Replace (sourceText, "%20");
            var url = "https://translate.google.com.vn/translate_tts?/ie=UTF-8&q=" + text
                + "&tl=" + targetLang + "&client=tw-ob";
            
            Debug.Log(url);

            var www = new WWW(url);
            await www;

            if (www.isDone)
            {
                audioSource.clip = www.GetAudioClip(false, false, AudioType.MPEG);
                audioSource.Play();
            }
        }
    }
}