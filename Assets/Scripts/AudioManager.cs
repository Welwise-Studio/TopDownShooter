using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public enum AudioChannel { Master, Sfx, Music };

    public float masterVolumePercent { get; private set; }
    public float sfxVolumePercent { get; private set; }
    public float musicVolumePercent { get; private set; }

    private AudioSource _sfx2DSource;
    private AudioSource[] _musicSources;
    private int _activeMusicSourceIndex;

    private Transform _audioListener;
    private Transform _playerT;

    private SoundLibrary _library;
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _library = GetComponent<SoundLibrary>();

        _musicSources = new AudioSource[2];

        for (int i = 0; i < _musicSources.Length; i++)
        {
            GameObject newMusicSource = new GameObject($"MusicSource{i + 1}");
            _musicSources[i] = newMusicSource.AddComponent<AudioSource>();
            newMusicSource.transform.parent = transform;
        }

        GameObject newSfx2DSource = new GameObject($"2D_SFX_Source");
        _sfx2DSource = newSfx2DSource.AddComponent<AudioSource>();
        newSfx2DSource.transform.parent = transform;

        _audioListener = FindObjectOfType<AudioListener>().transform;

        if (FindObjectOfType<Player>() != null)
        {
            _playerT = FindObjectOfType<Player>().transform;
        }

        masterVolumePercent = PlayerPrefs.GetFloat("Master Volume", 1);
        sfxVolumePercent = PlayerPrefs.GetFloat("SFX Volume", 1);
        musicVolumePercent = PlayerPrefs.GetFloat("Music Volume", 1);
        PlayerPrefs.Save();
    }
    private void Update()
    {
        if (_playerT != null)
        {
            _audioListener.position = _playerT.position;
        }
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (_playerT == null)
        {
            if (FindObjectOfType<Player>() != null)
            {
                _playerT = FindObjectOfType<Player>().transform;
            }
        }
    }
    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                break;

            case AudioChannel.Sfx:
                sfxVolumePercent = volumePercent;
                break;

            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
        }

        _musicSources[0].volume = musicVolumePercent * masterVolumePercent;
        _musicSources[1].volume = musicVolumePercent * masterVolumePercent;

        PlayerPrefs.SetFloat("Master Volume", masterVolumePercent);
        PlayerPrefs.SetFloat("SFX Volume", sfxVolumePercent);
        PlayerPrefs.SetFloat("Music Volume", musicVolumePercent);
        PlayerPrefs.Save();
    }
    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        _activeMusicSourceIndex = 1 - _activeMusicSourceIndex;
        _musicSources[_activeMusicSourceIndex].clip = clip;
        _musicSources[_activeMusicSourceIndex].Play();

        StartCoroutine(AnimateMusicCrossfade(fadeDuration));
    }
    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
        }
    }
    public void PlaySound(string soundName, Vector3 pos)
    {
        PlaySound(_library.GetClipFromName(soundName), pos);
    }
    public void PlaySound2D(string soundName)
    {
        _sfx2DSource.PlayOneShot(_library.GetClipFromName(soundName), sfxVolumePercent * masterVolumePercent);
    }
    private IEnumerator AnimateMusicCrossfade(float duration)
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            _musicSources[_activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
            _musicSources[1 - _activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);

            yield return null;
        }
    }
}
