using System;
using UnityEngine;
using YG;

public class Wallet : MonoBehaviour
{
    public Action<int> OnValuseChanged;
    public int Value { get => _value; }
    private int _value = 0;

    private void Load()
    {
        _value = YandexGame.savesData.Balance;
        OnValuseChanged?.Invoke(Value);
    }

    private void Start()
    {
        YandexGame.GetDataEvent += Load;
        if (YandexGame.SDKEnabled)
            Load();
    }

    private void OnDestroy()
    {
        YandexGame.savesData.Balance = _value;
        YandexGame.SaveProgress();
    }

    public void Add(int amount)
    {
        _value += amount;
        OnValuseChanged?.Invoke(Value);
    }

    public bool Enough(int amount) => Value >= amount;

    public bool TrySpend(int amount)
    {
        if (Enough(amount))
        {
            _value -= amount;
            OnValuseChanged?.Invoke(Value);
            return true;
        }
        else
            return false;
    }
}
