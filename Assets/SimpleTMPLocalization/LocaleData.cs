using System.Collections.Generic;
using TMPro;

namespace SimpleTMPLocalization
{
    public static class LocaleData
    {
        private static bool _isInit = false;

        private static LocaleStorage _storage;

        public static void Init()
        {
            if (!_isInit)
                return;

            _storage = LocaleStorage.GetFromResources();

            _isInit = true;
        }

        public static string GetText(string id, string lang)
        {
            if (!_isInit)
                Init();

            return _storage.GetTextByIdAndLang(id, lang);
        }
        public static Dictionary<string, string> GetTexts(string id)
        {
            if (!_isInit)
                Init();

            return _storage.GetTextsDataById(id);
        }

        public static TMP_FontAsset GetFontAsset(string lang)
        {
            if (!_isInit)
                Init();

            return _storage.GetFontByLang(lang);
        }
    }
}
