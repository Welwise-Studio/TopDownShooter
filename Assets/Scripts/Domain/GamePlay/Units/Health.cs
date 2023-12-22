using System;
using UnityEngine;
#if UNITY_EDITOR
using Utils;
#endif


namespace Domain.GamePlay.Units
{
    public class Health : MonoBehaviour
    {
        public event Action<float> OnChanged;
        public event Action<float> OnAdd;
        public event Action<float> OnRemove;
        public event Action<float> OnMaxHealthChanged;
        public event Action OnHealthZero;
        public event Action OnHealthMax;

        public float MaxHealth { get => _maxHealth; }
        public float Value { get => _value; }

        [SerializeField]
        private float _maxHealth;
        [SerializeField]
        private float _value;
        [SerializeField]
        private bool _printTrace;

        private string _holderName;

        private void Start()
        {
            SetHealth(_value);
            ChangeMaxHealth(_maxHealth);
            if (_printTrace)
                _holderName = gameObject.name;
        }

        private void OnDestroy()
        {
            OnChanged = null;
            OnAdd = null;
            OnRemove = null;
            OnMaxHealthChanged = null;
            OnHealthZero = null;
            OnHealthMax = null;
        }

        public void Add(float amount)
        {
            _value = Mathf.Clamp(Value+amount, 0, MaxHealth);

            PrintAction("add", amount);

            OnAdd?.Invoke(amount);
            OnChanged?.Invoke(Value);

            if (Value == MaxHealth)
                OnHealthMax?.Invoke();
        }

        public void Remove(float amount)
        {
            _value = Mathf.Clamp(Value-amount, 0, MaxHealth);

            PrintAction("remove", amount);

            OnRemove?.Invoke(amount);
            OnChanged?.Invoke(Value);

            if (Value == 0)
                OnHealthZero?.Invoke();
        }

        public void ChangeMaxHealth(float value)
        {
            _maxHealth = value;

            PrintAction("change max health", value);

            OnMaxHealthChanged?.Invoke(value);
        }

        public void SetHealth(float value)
        {
            _value = Mathf.Clamp(Value + value, 0, MaxHealth);

            PrintAction("set health", value);

            if (Value == 0)
                OnHealthZero?.Invoke();
            else if (Value == MaxHealth)
                OnHealthMax?.Invoke();
            else
                OnChanged?.Invoke(value);
        }

        public float GetClampedValue() => Mathf.Clamp01(Value / MaxHealth);
        private void PrintAction(string action, float value)
        {
            if (!_printTrace)
                return;

            Debug.Log($"[{_holderName}] <{action.ToUpper()}> : <{value}>");
        }
#if UNITY_EDITOR
        [Button(row: "row-1")]
        public void Add25() => Add(MaxHealth * 0.25f);

        [Button(row: "row-1")]
        public void Add50() => Add(MaxHealth * 0.5f);

        [Button(row: "row-1")]
        public void Add75() => Add(MaxHealth * 0.75f);

        [Button(row: "row-1")]
        public void AddFull() => Add(MaxHealth);

        [Button(row: "row-2")]
        public void Remove25() => Remove(MaxHealth * 0.25f);

        [Button(row: "row-2")]
        public void Remove50() => Remove(MaxHealth * 0.5f);

        [Button(row: "row-2")]
        public void Remove75() => Remove(MaxHealth * 0.75f);

        [Button(row: "row-2")]
        public void RemoveFull() => Remove(MaxHealth);
#endif
    }
}
