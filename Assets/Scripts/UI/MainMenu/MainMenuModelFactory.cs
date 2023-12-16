using UnityEngine;
using YG;

namespace UI.MainMenu
{
    public class MainMenuModelFactory
    {
        private static readonly string _pathToResources = "UI/mainMenuResources";

        public MainMenuModel Create()
        {
            var resource = Resources.Load<MainMenuModelResources>(_pathToResources);
            return new MainMenuModel(resource.GetLogo(YandexGame.EnvironmentData.language), resource.PlayScene.SceneName);
        }
    }
}
