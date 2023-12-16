using Architecture.Contexts;
using AudioManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using YG;

namespace Domain
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private ProjectContext _projectContext;
        [SerializeField] private SceneField _startScene;
        private void Awake()
        {
            //Wait YG Init
            YandexGame.GetDataEvent += Load;
        }

        private void Load()
        {
            Debug.Log("YandexGame.GetDataEvent");
            _projectContext.Init();
            _projectContext.Container
                .Resolve<AudioCore>()
                .BindSettings(_projectContext.Container
                .Resolve<SettingsSystem.Settings>());
            SceneManager.LoadScene(_startScene.SceneName) ;
        }
    }
}
