using Architecture.Contexts;
using Architecture.MVP;
using Domain.Wallets;


namespace UI.Wallet
{
    public sealed class WalletPresenterFactory : PresenterFactory<WalletView, WalletPresenter, IWallet>
    {
        protected override WalletPresenter Create()
        {
            _model = ProjectContext.Instance.Container.Resolve<IWallet>();
            return new WalletPresenter(_view, _model);
        }
    }
}
