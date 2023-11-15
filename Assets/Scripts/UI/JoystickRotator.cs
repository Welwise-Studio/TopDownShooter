using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Joystick))]
public class JoystickRotator : MonoBehaviour
{
    [SerializeField] private Transform _handle;
    private Joystick _joystick;

    private void Awake()
    {
        _joystick = GetComponent<Joystick>();
    }

    private void Update()
    {
        _handle.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(-_joystick.Horizontal, _joystick.Vertical) * Mathf.Rad2Deg);
    }
}
