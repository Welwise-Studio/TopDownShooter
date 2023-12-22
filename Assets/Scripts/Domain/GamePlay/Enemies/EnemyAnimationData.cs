using System;
using UnityEngine;

namespace Domain.GamePlay.Enemies
{
    [Serializable]
    public struct EnemyAnimationData
    {
        public Animator Animator;
        public string AttackAnimationName;
        public string TakeDamageAnimationName;
        public string RiseUpAnimationName;
        public string MoveAnimationName;
    }
}
