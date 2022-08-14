using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using EnglishByPictures.Ads;
using GoogleMobileAds.Api;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnglishByPictures
{
    [Serializable]
    public class ChangeWords : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mainText;
        [SerializeField] TextMeshProUGUI wordsLeftText;
        [SerializeField] TextMeshProUGUI finishText;
        [SerializeField] TextMeshProUGUI translatedText;
        [SerializeField] Image mainImage;
        [SerializeField] Button learnButton;
        [SerializeField] Button knowButton;
        [SerializeField] DropdownLanguages dropdownLanguages;

        List<string> words = new();
        List<string> learnedWords = new();
        int currentNumber = -1;
        int countBeforeHideFinishText = 2;
        LoadAndSave loadAndSave;
        Translate translate;
        public string CurrentWord { get; private set; }
        public string TranslatedWord { get; private set; }

        private void OnValidate()
        {
            if (!dropdownLanguages)
                dropdownLanguages = FindObjectOfType<DropdownLanguages>();
        }

        void Start()
        {
            Init();
            ShowWord();
        }

        void Init()
        {
            translate = new Translate();
            loadAndSave = new LoadAndSave();
            learnedWords = loadAndSave.LoadLearnedWords(Application.persistentDataPath);
            finishText.enabled = false;
            words = loadAndSave.LoadWords();
            RemoveLearnedWords();
            Subscribe();
            ShowWordsLeft();
        }

        void ChangeCulture() => GetTranslatedText(CurrentWord);

        void ShowWordsLeft() => wordsLeftText.text = $"{words.Count} words left";

        async UniTask ShowWord()
        {
            var randomNumber = Utils.GetRandomNumber(words);
            while (currentNumber == randomNumber && words.Count > 1)
                randomNumber = Utils.GetRandomNumber(words);

            currentNumber = randomNumber;
            CurrentWord = words[randomNumber];
            mainText.text = CurrentWord;
            mainImage.sprite = Resources.Load<Sprite>(words[randomNumber]);
            await GetTranslatedText(CurrentWord);
            CheckToHideFinishText();
        }

        async Task GetTranslatedText(string word)
        {
            var text = await translate.Process(dropdownLanguages.CurrentCulture, word);
            TranslatedWord = text;
            translatedText.text = "...";
            translatedText.text = Utils.ToUpperFirstChar(text);
        }

        private void CheckToHideFinishText()
        {
            if (finishText.isActiveAndEnabled)
                countBeforeHideFinishText--;

            if (countBeforeHideFinishText == 0)
                finishText.enabled = false;
        }


        void Learn()
        {
            CheckToShowIntestinalAd();
            ShowWord();
        }

        private static void CheckToShowIntestinalAd()
        {
            Interstitial.CountToShowAd++;
            if (Interstitial.CountToShowAd == 15)
            {
                Interstitial.CountToShowAd = 0;
                Interstitial.RequestInterstitial();
            }
        }

        void Know()
        {
            var removed = words[currentNumber];
            words.Remove(removed);
            learnedWords.Add(removed);
            if (words.Count == 0)
                ResetWords();

            ShowWordsLeft();
            ShowWord();
        }


        void RemoveLearnedWords()
        {
            if (learnedWords.Count != words.Count)
                words = words.Except(learnedWords).ToList();
        }

        void ResetWords()
        {
            learnedWords.Clear();
            words = loadAndSave.LoadWords();
            finishText.enabled = true;
        }

        void Subscribe()
        {
            dropdownLanguages.onCultureChanged += ChangeCulture;
            learnButton.onClick.AddListener(Learn);
            knowButton.onClick.AddListener(Know);
        }

        void Unsubscribe()
        {
            dropdownLanguages.onCultureChanged -= ChangeCulture;
            learnButton.onClick.RemoveListener(Learn);
            knowButton.onClick.RemoveListener(Know);
        }

        void OnDisable()
        {
            Unsubscribe();
            loadAndSave.SaveLearnedWords(Application.persistentDataPath, learnedWords);
        }

        void OnDestroy()
        {
            Unsubscribe();
            loadAndSave.SaveLearnedWords(Application.persistentDataPath, learnedWords);
        }
    }
}