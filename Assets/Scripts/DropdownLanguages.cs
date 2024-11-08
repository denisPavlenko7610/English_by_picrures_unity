﻿using System;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

namespace EnglishByPictures
{
    public class DropdownLanguages : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown dropdown;
        string currentCultureKey = "CurrentCulture";
        public string CurrentCulture { get; set; } = Languages.Ru.ToString();
        public event Action onCultureChanged;

        private void OnValidate()
        {
            if (!dropdown)
                dropdown = GetComponent<TMP_Dropdown>();
        }

        [ContextMenu("Set Options")]
        public void SetOptions()
        {
            dropdown.ClearOptions();
            dropdown.AddOptions(Utils
                .GetEnumValues<Languages>()
                .Cast<Languages>()
                .Select(LanguagesConverter.ConvertToText)
                .ToList());
        }

        private void OnEnable() => dropdown.onValueChanged.AddListener(OnSelected);

        private void OnDisable()
        {
            dropdown.onValueChanged.RemoveListener(OnSelected);
            PlayerPrefs.SetString(currentCultureKey, CurrentCulture);
        }

        private void Awake()
        {
            if (PlayerPrefs.HasKey(currentCultureKey))
            {
                CurrentCulture = PlayerPrefs.GetString(currentCultureKey);
                try
                {
                    dropdown.value = Utils.ConvertEnumToInt<Languages>(CurrentCulture);
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                    Debug.Log("language is not found");
                    dropdown.value = Utils.ConvertEnumToInt<Languages>(CurrentCulture);
                }
            }
            else
            {
                dropdown.value = Utils.ConvertEnumToInt<Languages>(CurrentCulture);
            }
        }

        private void OnSelected(int value)
        {
            var languagesArray = Utils.GetEnumValues<Languages>();
            foreach (var lang in languagesArray)
            {
                CurrentCulture = lang.ToString();
            }

            CurrentCulture = Utils.ConvertEnumToString<Languages>(value);
            onCultureChanged?.Invoke();
            PlayerPrefs.SetString(currentCultureKey, CurrentCulture);
        }

        private void OnDestroy()
        {
            PlayerPrefs.SetString(currentCultureKey, CurrentCulture);
        }
    }
}