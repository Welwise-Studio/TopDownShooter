using Architecture.Contexts;
using AudioManagement;
using UnityEngine;

namespace Domain
{
    public class BackgroundMusic : MonoBehaviour
    {

        [SerializeField]
        private AudioClip _music;

        private void Start()
        {
            ProjectContext.Instance.Container.Resolve<AudioCore>().SetLoop(_music, PlayableChannels.Music);
        }
    }
}
