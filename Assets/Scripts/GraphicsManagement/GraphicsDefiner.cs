using UnityEngine;
using YG;

namespace GraphicsManagement
{
    public class GraphicsDefiner : MonoBehaviour
    {
        [SerializeField] private MonoGraphicsService[] _graphicsServices;

        private void Awake()
        {
            YandexGame.GetDataEvent += Define;
        }

        private void Start()
        {
            if (YandexGame.SDKEnabled)
                Define();
        }

        private void Define()
        {
            var type = new GraphicsTypeConverter().FromDevice(YandexGame.EnvironmentData.deviceType);
            Debug.Log(type.ToString());

            foreach (var service in _graphicsServices)
            {
                service.Apply(type);
            }
        }
    }
}
