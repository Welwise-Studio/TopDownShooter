using System;

namespace Domain.GamePlay.Units
{
    public interface IDamageable
    {
        public void TakeDamage(float damage);
        public event Action<float> OnDamaged;

    }
}
