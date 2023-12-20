using Architecture.Contexts;
using Architecture.MVP;
using Domain.Wallets;


namespace UI.Wallet
{
    public sealed class WalletBinder : Binder<WalletView, WalletPresenter, IWallet>
    {
        protected override void Bind()
        {
            _model = ProjectContext.Instance.Container.Resolve<IWallet>();
            _presenter = new WalletPresenter(_view, _model);
        }

        protected override void Unbind()
        {
            _presenter.Dispose();
        }
    }
}
