using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Utils.ConditionalField;


public class Teleport : MonoBehaviour
{
    private static readonly string _catSceneName = "CatScene";
    [SerializeField]
    private bool _useSceneLoad;

    [SerializeField]
    private bool _usePosition;

    [SerializeField]
    [ConditionalField("_useSceneLoad")]
    private SceneField _sceneToLoad;

    [SerializeField]
    [ConditionalField("_usePosition")]
    private Transform _tpPosition;



    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            if (_usePosition)
                player.transform.position = _tpPosition.position;

            if (_useSceneLoad)
            {
                CatSceneLoader.NextSceneName = _sceneToLoad.SceneName;
                SceneManager.LoadScene(_catSceneName);
            }
        }
    }
}