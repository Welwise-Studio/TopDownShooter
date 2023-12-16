using AYellowpaper.SerializedCollections;
using Domain.SettingsSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace AudioManagement
{
    public class AudioCore : MonoBehaviour
    {
        public static readonly float MinVolume = -80;
        public static readonly float MaxVolume = 20;
        private static readonly int _initialPoolSize = 3;

        public Action<float> OnVolumeChange;

        [SerializeField] 
        private AudioMixer _audioMixer;
        [Space(10)]
        [SerializeField]
        [SerializedDictionary]
        private SerializedDictionary<PlayableChannels, AudioSource> _audioPrefabs = new SerializedDictionary<PlayableChannels, AudioSource>();
        [SerializeField]
        private Transform _container;

        private Dictionary<PlayableChannels, WorkerPool> _audioInstances = new Dictionary<PlayableChannels, WorkerPool>();

        private void Awake()
        {
            foreach (var pair in _audioPrefabs)
            {
                _audioInstances[pair.Key] = new WorkerPool(_container, pair.Value, _initialPoolSize);
            }

        }

        private void Start()
        {
            SceneManager.activeSceneChanged += PrepareForNextScene;
        }

        public void BindSettings(Settings settings)
        {
            settings.SFXVolume.Subscribe((value) => SetVolume(MixedChannles.SFX, value));
            settings.MusicVolume.Subscribe((value) => SetVolume(MixedChannles.Music, value));
            settings.MasterVolume.Subscribe((value) => SetVolume(MixedChannles.Master, value));
        }

        public void SetVolume(MixedChannles channel, float value)
        {
            var lerpedValue = Mathf.Lerp(MinVolume, MaxVolume,Mathf.Clamp01(value));
            _audioMixer.SetFloat(channel.ToString(), lerpedValue);
            OnVolumeChange?.Invoke(value);
        }

        public void PlayOneShoot(AudioClip clip, PlayableChannels channel) => _audioInstances[channel].Get().PlayOneShoot(clip);

        public AudioSourceWorker SetLoop(AudioClip clip, PlayableChannels channel)
        {
            var worker = _audioInstances[channel].GetNotLooped();
            worker.SetLoop(clip);
            return worker;
        }

        private void PrepareForNextScene(Scene old, Scene next)
        {
            foreach (var pair in _audioInstances)
            {
                pair.Value.ClearAll();
            }
        }
    }
}
