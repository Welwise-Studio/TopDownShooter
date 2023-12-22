using Domain.GamePlay.Units;
using UnityEngine;

namespace Domain.GamePlay.Enemies
{
    public class Enemy : Unit
    {
        [field: SerializeField]
        private EnemySkin _skin;

        [field: SerializeField]
        public EnemyStats Stats { get; private set; }

        public override void Hit(Vector3 point, Vector3 direction)
        {
            _skin.HitRenderer.PlayHit(point, direction, _cachedTransform.forward);
        }

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Die()
        {
            _skin.DeathParticle.Play();
        }

        public void PlayRiseUpFX() => _skin.RiseUpParticle.Play();
    }
}
