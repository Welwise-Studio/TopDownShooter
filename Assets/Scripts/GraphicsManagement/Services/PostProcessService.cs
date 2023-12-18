using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace GraphicsManagement
{
    public class PostProcessService : MonoGraphicsService
    {
        [SerializeField]
        private PostProcessVolume _postProcessVolume;

        [SerializeField]
        private PostProcessProfile _mobileProfile;

        [SerializeField]
        private PostProcessProfile _desktopProfile;

        public override void Apply(GraphicsType type)
        {
            switch (type)
            {
                case GraphicsType.Mobile:
                    _postProcessVolume.profile = _mobileProfile;
                    break;

                case GraphicsType.Desktop:
                    _postProcessVolume.profile = _desktopProfile;
                    break;

                default:
                    _postProcessVolume.profile = _desktopProfile;
                    break;
            }
        }
    }
}
