using System;
using UI.Settings;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public sealed class MainMenuPresenter : IDisposable
    {
        private readonly MainMenuView _mainMenuView;
        private readonly SettingsView _settingsView;
        private readonly MainMenuModel _mainMenuModel;

        public MainMenuPresenter(MainMenuView menuView, MainMenuModel model, SettingsView settingsView)
        {
            _mainMenuView = menuView;
            _settingsView = settingsView;
            _mainMenuModel = model;

            Init();
        }

        private void Init()
        {
            _mainMenuView.SettingsButton.onClick.AddListener(OnSettingsClick);
            _mainMenuView.PlayButton.onClick.AddListener(OnPlayClick);
        }

        public void Dispose()
        {
            _mainMenuView?.SettingsButton?.onClick.RemoveListener(OnSettingsClick);
            _mainMenuView?.PlayButton?.onClick.RemoveListener(OnPlayClick);
        }

        private void OnPlayClick() => SceneManager.LoadScene(_mainMenuModel.PlaySceneName);
        private void OnSettingsClick()
        {
            _mainMenuView.Hide();
            _settingsView.Show();
        }
    }
}
