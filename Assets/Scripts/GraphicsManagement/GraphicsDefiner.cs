using Assets.Scripts;
using UnityEngine;
using YG;

namespace GraphicsManagement
{
    public class GraphicsDefiner : MonoBehaviour
    {
        [SerializeField] private MonoGraphicsService[] _graphicsServices;

        private void Awake()
        {
            YandexGame.GetDataEvent += () => Define(YandexGame.EnvironmentData.deviceType);
        }

        private void Start()
        {
            if (YandexGame.SDKEnabled)
                Define(YandexGame.EnvironmentData.deviceType);

            if (YGPluginFix.DeviceType != null)
                Define(YGPluginFix.DeviceType);
        }

        private void Define(string type)
        {
            var ttype = new GraphicsTypeConverter().FromDevice(type);

            foreach (var service in _graphicsServices)
            {
                service.Apply(ttype);
            }
        }
    }
}
