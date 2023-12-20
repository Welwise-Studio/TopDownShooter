using Architecture.MVP;
using SettingsModel = Domain.SettingsSystem.Settings;

namespace UI.Settings
{
    public sealed class SettingsPresenter : Presenter<SettingsView, SettingsModel>
    {
        public SettingsPresenter(SettingsView view, SettingsModel model) : base(view, model) { }

        public override void Dispose()
        {
            _view?.SFXVolumeSlider?.Slider?.onValueChanged.RemoveListener(OnSFXVolumeChanged);
            _view?.MusicVolumeSlider?.Slider?.onValueChanged.RemoveListener(OnMusicVolumeChanged);
            _view?.MasterVolumeSlider?.Slider?.onValueChanged.RemoveListener(OnMasterVolumeChanged);
        }

        protected override void Init()
        {
            _view.SFXVolumeSlider.Slider.onValueChanged.AddListener(OnSFXVolumeChanged);
            _view.MusicVolumeSlider.Slider.onValueChanged.AddListener(OnMusicVolumeChanged);
            _view.MasterVolumeSlider.Slider.onValueChanged.AddListener(OnMasterVolumeChanged);
            _view.BackButton.onClick.AddListener(OnBackClicked);

            _view.SFXVolumeSlider.Slider.value = _model.SFXVolume.Value;
            _view.MusicVolumeSlider.Slider.value = _model.MusicVolume.Value;
            _view.MasterVolumeSlider.Slider.value = _model.MasterVolume.Value;

            _model.MusicVolume.Subscribe(UpdateMusicVolumeValue);
            _model.SFXVolume.Subscribe(UpdateSFXVolumeValue);
            _model.MasterVolume.Subscribe(UpdateMasterVolumeValue);
        }

        private void OnSFXVolumeChanged(float value) => _model.SFXVolume.Value = value;
        private void OnMusicVolumeChanged(float value) => _model.MusicVolume.Value = value;
        private void OnMasterVolumeChanged(float value) => _model.MasterVolume.Value = value;

        private void UpdateMasterVolumeValue(float value) => _view.MasterVolumeSlider.Slider.value = value;
        private void UpdateSFXVolumeValue(float value) => _view.SFXVolumeSlider.Slider.value = value;
        private void UpdateMusicVolumeValue(float value) => _view.MusicVolumeSlider.Slider.value = value;

        private void OnBackClicked()
        {
            _view.Hide();
            _view.MainMenuWindow.Show();
            _model.Save();
        }
    }
}
