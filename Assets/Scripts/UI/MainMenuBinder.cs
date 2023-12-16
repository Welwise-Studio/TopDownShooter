using Architecture.Contexts;
using UI.MainMenu;
using UI.Settings;
using UnityEngine;

namespace UI
{
    public class MainMenuBinder : MonoBehaviour
    {
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private SettingsView _settingsView;

        private SettingsPresenter _settingsPresenter;
        private MainMenuPresenter _mainMenuPresenter;

        private void Start()
        {
            var mainMenuModel = new MainMenuModelFactory().Create();
            _mainMenuPresenter = new MainMenuPresenter(_mainMenuView, mainMenuModel, _settingsView);

            var settingsModel = ProjectContext.Instance.Container.Resolve<Domain.SettingsSystem.Settings>();
            _settingsPresenter = new SettingsPresenter(_settingsView, settingsModel, _mainMenuView);
        }

        private void OnDestroy()
        {
            _mainMenuPresenter.Dispose();
            _settingsPresenter.Dispose();
        }
    }
}