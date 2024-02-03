using System;
using UnityEngine;

namespace HealthSystem
{
    [RequireComponent(typeof(LivingEntity))]
    public class HPRegeneration : MonoBehaviour
    {
        public float RegenerationPerSecond;

        [SerializeField]
        [Range(.001f, 1)]
        private float _interval;

        private float _timer;

        private LivingEntity _entity;

        private void Awake()
        {
            _entity = GetComponent<LivingEntity>();
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _interval)
            {
                _entity.AddHealth(RegenerationPerSecond / _interval);
                _interval = 0;
            }
        }
    }
}
