using UnityEngine;

namespace UI.Settings
{
    public class SettingsModel
    {
        public float SFXVolume 
        { 
            get => _sfxVolume;
            set => _sfxVolume = Mathf.Clamp01(value);
        }

        public float MasterVolume
        {
            get => _masterVolume;
            set => _masterVolume = Mathf.Clamp01(value);
        }

        public float MusicVolume
        {
            get => _musicVolume;
            set => _musicVolume = Mathf.Clamp01(value);
        }

        private float _sfxVolume;
        private float _masterVolume;
        private float _musicVolume;

        public SettingsModel(float sfx, float master, float music)
        {
            _sfxVolume = sfx;
            _masterVolume = master;
            _musicVolume = music;
        }
    }
}
