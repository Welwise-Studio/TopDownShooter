using Architecture.Contexts;
using Architecture.MVP;
using SettingsModel = Domain.SettingsSystem.Settings;

namespace UI.Settings
{
    public sealed class SettingsBinder : Binder<SettingsView, SettingsPresenter, SettingsModel>
    {
        protected override void Bind()
        {
            _model = ProjectContext.Instance.Container.Resolve<SettingsModel>();
            _presenter = new SettingsPresenter(_view, _model);
        }

        protected override void Unbind()
        {
            _presenter.Dispose();
        }
    }
}
