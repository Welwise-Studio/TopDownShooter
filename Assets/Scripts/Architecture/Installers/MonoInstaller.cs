using Architecture.DI;
using UnityEngine;

namespace Architecture.Installers
{
    public abstract class MonoInstaller : MonoBehaviour, IInstaller
    {
        public abstract void InstallBindings(IDIContainer container);
    }
}
