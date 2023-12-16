using Architecture.DI;

namespace Architecture.Installers
{
    public interface IInstaller
    {
        void InstallBindings(IDIContainer localContainer);
    }
}
