using UnityEngine;
using UnityEngine.AI;

namespace ShelterSystem
{
    public class Fence : LivingEntity
    {
        [SerializeField]
        private GameObject _model;

        [SerializeField]
        private ParticleSystem[] _poofParticles;

        [SerializeField]
        private Collider[] _toDisableOnDie;

        [SerializeField]
        private NavMeshObstacle _obstacle;

        public void Restore()
        {
            foreach (var item in _toDisableOnDie)
                item.enabled = true;

            _obstacle.enabled = true;
            _model.SetActive(true);
            AddHealth(startingHealth);
            foreach (var item in _poofParticles)
            {
                item.Play();
            }
        }

        public override void Die()
        {
            foreach (var item in _toDisableOnDie)
                item.enabled = false;

            _obstacle.enabled = false;
            dead = true;

            foreach (var item in _poofParticles)
            {
                item.Play();
            }

            _model.SetActive(false);
            CallOnDeath();
        }
    }
}
