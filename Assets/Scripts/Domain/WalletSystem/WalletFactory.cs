using YG;

namespace Domain.WalletSystem
{
    public class WalletFactory
    {
        public IWallet FromYG() => new Wallet((uint)YandexGame.savesData.Balance);
    }
}
