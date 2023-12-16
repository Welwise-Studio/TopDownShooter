using Architecture.DI;
using Architecture.Installers;
using UnityEngine;

namespace Architecture.Contexts
{
    public abstract class Context : MonoBehaviour
    {
        [SerializeField] private MonoInstaller[] _monoInstallers;
        [SerializeField] private ScriptableInstaller[] _scriptableInstallers;

        public IDIContainer Container => _container ??= CreateLocalContainer();

        private IDIContainer _container;
        protected abstract IDIContainer CreateLocalContainer();

        protected void Initialize()
        {
            foreach (var monoInstaller in _monoInstallers)
            {
                monoInstaller.InstallBindings(Container);
            }

            foreach (var scriptableInstaller in _scriptableInstallers)
            {
                scriptableInstaller.InstallBindings(Container);
            }
        }

        private void OnDestroy()
        {
            Container.Dispose();
        }
    }
}
