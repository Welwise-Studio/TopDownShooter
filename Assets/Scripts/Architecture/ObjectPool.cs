using System;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture
{
    public class ObjectPool<T> : IDisposable
    {
        private readonly Action<T> _getAction;
        private readonly Action<T> _returnAction;
        private readonly Func<T> _preloadAction;
        private readonly Action<T> _disposeAction;

        private Queue<T> _pool = new Queue<T>();
        private List<T> _active = new List<T>();

        public ObjectPool(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, Action<T> disposeAction, int preloadCount)
        {
            _preloadAction = preloadFunc;
            _getAction = getAction;
            _returnAction = returnAction;
            _disposeAction = disposeAction;
            if (preloadFunc == null)
            {
                Debug.LogError("[ObjectPool] Preload function is null");
            }

            for (int i = 0; i < preloadCount; i++)
            {
                Return(preloadFunc());
            }
        }

        public T Get()
        {
            T item = _pool.Count >0 ? _pool.Dequeue() : _preloadAction();
            _getAction(item);
            _active.Add(item);

            return item;
        }

        public void Return(T item)
        {
            _returnAction(item);
            _pool.Enqueue(item);
            _active.Remove(item);
        }

        public void ReturnAll()
        {
            foreach (var item in _active)
                Return(item);
        }

        public void Dispose()
        {
            foreach (var item in _pool)
                _disposeAction(item);

            foreach (var item in _active)
                _disposeAction(item);
        }
    }
}
