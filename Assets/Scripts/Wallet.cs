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
        _value = CombinedSDK.AllSavesCombinedSDK.Balance;
        OnValuseChanged?.Invoke(Value);
    }

    private void OnEnable()
    {
        CombinedSDK.OnCombinedSDKInitilizedEvent += Load;
    }

    private void OnDisable()
    {
        CombinedSDK.OnCombinedSDKInitilizedEvent -= Load;
    }

    private void Start()
    {
        if (CombinedSDK.IsInitilized)
            Load();
    }

    private void OnDestroy()
    {
        CombinedSDK.AllSavesCombinedSDK.Balance = _value;
        CombinedSDK.SaveProgressData();
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
