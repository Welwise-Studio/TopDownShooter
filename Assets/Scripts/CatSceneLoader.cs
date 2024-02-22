using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayableDirector))]
public class CatSceneLoader : MonoBehaviour
{
    public static string NextSceneName;

    private PlayableDirector _playableDirector;

    private float cMaster;

    private void Start()
    {
        cMaster = AudioManager.Instance.masterVolumePercent;
        AudioManager.Instance.SetVolume(0, AudioManager.AudioChannel.Master);
    }

    private void OnEnable()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        _playableDirector.stopped += LoadScene;
    }

    private void OnDisable()
    {
        _playableDirector.stopped -= LoadScene;
    }

    private void LoadScene(PlayableDirector p)
    {
        AudioManager.Instance.SetVolume(cMaster, AudioManager.AudioChannel.Master);
        SceneManager.LoadScene(NextSceneName);
    }
}
