using Domain.GamePlay.Hitting;
using System;
using UnityEngine;

namespace Domain.GamePlay.Units
{
    [RequireComponent(typeof(Health))]
    public abstract class Unit : MonoBehaviour, IDamageable, IHealable, IHitable
    {
        public event Action<float> OnHealed;
        public event Action<float> OnDamaged;
        public event Action OnDied;
        public event Action<Vector3, Vector3> OnHit;

        public Health Health {  get; private set; }

        protected Transform _cachedTransform;

        protected virtual void Awake()
        {
            Health = GetComponent<Health>();
            Health.OnHealthZero += Die;
            Health.OnHealthZero += OnDied;
            _cachedTransform = transform;
        }

        public virtual void Heal(float health)
        {
            this.Health.Add(health);
            OnHealed?.Invoke(health);
        }

        public virtual void TakeDamage(float damage)
        {
            this.Health.Remove(damage);
            OnDamaged?.Invoke(damage);
        }

        protected abstract void Die();

        public abstract void Hit(Vector3 point, Vector3 direction);
    }
}
