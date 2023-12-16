using Architecture.DI;
using Architecture.Installers;
using UnityEngine;
using YG;

namespace Domain.Installers
{
    public class YandexGameInstaller : MonoInstaller
    {
        [SerializeField]
        private YandexGame _yandexGame;

        public override void InstallBindings(IDIContainer container)
        {
            container.Bind(_yandexGame);
            DontDestroyOnLoad(_yandexGame);
        }
    }
}
