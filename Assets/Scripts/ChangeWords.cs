using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace EnglishByPictures
{
    [Serializable]
    public class ChangeWords : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mainText;
        [SerializeField] TextMeshProUGUI wordsLeftText;
        [SerializeField] TextMeshProUGUI finishText;
        [SerializeField] Image mainImage;
        [SerializeField] Button learnButton;
        [SerializeField] Button knowButton;
        [SerializeField] Button switchThemeButton;
        [SerializeField] Sprite switchThemeBlackImage;
        [SerializeField] Sprite switchThemeWhiteImage;
        [SerializeField] Image backgroundImage;
        [SerializeField] Color blackColor;
        [SerializeField] Color whiteColor;

        List<string> words = new();
        List<string> learnedWords = new();
        string path;
        int currentNumber = -1;
        int countBeforeHideFinishText = 2;
        Theme theme = Theme.Black;
        LoadAndSave loadAndSave;

        void Start()
        {
            Init();
            ShowWord();
        }

        void Init()
        {
            loadAndSave = new LoadAndSave();
            path = Application.dataPath + "/Text/";
            learnedWords = loadAndSave.LoadLearnedWords(path);
            finishText.enabled = false;
            words = loadAndSave.LoadWords(path);
            RemoveLearnedWords();
            Subscribe();
            ShowWordsLeft();
            theme = loadAndSave.LoadThemeSettings();
            SetTheme();
        }

        void ShowWordsLeft()
        {
            wordsLeftText.text = $"{words.Count} words left";
        }
        
        void ShowWord()
        {
            var randomNumber = GetRandomNumber();
            while (currentNumber == randomNumber && words.Count > 1)
                randomNumber = GetRandomNumber();

            currentNumber = randomNumber;
            var str = words[randomNumber];
            mainText.text = char.ToUpper(str[0]) + str[1..];
            mainImage.sprite = Resources.Load<Sprite>(words[randomNumber]);

            CheckToHideFinishText();
        }

        private void CheckToHideFinishText()
        {
            if (finishText.isActiveAndEnabled)
                countBeforeHideFinishText--;
            
            if (countBeforeHideFinishText == 0)
                finishText.enabled = false;
        }

        int GetRandomNumber() => Random.Range(0, words.Count);
        
        void Learn()
        {
            ShowWord();
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

        void SwitchTheme()
        {
            if (theme == Theme.Black)
                theme = Theme.White;
            else
                theme = Theme.Black;
            
            SetTheme();
            loadAndSave.SaveThemeSettings(theme);
        }

        private void SetTheme()
        {
            Color buttonTextColor;
            Color textColor;
            if (theme == Theme.Black)
            {
                buttonTextColor = whiteColor;
                textColor = blackColor;
                switchThemeButton.image.sprite = switchThemeBlackImage;
            }
            else
            {
                switchThemeButton.image.sprite = switchThemeWhiteImage;
                textColor = whiteColor;
                buttonTextColor = blackColor;
            }

            backgroundImage.color = buttonTextColor;
            mainText.color = textColor;
            wordsLeftText.color = textColor;
        }

        void RemoveLearnedWords()
        {
            if (learnedWords.Count != words.Count)
                words = words.Except(learnedWords).ToList();
        }

        void ResetWords()
        {
            learnedWords.Clear();
            words = loadAndSave.LoadWords(path);
            finishText.enabled = true;
        }
        
        void Subscribe()
        {
            learnButton.onClick.AddListener(Learn);
            knowButton.onClick.AddListener(Know);
            switchThemeButton.onClick.AddListener(SwitchTheme);
        }

        void Unsubscribe()
        {
            learnButton.onClick.RemoveListener(Learn);
            knowButton.onClick.RemoveListener(Know);
            switchThemeButton.onClick.RemoveListener(SwitchTheme);
        }

        void OnDisable()
        {
            Unsubscribe();
            loadAndSave.SaveLearnedWords(path, learnedWords);
        }

        void OnDestroy()
        {
            Unsubscribe();
            loadAndSave.SaveLearnedWords(path, learnedWords);
        }
    }
}