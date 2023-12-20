using Architecture.MVP;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public sealed class MainMenuView : View
    {
        [field: SerializeField]
        public Button SettingsButton {  get; private set; }

        [field: SerializeField]
        public Button PlayButton { get; private set; }

        [field: SerializeField]
        public Image LogoImage { get; private set; }

        [field: SerializeField]
        public View SettingsWindow { get; private set; }

    }
}
