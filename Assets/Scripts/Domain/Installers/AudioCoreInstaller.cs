using Architecture.DI;
using Architecture.Installers;
using AudioManagement;
using UnityEngine;

namespace Domain.Installers
{
    public class AudioCoreInstaller : MonoInstaller
    {
        [SerializeField] private AudioCore _audioCore;

        public override void InstallBindings(IDIContainer container)
        {
            container.Bind<AudioCore>(_audioCore);
            DontDestroyOnLoad(_audioCore);
        }
    }
}
