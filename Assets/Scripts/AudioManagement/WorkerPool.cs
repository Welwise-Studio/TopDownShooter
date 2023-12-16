using System.Collections.Generic;
using UnityEngine;

namespace AudioManagement
{
    public class WorkerPool
    {
        private const int _capacityGrow = 4;

        public int Lenght => _workers.Count;
        private List<AudioSourceWorker> _workers = new List<AudioSourceWorker>();
        private int _capacity;
        private AudioSource _prefab;
        private Transform _container;

        public WorkerPool(Transform container, AudioSource prefab, int capacity = _capacityGrow)
        {
            _capacity = capacity;
            _container = container;
            _prefab = prefab;

            Allocate();
        }

        public AudioSourceWorker Get() => _workers[0];

        public AudioSourceWorker GetNotLooped()
        {
            if (Lenght == _capacity)
                Extend();

            foreach (var worker in _workers)
            {
                if (!worker.IsPlayLoop)
                    return worker;
            }
            var last = Lenght;
            Extend();
            return _workers[last];
        }

        public void ClearAll()
        {
            foreach (var worker in _workers)
            {
                worker.StopLoop();
            }
        }

        private void Allocate()
        {
            for (int i = _capacity - 1; i >= 0; i--)
            {
                CreateWorker();
            }
        }

        private void Extend()
        {
            _capacity += _capacityGrow;
            for (int i = _capacityGrow - 1; i >= 0; i--)
            {
                CreateWorker();
            }
        }

        private void CreateWorker() =>  _workers.Add(new AudioSourceWorker(Object.Instantiate<AudioSource>(_prefab, _container)));
    }
}
