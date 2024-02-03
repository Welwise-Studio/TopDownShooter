using UnityEngine;

namespace ShelterSystem
{
    public class Fence : LivingEntity
    {
        [SerializeField]
        private GameObject _model;

        [SerializeField]
        private ParticleSystem[] _poofParticles;

        public void Restore()
        {
            _model.SetActive(true);
            AddHealth(startingHealth);
            foreach (var item in _poofParticles)
            {
                item.Play();
            }
        }

        public override void Die()
        {
            Debug.Log("DEAD fence");
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
