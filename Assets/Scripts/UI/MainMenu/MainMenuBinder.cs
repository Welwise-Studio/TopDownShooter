using Architecture.MVP;

namespace UI.MainMenu
{
    public sealed class MainMenuBinder : Binder<MainMenuView, MainMenuPresenter, MainMenuModel>
    {
        protected override void Bind()
        {
            _model = new MainMenuModelFactory().Create();
            _presenter = new MainMenuPresenter(_view, _model);
        }

        protected override void Unbind()
        {
            _presenter.Dispose();
        }
    }
}