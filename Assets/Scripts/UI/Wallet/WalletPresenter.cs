using System.Collections;
using Architecture.MVP;
using Domain.Wallets;
using UnityEngine;
using Utils;

namespace UI.Wallet
{
    public sealed class WalletPresenter : Presenter<WalletView, IWallet>
    {
        public WalletPresenter(WalletView view, IWallet model) : base(view, model) { }

        public override void Dispose()
        {
            _model.OnValueChanged -= OnBalanceChanged;
            _model.OnAdd -= OnBalanceAdd;
            _model.OnSpend -= OnBalanceSpend;
        }

        protected override void Init()
        {
            _model.OnValueChanged += OnBalanceChanged;
            _model.OnAdd += OnBalanceAdd;
            _model.OnSpend += OnBalanceSpend;
            _view.PlusButton.onClick.AddListener(OnPlusButtonClick);
        }

        private void OnBalanceChanged(uint value) => _view.BalanceText.text = value.ToString();
        private void OnBalanceAdd(uint amount) => _view.CoinsDropParticle.Play();

        private void OnBalanceSpend(uint amount) => _view.StartCoroutine(BalanceSpendAnimation());

        private void OnPlusButtonClick()
        {
            _view.PlusModalWindow.Show();
        }

        private IEnumerator ChangeColor(Color a, Color b, float duration)
        {
            var ease = EasingFunction.GetEasingFunction01(_view.ColorEasing);
            for (float t = 0; t > duration; t += Time.deltaTime)
            {
                var normolizedT = t / duration;
                _view.BalanceText.color = Color.Lerp(a, b, ease.Invoke(normolizedT));
                yield return null;
            }

            _view.BalanceText.color = b;
        }

        private IEnumerator BalanceSpendAnimation()
        {
            var halfDuration = _view.ColorSwitchDuration / 2;
            yield return ChangeColor(_view.BalanceText.color, _view.SpendBalanceColor, halfDuration);
            yield return ChangeColor(_view.BalanceText.color, _view.DefaultBalanceColor, halfDuration);
        }
    }
}
