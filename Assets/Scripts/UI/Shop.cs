using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Shop : MonoBehaviour
{
    [SerializeField] private GunController _gunController;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private ShopItem[] _items;
    private ShopItem _lastItem;

    private void Awake()
    {
        foreach (var item in _items)
        {
            item.OnClicked += Interaction;
        }
        _gunController.OnGunChanged += LightCurrentItem;
        YandexGame.GetDataEvent += Load;

        if (YandexGame.SDKEnabled)
            Load();
    }

    private void Load()
    {
        foreach (var item in _items)
        {         
            if (YandexGame.savesData.openedWeapons.ContainsKey(item.Id) && YandexGame.savesData.openedWeapons[item.Id] == true)
                item.Unlock();
            else
                item.Lock();
        }
    }

    private void LightCurrentItem(Gun gun)
    {
        foreach(var item in _items)
        {
            if (gun.Icon == item.Gun.Icon)
            {
                _lastItem?.Unlight();
                item.Light();
                _lastItem = item;
            }
        }
    }

    private void Interaction(ShopItem item)
    {
        if (!item.IsLocked)
        {
            _gunController.EquipGun(item.Gun);
        }
        else if (_wallet.TrySpend(item.Price))
        {
            item.Unlock();
            _gunController.EquipGun(item.Gun);;
        }
        else
        {
            item.Shake();
        }
    }
}
