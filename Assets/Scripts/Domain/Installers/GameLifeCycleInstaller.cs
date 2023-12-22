using Architecture.DI;
using Architecture.Installers;
using UnityEngine;

namespace Domain.Installers
{
    public sealed class GameLifeCycleInstaller : MonoInstaller
    {
        [SerializeField]
        private GameLifeCycle _lifeCycle;

        public override void InstallBindings(IDIContainer container)
        {
            container.Bind<GameLifeCycle>(_lifeCycle);
        }
    }
}
