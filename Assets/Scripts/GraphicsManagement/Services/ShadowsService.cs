using UnityEngine;

namespace GraphicsManagement.Services
{
    public class ShadowsService : MonoGraphicsService
    {
        [SerializeField]
        private Light _light;

        public override void Apply(GraphicsType type)
        {
            switch (type)
            {
                case GraphicsType.Mobile:
                    _light.shadows = LightShadows.None;
                    break;
                case GraphicsType.Desktop:
                    _light.shadows = LightShadows.Soft;
                    break;
                default:
                    break;
            }
        }
    }
}
