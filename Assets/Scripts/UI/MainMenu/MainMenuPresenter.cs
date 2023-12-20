using Architecture.MVP;
using System;
using UI.Settings;
using UnityEngine.SceneManagement;

namespace UI.MainMenu
{
    public sealed class MainMenuPresenter : Presenter<MainMenuView, MainMenuModel>
    {
        public MainMenuPresenter(MainMenuView view, MainMenuModel model) : base(view, model) { }

        public override void Dispose()
        {
            _view?.SettingsButton?.onClick.RemoveListener(OnSettingsClick);
            _view?.PlayButton?.onClick.RemoveListener(OnPlayClick);
        }

        protected override void Init()
        {
            _view.SettingsButton.onClick.AddListener(OnSettingsClick);
            _view.PlayButton.onClick.AddListener(OnPlayClick);
        }

        private void OnPlayClick() => SceneManager.LoadScene(_model.PlaySceneName);
        private void OnSettingsClick()
        {
            _view.Hide();
            _view.SettingsWindow.Show();
        }
    }
}
