using AYellowpaper.SerializedCollections;
using UnityEngine;
using Utils;

namespace UI.MainMenu
{
    [CreateAssetMenu(fileName = "mainMenuResources", menuName = "Resources/Create Main Menu Resources", order = 1)]
    public class MainMenuModelResources : ScriptableObject
    {
        [System.Serializable]
        private struct LogoWithLang
        {
            public string Lang;
            public Sprite Logo;
        }

        [field: SerializeField]
        public SceneField PlayScene { get; private set; }

        [Header("Logos")]
        [SerializeField]
        private Sprite _defaultLogo;

        [SerializeField]
        [SerializedDictionary("Language", "Logo (Sprite)")]
        private SerializedDictionary<string, Sprite> _logos;

        public Sprite GetLogo(string lang)
        {
            if (_logos.ContainsKey(lang))
                return _logos[lang];

            return _defaultLogo;
        }
    }
}
