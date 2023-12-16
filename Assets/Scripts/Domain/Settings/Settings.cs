using Architecture.Reactive;
using YG;
using UnityEngine;

namespace Domain.SettingsSystem
{
    public class Settings
    {
        public ReactiveProperty<float> SFXVolume { get; private set; }
        public ReactiveProperty<float> MasterVolume { get; private set; }
        public ReactiveProperty<float> MusicVolume { get; private set; }
        public Settings(float sfx, float master, float music)
        {
            SFXVolume = new ReactiveProperty<float>(sfx, Validator);
            MasterVolume = new ReactiveProperty<float>(master, Validator);
            MusicVolume = new ReactiveProperty<float>(music, Validator);
        }

        public void Save()
        {
            YandexGame.savesData.Sfx = SFXVolume.Value;
            YandexGame.savesData.Master = MasterVolume.Value;
            YandexGame.savesData.Music = MusicVolume.Value;
            YandexGame.SaveProgress();
        }

        private float Validator(float value) => Mathf.Clamp01(value);
    }
}
