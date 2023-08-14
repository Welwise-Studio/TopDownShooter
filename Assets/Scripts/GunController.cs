using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform _weaponHold;
    [SerializeField] private Gun[] _allGuns;
    private Gun _equippedGun;
    public float GunHeight
    {
        get => _weaponHold.position.y;
    }
    public void EquipGun(Gun gunToEquip)
    {
        if (_equippedGun != null)
        {
            Destroy(_equippedGun.gameObject);
        }

        _equippedGun = Instantiate(gunToEquip, _weaponHold.position, _weaponHold.rotation, _weaponHold);
    }
    public void EquipGun(int weaponIndex)
    {
        EquipGun(_allGuns[weaponIndex]);
    }
    public void OnTriggerHold()
    {
        if (_equippedGun != null)
        {
            _equippedGun.OnTriggerHold();
        }
    }
    public void OnTriggerRelease()
    {
        if (_equippedGun != null)
        {
            _equippedGun.OnTriggerRelease();
        }
    }
    public void Aim (Vector3 aimPoint)
    {
       if (_equippedGun != null)
       {
        _equippedGun.Aim(aimPoint);
       }
    }
    public void Reload()
    {
        if (_equippedGun != null)
        {
            _equippedGun.Reload();
        }
    }
}
