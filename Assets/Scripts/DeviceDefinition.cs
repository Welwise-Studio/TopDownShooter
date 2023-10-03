using UnityEngine;
public class DeviceDefinition : MonoBehaviour
{
    [SerializeField]private GameObject joystick;

    private void Start()
    {
        if(Application.isMobilePlatform)
        {
            joystick.SetActive(true);
        }
        else
        {
            joystick.SetActive(false);  
        }
    }
}
