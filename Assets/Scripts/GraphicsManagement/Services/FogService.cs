using UnityEngine;

namespace GraphicsManagement.Services
{
    public class FogService : MonoGraphicsService
    {
        [SerializeField]
        private float _linerStart = 0;
        [SerializeField]
        private float _linerEnd = 53;

        [SerializeField]
        private float _expsqrDensity = 0.029f;

        public override void Apply(GraphicsType type)
        {
            switch (type)
            {
                case GraphicsType.Mobile:
                    RenderSettings.fogMode = FogMode.Linear;
                    RenderSettings.fogEndDistance = _linerEnd;
                    RenderSettings.fogStartDistance = _linerStart;
                    break;
                case GraphicsType.Desktop:
                    RenderSettings.fogMode = FogMode.ExponentialSquared;
                    RenderSettings.fogDensity = _expsqrDensity;
                    break;
                default:
                    break;
            }
        }
    }
}
