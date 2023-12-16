using UnityEngine;

namespace AudioManagement
{
    public class AudioSourceWorker
    {
        public bool IsPlayLoop { get; private set; }
        private AudioSource _audioSource;

        public AudioSourceWorker(AudioSource source)
        {
            _audioSource = source;
            ClearSource();
        }

        public void PlayOneShoot(AudioClip clip) => _audioSource.PlayOneShot(clip);
        public void SetLoop(AudioClip clip)
        {
            ClearSource();
            _audioSource.clip = clip;
            _audioSource.loop = true;
            IsPlayLoop = true;
            _audioSource.Play();
        }

        public void StopLoop()
        {
            ClearSource();
            IsPlayLoop = false;
        }

        private void ClearSource()
        {
            _audioSource.Stop();
            _audioSource.loop = false;
            _audioSource.clip = null;
            _audioSource.playOnAwake = false;
        }
    }
}
