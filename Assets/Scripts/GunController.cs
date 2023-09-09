using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform _weaponHold;
    [field: SerializeField] public Gun[] _allGuns { get; private set; }
    public Gun _equippedGun { get; private set; }
    public float GunHeight { get => _weaponHold.position.y; }

    private void EquipGun(Gun gunToEquip)
    {
        if (_equippedGun != null)
        {
            Destroy(_equippedGun.gameObject);
        }

        _equippedGun = Instantiate(gunToEquip, _weaponHold.position, _weaponHold.rotation, _weaponHold);
    }
    public void EquipGun(int weaponIndex)
    {
        if (weaponIndex < _allGuns.Length)
        {
            EquipGun(_allGuns[weaponIndex]);
        }
        else
        {
            EquipGun(_allGuns[^1]);
        }
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
    public void Aim(Vector3 aimPoint)
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
