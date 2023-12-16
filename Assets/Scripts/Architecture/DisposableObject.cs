using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Architecture
{
    public abstract class DisposableObject : IDisposable
    {
        private bool _isDisposed;
        private List<IDisposable> _mainThreadDisposables;
        private List<Object> _unityObjects;

        public void Dispose()
        {
            if (_isDisposed)
                return;
            _isDisposed = true;
            if (_mainThreadDisposables != null)
            {
                var mainThreadDisposables = _mainThreadDisposables;
                for (var i = mainThreadDisposables.Count - 1; i >= 0; i--)
                    mainThreadDisposables[i]?.Dispose();
                mainThreadDisposables.Clear();
            }
            try
            {
                OnDispose();
            }
            catch (Exception e)
            {
                Debug.Log($"This exception can be ignored. Disposable of {GetType().Name}: {e}");
            }

            if (_unityObjects == null) return;
            foreach (var obj in _unityObjects.Where(obj => obj))
            {
                Object.Destroy(obj);
            }
        }

        protected virtual void OnDispose() { }

        protected TDisposable AddToDisposables<TDisposable>(TDisposable disposable) where TDisposable : IDisposable
        {
            if (_isDisposed)
            {
                Debug.Log("disposed");
                return default;
            }
            if (disposable == null)
            {
                return default;
            }

            _mainThreadDisposables ??= new List<IDisposable>(1);
            _mainThreadDisposables.Add(disposable);
            return disposable;
        }
    }
}