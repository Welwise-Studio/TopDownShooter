using Architecture.Contexts;
using AudioManagement;
using UnityEngine;

namespace Domain
{
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField]
        private AudioClip _backgroundMusic;

        private AudioSourceWorker _worker;

        private void Start()
        {
            _worker = ProjectContext.Instance.Container.Resolve<AudioCore>().SetLoop(_backgroundMusic, PlayableChannels.Music);
        }
    }
}
