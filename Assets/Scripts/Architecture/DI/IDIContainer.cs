using System;

namespace Architecture.DI
{
    public interface IDIContainer : IDisposable
    {
        public bool IsRoot { get; }
        public void Bind<T>(T instance) where T : class;
        public T Resolve<T>() where T : class;
    }
}
