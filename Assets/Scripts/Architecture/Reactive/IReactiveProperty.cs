using System;

namespace Architecture.Reactive
{
    public interface IReactiveProperty<T> : IDisposable
    {
        T Value { get; set; }
        public event Action<T> OnChanged;
        void Subscribe(Action<T> action);
    }
}
