using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayableDirector))]
public class CatSceneLoader : MonoBehaviour
{
    public static string NextSceneName;

    private PlayableDirector _playableDirector;

    private void OnEnable()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        _playableDirector.stopped += LoadScene;
    }

    private void OnDisable()
    {
        _playableDirector.stopped -= LoadScene;
    }

    private void LoadScene(PlayableDirector p) => SceneManager.LoadScene(NextSceneName);
}
