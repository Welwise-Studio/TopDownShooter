using Architecture.DI;
using Architecture.Installers;
using Domain.SettingsSystem;

namespace Domain.Installers
{
    public class SettingsInstaller : MonoInstaller
    {
        public override void InstallBindings(IDIContainer container)
        {
            var settings = new SettingsFactory().FromYG();
            container.Bind(settings);
        }
    }
}
