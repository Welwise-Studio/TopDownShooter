using Architecture;
using Architecture.Contexts;
using AudioManagement;
using System.Collections;
using UnityEngine;
using Utils;

namespace Domain.GamePlay.Hitting
{
    public class HitRenderer : MonoBehaviour
    {
        [SerializeField]
        [OnlyPrefab]
        private ParticleSystem _hitParticlePrefab;

        [SerializeField]
        private AudioClip _hitSoundClip;

        private const int PRELOAD_COUNT = 10;
        private ObjectPool<ParticleSystem> _pool;
        private AudioCore _audioCore;

        private void Awake()
        {
            _audioCore = ProjectContext.Instance.Container.Resolve<AudioCore>();
            _pool = new ObjectPool<ParticleSystem>(PreloadParticle, GetParticleAction, RetrunParticleAction, DisposeParticleAction, PRELOAD_COUNT);
        }

        public void PlayHit(Vector3 point, Vector3 direction, Vector3 view)
        {
            _audioCore.PlayOneShoot(_hitSoundClip, PlayableChannels.SFX);
            StartCoroutine(HitingRoutine(_pool.Get(),point, direction, view));
        }

        private IEnumerator HitingRoutine(ParticleSystem particle, Vector3 point, Vector3 direction, Vector3 view)
        {
            particle.Play();
            particle.transform.rotation = Quaternion.FromToRotation(view, direction);
            yield return new WaitForSeconds(particle.main.startLifetime.constant);
            particle.gameObject.SetActive(false);
        }

        private ParticleSystem PreloadParticle() => Instantiate(_hitParticlePrefab);
        private void GetParticleAction(ParticleSystem particle) => particle.gameObject.SetActive(true);
        private void RetrunParticleAction(ParticleSystem particle) => particle.gameObject.SetActive(true);
        private void DisposeParticleAction(ParticleSystem particle) => Destroy(particle);

        private void OnDestroy()
        {
            _pool.Dispose();
        }

    }
}
