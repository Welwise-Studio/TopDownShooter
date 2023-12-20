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
        private AudioClip _music;

        private void Start()
        {
            ProjectContext.Instance.Container.Resolve<AudioCore>().SetLoop(_music, PlayableChannels.Music);
        }
    }
}
