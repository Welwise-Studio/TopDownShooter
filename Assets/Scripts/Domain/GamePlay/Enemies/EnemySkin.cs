using Domain.GamePlay.Hitting;
using UnityEngine;

namespace Domain.GamePlay.Enemies
{
    public class EnemySkin : MonoBehaviour
    {
        [field: SerializeField]
        public EnemyAnimationData AnimationData { get; private set; }
        [field: SerializeField]
        public ParticleSystem RiseUpParticle { get; private set; }

        [field: SerializeField]
        public Color BloodColor { get; private set; }

        [field: SerializeField]
        public ParticleSystem DeathParticle { get; private set; }

        [field: SerializeField]
        public HitRenderer HitRenderer { get; private set; }
    }
}
