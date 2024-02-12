using System;
using UnityEngine;
using YG;

public class GunController : MonoBehaviour
{
    public Action<Gun> OnGunChanged; 
    [SerializeField] private Transform _weaponHold;
    [field: SerializeField] public Gun[] _allGuns { get; private set; }
    public Gun _equippedGun { get; private set; }
    public float GunHeight { get => _weaponHold.position.y; }

    public void EquipGun(Gun gunToEquip)
    {
        if (_equippedGun != null)
        {
            Destroy(_equippedGun.gameObject);
        }

        _equippedGun = Instantiate(gunToEquip, _weaponHold.position,gunToEquip.transform.rotation,_weaponHold);
        _equippedGun.name = gunToEquip.name;
        CombinedSDK.AllSavesCombinedSDK.LastGunIndex = 0;
        for (int i = _allGuns.Length - 1; i >= 0; i--)
        {
            if (_allGuns[i].name == gunToEquip.name)
            {
                CombinedSDK.AllSavesCombinedSDK.LastGunIndex = i;
                break;
            }
        }
        CombinedSDK.SaveProgressData();

        OnGunChanged?.Invoke(gunToEquip);
    }
    public void EquipGun(int weaponIndex)
    {
        if (weaponIndex < _allGuns.Length)
        {
            EquipGun(_allGuns[weaponIndex]);
            CombinedSDK.AllSavesCombinedSDK.LastGunIndex = weaponIndex;
        }
        else
        {
            EquipGun(_allGuns[^1]);
            CombinedSDK.AllSavesCombinedSDK.LastGunIndex = 0;
        }
        CombinedSDK.SaveProgressData();
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
