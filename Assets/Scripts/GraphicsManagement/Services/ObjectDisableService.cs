using UnityEngine;

namespace GraphicsManagement
{
    public class ObjectDisableService : MonoGraphicsService
    {
        [SerializeField]
        private GameObject[] _desktopGroup;

        public override void Apply(GraphicsType type)
        {
            switch (type)
            {
                case GraphicsType.Desktop:
                    SetActiveDesktopGroup(true);
                    break;

                case GraphicsType.Mobile:
                    SetActiveDesktopGroup(false);
                    break;

                default: 
                    SetActiveDesktopGroup(true);
                    break;
            }
        }

        private void SetActiveDesktopGroup(bool state)
        {
            foreach (var item in _desktopGroup)
            {
                Debug.Log($"disable: {item.name}");
                item.SetActive(state);
            }
        }
    }
}
