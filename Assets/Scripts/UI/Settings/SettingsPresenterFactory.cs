using Architecture.Contexts;
using Architecture.MVP;
using SettingsModel = Domain.SettingsSystem.Settings;

namespace UI.Settings
{
    public sealed class SettingsPresenterFactory : PresenterFactory<SettingsView, SettingsPresenter, SettingsModel>
    {
        protected override SettingsPresenter Create()
        {
            _model = ProjectContext.Instance.Container.Resolve<SettingsModel>();
            return new SettingsPresenter(_view, _model);
        }
    }
}
