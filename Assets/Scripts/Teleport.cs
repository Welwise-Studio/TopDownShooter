using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Utils.ConditionalField;


public class Teleport : MonoBehaviour
{
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
                SceneManager.LoadScene(_sceneToLoad.SceneName);
        }
    }
}