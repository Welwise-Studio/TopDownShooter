using UnityEngine;

namespace UI.MainMenu
{
    public sealed class MainMenuModel
    {
        public readonly Sprite Logo;
        public readonly string PlaySceneName;

        public MainMenuModel(Sprite logo, string playSceneName)
        {
            Logo = logo;
            PlaySceneName = playSceneName;
        }
    }
}
