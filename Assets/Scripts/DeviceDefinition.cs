using Assets.Scripts;
using UnityEngine;
using YG;

public class DeviceDefinition : MonoBehaviour
{
    [SerializeField] private GameObject _mobileUI;
    [SerializeField] private ShopTogler[] _togglers;
    [SerializeField] private AimAssistent _aimAssistent;
    [SerializeField] private Player _player;
    public void DefineDevice(string type)
    {
        if (type == "mobile")
        {
            _player.MobileControll = true;
            _mobileUI.SetActive(true);
            _aimAssistent.enabled = true;
            foreach (var togler in _togglers)
            {
                togler.IsMobile = true;
            }
        }
    } 

    private void Start()
    {
        _player.MobileControll = false;
        _mobileUI.SetActive(false);
        _aimAssistent.enabled = false;
        _player.MobileControll = false;
        foreach (var togler in _togglers)
        {
            togler.IsMobile = false;
        }

        if (YandexGame.SDKEnabled)
            DefineDevice(YandexGame.EnvironmentData.deviceType);

        if (YGPluginFix.DeviceType != null)
            DefineDevice(YGPluginFix.DeviceType);
    }

    private void OnEnable() => YandexGame.GetDataEvent += () => DefineDevice(YandexGame.EnvironmentData.deviceType);

    private void OnDisable() => YandexGame.GetDataEvent -= () => DefineDevice(YandexGame.EnvironmentData.deviceType);
}
