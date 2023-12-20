using Architecture.MVP;
using TMPro;
using UI.Components.ModalWindow;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI.Wallet
{
    public sealed class WalletView : View
    {
        [field: Header("Style")]
        [field: SerializeField]
        public Color DefaultBalanceColor { get; private set; }
        [field: SerializeField]
        public Color SpendBalanceColor { get; private set; }
        [field: SerializeField]
        public ParticleSystem CoinsDropParticle { get; private set; }
        [field: SerializeField]
        public EasingFunction.Ease ColorEasing { get; private set; }
        [field: SerializeField]
        public float ColorSwitchDuration { get; private set; }



        [field: Header("References")]
        [field: SerializeField]
        public TMP_Text BalanceText { get; private set; }

        [field: SerializeField]
        public Button PlusButton { get; private set; }
        [field: SerializeField]
        public ModalWindowView PlusModalWindow { get; private set; }

    }
}
