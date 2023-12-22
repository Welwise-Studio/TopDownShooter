using Architecture.MVP;

namespace UI.MainMenu
{
    public sealed class MainMenuPresenterFactory : PresenterFactory<MainMenuView, MainMenuPresenter, MainMenuModel>
    {
        protected override MainMenuPresenter Create()
        {
            _model = new MainMenuModelFactory().Create();
            return new MainMenuPresenter(_view, _model);
        }
    }
}