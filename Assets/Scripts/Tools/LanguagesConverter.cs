using System;

namespace EnglishByPictures
{
    public static class LanguagesConverter
    {
        public static string ConvertToText(Languages lang)
        {
            switch (lang)
            {
                case Languages.Af:
                    return LanguagesFull.Afrikaans.ToString();
                case Languages.Az:
                    return LanguagesFull.Azerbaijani.ToString();
                case Languages.Bg:
                    return LanguagesFull.Bulgarian.ToString();
                case Languages.Ca:
                    return LanguagesFull.Catalan.ToString();
                case Languages.Cs:
                    return LanguagesFull.Czech.ToString();
                case Languages.Cy:
                    return LanguagesFull.Welsh.ToString();
                case Languages.Da:
                    return LanguagesFull.Danish.ToString();
                case Languages.De:
                    return LanguagesFull.German.ToString();
                case Languages.El:
                    return LanguagesFull.Greek.ToString();
                case Languages.Es:
                    return LanguagesFull.Spanish.ToString();
                case Languages.Et:
                    return LanguagesFull.Estonian.ToString();
                case Languages.Eu:
                    return LanguagesFull.Basque.ToString();
                case Languages.Fi:
                    return LanguagesFull.Finnish.ToString();
                case Languages.Fil:
                    return LanguagesFull.Filipino.ToString();
                case Languages.Fr:
                    return LanguagesFull.French.ToString();
                case Languages.Gl:
                    return LanguagesFull.Galician.ToString();
                case Languages.Hr:
                    return LanguagesFull.Croatian.ToString();
                case Languages.Hu:
                    return LanguagesFull.Hungarian.ToString();
                case Languages.Id:
                    return LanguagesFull.Indonesian.ToString();
                case Languages.Is:
                    return LanguagesFull.Icelandic.ToString();
                case Languages.It:
                    return LanguagesFull.Italian.ToString();
                case Languages.Iw:
                    return LanguagesFull.Hebrew.ToString();
                case Languages.Kk:
                    return LanguagesFull.Kazakh.ToString();
                case Languages.Ky:
                    return LanguagesFull.Kirghiz.ToString();
                case Languages.Lt:
                    return LanguagesFull.Lithuanian.ToString();
                case Languages.Lv:
                    return LanguagesFull.Latvian.ToString();
                case Languages.Mk:
                    return LanguagesFull.Macedonian.ToString();
                case Languages.Mn:
                    return LanguagesFull.Mongolian.ToString();
                case Languages.Ms:
                    return LanguagesFull.Malay.ToString();
                case Languages.Nl:
                    return LanguagesFull.Dutch.ToString();
                case Languages.No:
                    return LanguagesFull.Norwegian.ToString();
                case Languages.Pl:
                    return LanguagesFull.Polish.ToString();
                case Languages.Pt:
                    return LanguagesFull.Portuguese.ToString();
                case Languages.Ro:
                    return LanguagesFull.Romanian.ToString();
                case Languages.Ru:
                    return LanguagesFull.Russian.ToString();
                case Languages.Sk:
                    return LanguagesFull.Slovak.ToString();
                case Languages.Sl:
                    return LanguagesFull.Slovenian.ToString();
                case Languages.Sq:
                    return LanguagesFull.Albanian.ToString();
                case Languages.Sr:
                    return LanguagesFull.Serbian.ToString();
                case Languages.Sv:
                    return LanguagesFull.Swedish.ToString();
                case Languages.Sw:
                    return LanguagesFull.Swahili.ToString();
                case Languages.Tr:
                    return LanguagesFull.Turkish.ToString();
                case Languages.Uk:
                    return LanguagesFull.Ukrainian.ToString();
                case Languages.Uz:
                    return LanguagesFull.Uzbek.ToString();
                case Languages.Zu:
                    return LanguagesFull.Zulu.ToString();
                default:
                    throw new ArgumentOutOfRangeException(nameof(lang), lang, null);
            }
        }
    }
}