using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public Action<int> OnValuseChanged;
    public int Value {  get; private set; }

    public void Add(int amount)
    {
        Value += amount;
        OnValuseChanged?.Invoke(Value);
    }

    public bool Enough(int amount) => Value >= amount;

    public bool TrySpend(int amount)
    {
        if (Enough(amount))
        {
            Value -= amount;
            OnValuseChanged?.Invoke(Value);
            return true;
        }
        else
            return false;
    }
}
