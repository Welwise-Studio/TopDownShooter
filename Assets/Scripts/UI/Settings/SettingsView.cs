using Architecture.MVP;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    public sealed class SettingsView : View
    {

        [field: SerializeField] 
        public SliderWithTitleView MasterVolumeSlider { get; private set; }

        [field: SerializeField]
        public SliderWithTitleView MusicVolumeSlider { get; private set; }

        [field: SerializeField]
        public SliderWithTitleView SFXVolumeSlider { get; private set; }

        [field: SerializeField]
        public Button BackButton { get; private set; }

        [field: SerializeField]
        public View MainMenuWindow { get; private set;}
    }
}
