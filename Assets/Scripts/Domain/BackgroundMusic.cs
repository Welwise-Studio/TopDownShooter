using Architecture.Contexts;
using AudioManagement;
using UnityEngine;

#if UNITY_EDITOR
using System.IO;
#endif

namespace Domain
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class BackgroundMusic : MonoBehaviour
    {

        [SerializeField]
        private string _musicFile;

        private async void Start()
        {
            ProjectContext.Instance.Container.Resolve<AudioCore>().SetLoop(await _loader.Load(_musicFile), PlayableChannels.Music);
        }
    }
}
