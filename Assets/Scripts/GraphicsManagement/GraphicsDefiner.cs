using Assets.Scripts;
using GamePush;
using UnityEngine;
using YG;

namespace GraphicsManagement
{
    public class GraphicsDefiner : MonoBehaviour
    {
        [SerializeField] private MonoGraphicsService[] _graphicsServices;

        private void Start()
        {
            if (GP_Device.IsMobile())
            {
                Define("mobile");
            }
            else
                Define("desktop");
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
