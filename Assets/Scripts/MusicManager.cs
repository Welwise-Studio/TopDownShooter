using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip _menuTheme;
    [SerializeField] private AudioClip[] _mainThemes;
    private string _sceneName;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string newSceneName = scene.name;

        if (newSceneName != _sceneName)
        {
            _sceneName = newSceneName;
            Invoke("PlayMusic", 0.2f);
        }
    }
    private void PlayMusic()
    {
        AudioClip clipToPlay = null;

        if (_sceneName == "Menu")
        {
            clipToPlay = _menuTheme;
        }
        else if (_sceneName == "Game")
        {
            clipToPlay = _mainThemes[Random.Range(0, _mainThemes.Length)];
        }

        if (clipToPlay != null)
        {
            AudioManager.Instance.PlayMusic(clipToPlay, 2);
            Invoke("PlayMusic", clipToPlay.length);
        }
    }
}
