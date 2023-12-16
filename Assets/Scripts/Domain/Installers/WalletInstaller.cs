using Architecture.DI;
using Architecture.Installers;
using Domain.WalletSystem;

namespace Domain.Installers
{
    public class WalletInstaller : MonoInstaller
    {
        public override void InstallBindings(IDIContainer container)
        {
            var wallet = new WalletFactory().FromYG();
            container.Bind<IWallet>(wallet);
        }
    }
}
