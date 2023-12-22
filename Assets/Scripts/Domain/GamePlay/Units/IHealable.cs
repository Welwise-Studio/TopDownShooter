using System;

namespace Domain.GamePlay.Units
{
    public interface IHealable
    {
        public void Heal(float health);
        public event Action<float> OnHealed;
    }
}
