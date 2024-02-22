using System;
using System.Collections.Generic;

namespace SimpleTMPLocalization
{
    [Serializable]
    public class LocaleText
    {
        public readonly Dictionary<string, string> LangText = new Dictionary<string, string>();
    }
}
