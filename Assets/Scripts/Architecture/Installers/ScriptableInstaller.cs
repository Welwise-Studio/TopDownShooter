using Architecture.DI;
using UnityEngine;

namespace Architecture.Installers
{
    public abstract class ScriptableInstaller : ScriptableObject, IInstaller
    {
        public abstract void InstallBindings(IDIContainer container);
    }
}
