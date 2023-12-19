using System;

namespace Environment
{
    public static class LanguageHelper
    {
        public static Languages GetLanguage(string code) => Enum.TryParse(typeof(Languages), code.ToUpper(), out var lang) ? (Languages)lang : Languages.EN;
    }
}
