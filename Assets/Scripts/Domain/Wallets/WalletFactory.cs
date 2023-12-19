using YG;

namespace Domain.Wallets
{
    public static class WalletFactory
    {
        public static IWallet Create() => new Wallet((uint)YandexGame.savesData.Balance);
    }
}
