using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EnglishByPictures
{
    public class ThemeSettings : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI mainText;
        [SerializeField] TextMeshProUGUI wordsLeftText;
        [SerializeField] TextMeshProUGUI finishText;
        [SerializeField] TextMeshProUGUI translatedText;
        [SerializeField] Color blackColor;
        [SerializeField] Color whiteColor;
        [SerializeField] Image backgroundImage;
        [SerializeField] Image arrowImage;
        [SerializeField] Button switchThemeButton;

        Theme theme = Theme.Black;
        private LoadAndSave loadAndSave;

        private void OnEnable()
        {
            switchThemeButton.onClick.AddListener(SwitchTheme);
        }

        private void OnDisable()
        {
            switchThemeButton.onClick.RemoveListener(SwitchTheme);
        }

        private void Start()
        {
            loadAndSave = new LoadAndSave();
            theme = loadAndSave.LoadThemeSettings();
            SetTheme();
        }

        public void SetTheme()
        {
            Color buttonTextColor;
            Color textColor;
            if (theme == Theme.Black)
            {
                buttonTextColor = whiteColor;
                textColor = blackColor;
                switchThemeButton.image.sprite = Resources.Load<Sprite>("Tools/SunBlack");
                arrowImage.sprite = Resources.Load<Sprite>("Tools/ArrowWhite");
            }
            else
            {
                switchThemeButton.image.sprite = Resources.Load<Sprite>("Tools/SunWhite");;
                arrowImage.sprite = Resources.Load<Sprite>("Tools/ArrowBlack");;
                textColor = whiteColor;
                buttonTextColor = blackColor;
            }

            finishText.color = textColor;
            translatedText.color = textColor;
            backgroundImage.color = buttonTextColor;
            mainText.color = textColor;
            wordsLeftText.color = textColor;
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
    }
}