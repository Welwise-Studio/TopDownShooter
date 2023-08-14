using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenuHolder;
    [SerializeField] private GameObject _optionsMenuHolder;

    [SerializeField] private Slider[] _volumeSliders;
    [SerializeField] private Toggle[] _resolutionToggles;
    [SerializeField] private int[] _screenWidths;
    [SerializeField] private Toggle _fullscreenToggle;

    private int _activeScreenResIndex;

    private void Start()
    {
        _activeScreenResIndex = PlayerPrefs.GetInt("ScreenResIndex");
        bool isFullscreen = PlayerPrefs.GetInt("Fullscreen") == 1 ? true : false;

        _volumeSliders[0].value = AudioManager.Instance.masterVolumePercent;
        _volumeSliders[1].value = AudioManager.Instance.musicVolumePercent;
        _volumeSliders[2].value = AudioManager.Instance.sfxVolumePercent;

        for (int i = 0; i < _resolutionToggles.Length; i++)
        {
            _resolutionToggles[i].isOn = i == _activeScreenResIndex;
        }

        _fullscreenToggle.isOn = isFullscreen;
    }
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
    public void OptionsMenu()
    {
        _mainMenuHolder.SetActive(false);
        _optionsMenuHolder.SetActive(true);
    }
    public void MainMenu()
    {
        _optionsMenuHolder.SetActive(false);
        _mainMenuHolder.SetActive(true);
    }
    public void SetScreenResolution(int i)
    {
        if (_resolutionToggles[i].isOn)
        {
            _activeScreenResIndex = i;
            float aspectRatio = 16 / 9;
            Screen.SetResolution(_screenWidths[i], (int)(_screenWidths[i] / aspectRatio), false);

            PlayerPrefs.SetInt("ScreenResIndex", _activeScreenResIndex);
            PlayerPrefs.Save();
        }
    }
    public void SetFullscreen(bool isFullscreen)
    {
        for (int i = 0; i < _resolutionToggles.Length; i++)
        {
            _resolutionToggles[i].interactable = !isFullscreen;
        }

        if (isFullscreen)
        {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[^1];
            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else
        {
            SetScreenResolution(_activeScreenResIndex);
        }

        PlayerPrefs.SetInt("Fullscreen", ((isFullscreen) ? 1 : 0));
        PlayerPrefs.Save();
    }
    public void SetMasterVolume(float value)
    {
        AudioManager.Instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }
    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }
    public void SetSfxVolume(float value)
    {
        AudioManager.Instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        UnityEditor.EditorApplication.ExitPlaymode();
    }
}
