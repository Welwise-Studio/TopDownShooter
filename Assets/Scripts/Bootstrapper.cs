using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _objectsToDestroyOnload;

    [SerializeField]
    private string _firstScene;

    private void OnEnable()
    {
        CombinedSDK.OnCombinedSDKInitilizedEvent += Boot;
    }

    private void OnDisable()
    {
        CombinedSDK.OnCombinedSDKInitilizedEvent -= Boot;
    }

    private void Boot()
    {
        foreach (var item in _objectsToDestroyOnload)
        {
            DontDestroyOnLoad(item);
        }
        if (!_firstScene.IsNullOrEmpty())
            SceneManager.LoadScene(_firstScene);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        Debug.Log(CombinedSDK.IsInitilized);
    }
}
