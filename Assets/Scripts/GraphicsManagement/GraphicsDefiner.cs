using UnityEngine;
using YG;

namespace GraphicsManagement
{
    public class GraphicsDefiner : MonoBehaviour
    {
        [SerializeField] private MonoGraphicsService[] _graphicsServices;

        private void Start()
        {
            var type = new GraphicsTypeConverter().FromDevice(YandexGame.EnvironmentData.deviceType);

            foreach (var service in _graphicsServices)
            {
                service.Apply(type);
            }
        }
    }
}
