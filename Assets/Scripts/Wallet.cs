using FlexibleSaveSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public Action<int> OnValuseChanged;
    public int Value { get => _value; }
    [SaveData] private int _value = 0;

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
