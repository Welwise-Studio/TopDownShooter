using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SimpleTMPLocalization
{
    public class LocaleStorage : ScriptableObject
    {
        [Serializable]
        public class Config
        {
            [SerializeField]
            public string Path = "locales";

            [SerializeField]
            public string DefaultFontKey = "default";
        }

        [SerializeField]
        private static Config _cfg = new();

        [SerializeField]
        private Dictionary<string, Dictionary<string, string>> _storage;

        [SerializeField]
        private Dictionary<string, TMP_FontAsset> _fonts;

        public Dictionary<string, string> GetTextsDataById(string id)
        {
            if (_storage.TryGetValue(id, out var texts))
                return texts;
            else
                throw new KeyNotFoundException($"Storage dosen't contain data by id<{id}>");

        }

        public string GetTextByIdAndLang(string id, string lang)
        {
            if (_storage.TryGetValue(id, out var texts) && texts.TryGetValue(lang, out var text))
                return text;
            else 
                throw new KeyNotFoundException(texts ==null ? $"Storage dosen't contain data by id<{id}>" : $"Storage dosen't contain data by lang<{lang}>");
        }

        public TMP_FontAsset GetFontByLang(string lang) => _fonts.ContainsKey(lang) ? _fonts[lang] : _fonts[_cfg.DefaultFontKey];

        public static LocaleStorage GetFromResources()
        {
            var target = Resources.Load<LocaleStorage>(_cfg.Path);
            if (target != null)
                return target;
            else
                throw new Exception("LocaleStorage SO not found in Resources");
        }
    }
}
