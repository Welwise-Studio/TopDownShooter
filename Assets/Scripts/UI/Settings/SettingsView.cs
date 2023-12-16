using Architecture.MVP;
using System;
using UI.Components;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    public sealed class SettingsView : MonoBehaviour, IView
    {

        [field: SerializeField] 
        public SliderWithTitleView MasterVolumeSlider { get; private set; }

        [field: SerializeField]
        public SliderWithTitleView MusicVolumeSlider { get; private set; }

        [field: SerializeField]
        public SliderWithTitleView SFXVolumeSlider { get; private set; }

        [field: SerializeField]
        public Button BackButton { get; private set; }

        public event Action OnShow;
        public event Action OnHide;

        public void Hide()
        {
            this.gameObject.SetActive(false);
            this.OnHide?.Invoke();
        }


        public void Show() 
        {
            this.gameObject.SetActive(true);
            OnShow?.Invoke();
        }
    }
}
