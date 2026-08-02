using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

namespace Rune.Localization
{
    /// <summary>
    /// Unity Localization을 간편하게 사용하기 위한 static 유틸.
    /// </summary>
    public static class Locale
    {
        /// <summary>
        /// 테이블과 엔트리 키로 로컬라이즈된 문자열을 가져옵니다.
        /// </summary>
        public static string Get(string table, string entry)
        {
            return LocalizationSettings.StringDatabase
                .GetLocalizedString(table, entry);
        }

        /// <summary>
        /// 현재 언어를 변경합니다.
        /// </summary>
        public static void SetLanguage(string languageCode)
        {
            var locale = LocalizationSettings.AvailableLocales
                .GetLocale(languageCode);

            if (locale != null)
            {
                LocalizationSettings.SelectedLocale = locale;
            }
        }

        /// <summary>
        /// 현재 선택된 언어 코드를 반환합니다.
        /// </summary>
        public static string CurrentLanguage =>
            LocalizationSettings.SelectedLocale?.Identifier.Code ?? "";
    }
}
