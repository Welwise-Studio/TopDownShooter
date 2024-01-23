using UnityEngine;
using YG;

namespace Assets.Scripts
{
    public class BootYGFix : MonoBehaviour
    {
        private void Fix() => YGPluginFix.DeviceType = YandexGame.EnvironmentData.deviceType;

        private void Start()
        {
            if (YandexGame.SDKEnabled)
                Fix();
        }
        private void OnEnable() => YandexGame.GetDataEvent += Fix;

        private void OnDisable() => YandexGame.GetDataEvent -= Fix;
    }
}
