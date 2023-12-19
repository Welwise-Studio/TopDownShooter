using Architecture.DI;
using Architecture.Installers;
using Domain.Wallets;

namespace Domain.Installers
{
    public class WalletInstaller : MonoInstaller
    {
        public override void InstallBindings(IDIContainer container)
        {
            var wallet = WalletFactory.Create();
            container.Bind<IWallet>(wallet);
        }
    }
}
