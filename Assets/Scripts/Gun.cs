using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum FireMode { Auto, Burst, Single };
    [field: SerializeField] public FireMode fireMode { get; private set; } = FireMode.Auto;
    [SerializeField] private Transform[] _projectileSpawn;
    [SerializeField] private Projectile _projectile;
    [SerializeField] private float _firerate = 100f;
    [SerializeField] [Tooltip("Bullet speed")] private float _muzzleVelocity = 35f;
    [SerializeField] private float _bulletDamage = 1f;
    [SerializeField] private int _burstCount;
    [SerializeField] private int _projectilesPerMag;
    [SerializeField] private float _reloadTime = 0.3f;

    [Header("Recoil")]
    [SerializeField] private Vector2 _kickMinMax = new Vector2(0.05f, 0.2f);
    [SerializeField] private Vector2 _recoilAngleMinMax = new Vector2(3f, 5f);
    [SerializeField] private float _recoilMoveSettleTime = 0.1f;
    [SerializeField] private float _recoilRotationSettleTime = 0.1f;

    [Header("Effects")]
    [SerializeField] private Transform _shell;
    [SerializeField] private Transform _shellEjection;
    [SerializeField] private AudioClip _shootAudio;
    [SerializeField] private AudioClip _reloadAudio;
    private MuzzleFlash _muzzleFlash;
    private float _nextShotTime;

    private bool _triggerReleasedSinceLastShot;
    private int _shotRemainingInBurst;
    public int _projectilesRemainingInMag { get; private set; }
    public bool _isReloading { get; private set; }

    private Vector3 _recoilSmoothDampVelocity;
    private float _recoilRotSmoothDampVelocity;
    private float _recoilAngle;
    private void Start()
    {
        _muzzleFlash = GetComponent<MuzzleFlash>();
        _shotRemainingInBurst = _burstCount;
        _projectilesRemainingInMag = _projectilesPerMag;
    }
    private void LateUpdate()
    {
        RecoilAnimation();

        ReloadAnimation();
    }
    private void ReloadAnimation()
    {
        if (!_isReloading && _projectilesRemainingInMag == 0)
        {
            Reload();
        }
    }
    private void RecoilAnimation()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, ref _recoilSmoothDampVelocity, _recoilMoveSettleTime);
        _recoilAngle = Mathf.SmoothDamp(_recoilAngle, 0, ref _recoilRotSmoothDampVelocity, _recoilRotationSettleTime);
        transform.localEulerAngles = transform.localEulerAngles + Vector3.left * _recoilAngle;
    }
    private void Shoot()
    {
        if (!_isReloading && Time.time > _nextShotTime && _projectilesRemainingInMag > 0)
        {
            if (fireMode == FireMode.Burst)
            {
                if (_shotRemainingInBurst == 0)
                {
                    return;
                }

                _shotRemainingInBurst--;
            }
            else if (fireMode == FireMode.Single)
            {
                if (!_triggerReleasedSinceLastShot)
                {
                    return;
                }
            }

            for (int i = 0; i < _projectileSpawn.Length; i++)
            {
                if (_projectilesRemainingInMag == 0)
                {
                    break;
                }

                _projectilesRemainingInMag--;

                _nextShotTime = Time.time + _firerate / 1000;

                Projectile newProjectile = Instantiate(_projectile, _projectileSpawn[i].position, _projectileSpawn[i].rotation);
                newProjectile.SetSpeed(_muzzleVelocity);
                newProjectile.SetDamage(_bulletDamage);
            }

            Instantiate(_shell, _shellEjection.position, _shell.rotation);
            _muzzleFlash.Activate();
            transform.localPosition -= Vector3.forward * Random.Range(_kickMinMax.x, _kickMinMax.y);
            _recoilAngle += Random.Range(_recoilAngleMinMax.x, _recoilAngleMinMax.y);
            _recoilAngle = Mathf.Clamp(_recoilAngle, 0, 30f);

            AudioManager.Instance.PlaySound(_shootAudio, transform.position);
        }
    }
    public void Reload()
    {
        if (!_isReloading && _projectilesRemainingInMag != _projectilesPerMag)
        {
            StartCoroutine(AnimateReload());

            AudioManager.Instance.PlaySound(_reloadAudio, transform.position);
        }
    }
    private IEnumerator AnimateReload()
    {
        _isReloading = true;

        yield return new WaitForSeconds(0.2f);

        float reloadSpeed = 1 / _reloadTime;
        float percent = 0f;
        Vector3 initialRot = transform.localEulerAngles;
        float maxReloadAngle = 30f;

        while (percent < 1)
        {
            percent += Time.deltaTime * reloadSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            float reloadAngle = Mathf.Lerp(0, maxReloadAngle, interpolation);
            //transform.localEulerAngles = initialRot + Vector3.left * reloadAngle;

            yield return null;
        }

        _isReloading = false;
        _projectilesRemainingInMag = _projectilesPerMag;
    }
    public void Aim(Vector3 aimPoint)
    {
        if (!_isReloading)
        {
            transform.LookAt(aimPoint);
        }
    }
    public void OnTriggerHold()
    {
        Shoot();
        _triggerReleasedSinceLastShot = false;
    }
    public void OnTriggerRelease()
    {
        _triggerReleasedSinceLastShot = true;
        _shotRemainingInBurst = _burstCount;
    }
}
