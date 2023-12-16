using System;

namespace Architecture.Reactive
{
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        public T Value 
        { 
            get => _value; 
            set
            {
                _value = _validator == null ? value : _validator(value);
                OnChanged?.Invoke(_value);
            }
        }

        public event Action<T> OnChanged;

        private readonly Func<T, T> _validator;
        private T _value;

        public ReactiveProperty(T initialValue, Func<T, T> validator = null)
        {
            _validator = validator;
            _value = initialValue;
        }

        public ReactiveProperty(Func<T, T> validator = null)
        {
            _validator = validator;
        }

        public void Dispose()
        {
            OnChanged = null;
        }

        public void Subscribe(Action<T> action) => OnChanged += action;
        public void Unsubscribe(Action<T> action) => OnChanged -= action;
    }
}
