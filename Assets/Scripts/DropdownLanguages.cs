using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace EnglishByPictures
{
    public class DropdownLanguages : MonoBehaviour
    {
        [SerializeField] TMP_Dropdown dropdown;
        string currentCultureKey = "CurrentCulture";
        public string CurrentCulture { get; set; } = "Ru";
        public event Action onCultureChanged; 

        private void OnValidate()
        {
            if (!dropdown)
            { dropdown = GetComponent<TMP_Dropdown>();
                dropdown.AddOptions(Enum
                    .GetValues(typeof(Languages))
                    .Cast<Languages>()
                    .Select(d => d.ToString())
                    .ToList());
            }
        }

        private void OnEnable() => dropdown.onValueChanged.AddListener(OnSelected);

        private void OnDisable() => dropdown.onValueChanged.RemoveListener(OnSelected);

        private void Start()
        {
            if (PlayerPrefs.HasKey(currentCultureKey))
            {
                CurrentCulture = PlayerPrefs.GetString(currentCultureKey);
                dropdown.value = Utils.ConvertEnumToInt(CurrentCulture);
            }
        }

        public void OnSelected(int value)
        {
            if (value == (int)Languages.Ru)
            {
                CurrentCulture = Languages.Ru.ToString();
            }
            else if (value == (int)Languages.De)
            {
                CurrentCulture = Languages.De.ToString();
            }
            else if (value == (int)Languages.Uk)
            {
                CurrentCulture = Languages.Uk.ToString();
            }

            CurrentCulture = Utils.ConvertEnumToString(value);
            onCultureChanged?.Invoke();
            PlayerPrefs.SetString(currentCultureKey, CurrentCulture);
        }
    }
}