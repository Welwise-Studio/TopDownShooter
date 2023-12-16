using System;
using UI.MainMenu;
using SettingsModel = Domain.SettingsSystem.Settings;

namespace UI.Settings
{
    public sealed class SettingsPresenter : IDisposable
    {
        private readonly SettingsView _settingsView;
        private readonly SettingsModel _model;
        private readonly MainMenuView _mainMenuView;

        public SettingsPresenter(SettingsView settingsView, SettingsModel model, MainMenuView mainMenuView)
        {
            _settingsView = settingsView;
            _model = model;
            _mainMenuView = mainMenuView;

            Init();
        }

        public void Dispose()
        {
            _settingsView?.SFXVolumeSlider?.Slider?.onValueChanged.RemoveListener(OnSFXVolumeChanged);
            _settingsView?.MusicVolumeSlider?.Slider?.onValueChanged.RemoveListener(OnMusicVolumeChanged);
            _settingsView?.MasterVolumeSlider?.Slider?.onValueChanged.RemoveListener(OnMasterVolumeChanged);
        }

        private void Init()
        {
            _settingsView.SFXVolumeSlider.Slider.onValueChanged.AddListener(OnSFXVolumeChanged);
            _settingsView.MusicVolumeSlider.Slider.onValueChanged.AddListener(OnMusicVolumeChanged);
            _settingsView.MasterVolumeSlider.Slider.onValueChanged.AddListener(OnMasterVolumeChanged);
            _settingsView.BackButton.onClick.AddListener(OnBackClicked);

            _settingsView.SFXVolumeSlider.Slider.value = _model.SFXVolume.Value;
            _settingsView.MusicVolumeSlider.Slider.value = _model.MusicVolume.Value;
            _settingsView.MasterVolumeSlider.Slider.value = _model.MasterVolume.Value;

            _model.MusicVolume.Subscribe(UpdateMusicVolumeValue);
            _model.SFXVolume.Subscribe(UpdateSFXVolumeValue);
            _model.MasterVolume.Subscribe(UpdateMasterVolumeValue);
        }

        private void OnSFXVolumeChanged(float value) => _model.SFXVolume.Value = value;
        private void OnMusicVolumeChanged(float value) => _model.MusicVolume.Value = value;
        private void OnMasterVolumeChanged(float value) => _model.MasterVolume.Value = value;

        private void UpdateMasterVolumeValue(float value) => _settingsView.MasterVolumeSlider.Slider.value = value;
        private void UpdateSFXVolumeValue(float value) => _settingsView.SFXVolumeSlider.Slider.value = value;
        private void UpdateMusicVolumeValue(float value) => _settingsView.MusicVolumeSlider.Slider.value = value;

        private void OnBackClicked()
        {
            _settingsView.Hide();
            _mainMenuView.Show();
            _model.Save();
        }
    }
}
