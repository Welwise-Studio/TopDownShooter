using UnityEngine;
using YG;

public class DeviceDefinition : MonoBehaviour
{
    [SerializeField] private GameObject joystick;
    [SerializeField] private ShopTogler[] _togglers;
    public void DefineDevice()
    {
        if (YandexGame.EnvironmentData.isMobile)
        {
            joystick.SetActive(true);
            foreach (var togler in _togglers)
            {
                togler.IsMobile = true;
            }
        }
    } 

    private void Start()
    {
        joystick.SetActive(false);
        foreach (var togler in _togglers)
        {
            togler.IsMobile = false;
        }
        DefineDevice();
    }
}
