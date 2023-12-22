using System;

namespace Domain.GamePlay.Enemies
{
    [Serializable]
    public struct EnemyStats
    {
        public float AttackCooldown;
        public float AttackSpeed;
        public float Damage;
        public float MoveSpeed;
    }
}
