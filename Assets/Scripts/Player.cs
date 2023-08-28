using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Crosshairs _crosshairs;
    [SerializeField] private float _playerFallBound = -10.0f;
    private PlayerController _playerControllerScript;
    private GunController _gunControllerScript;
    private Camera _cameraMain;
    private void Awake()
    {
        _playerControllerScript = GetComponent<PlayerController>();
        _gunControllerScript = GetComponent<GunController>();
        _cameraMain = Camera.main;

        FindObjectOfType<Spawner>().OnNewWave += OnNewWave;
    }
    protected override void Start()
    {
        base.Start();
    }
    private void Update()
    {
        MovementInput();

        LookInput();

        WeaponInput();

        CheckPlayerIsFall();

        CheckAmmoForReload();
    }
    public void OnNewWave(int waveNumber)
    {
        //health = startingHealth;
        //_gunControllerScript.EquipGun(waveNumber - 1);
        _gunControllerScript.EquipGun(0);
    }
    private void CheckAmmoForReload()
    {
        if (_gunControllerScript._equippedGun._projectilesRemainingInMag == 0)
        {
            _playerAnimator.SetBool("isReloadWeapon_b", true);
        }
        else
        {
            _playerAnimator.SetBool("isReloadWeapon_b", false);
        }
    }
    private void MovementInput()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * _moveSpeed;
        _playerControllerScript.Move(moveVelocity);

        if (moveVelocity != Vector3.zero)
        {
            _playerAnimator.SetBool("isRun_b", true);
        }
        else
        {
            _playerAnimator.SetBool("isRun_b", false);
        }
    }
    private void LookInput()
    {
        Ray ray = _cameraMain.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * _gunControllerScript.GunHeight);

        if (groundPlane.Raycast(ray, out float rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            _playerControllerScript.LookAt(point);
            _crosshairs.transform.position = point;
            _crosshairs.DetectTarget(ray);

            if ((new Vector2(point.x, point.z) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 1f)
            {
                _gunControllerScript.Aim(point);
            }
        }
    }
    private void WeaponInput()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            _gunControllerScript.OnTriggerHold();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _gunControllerScript.OnTriggerRelease();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            _gunControllerScript.Reload();
        }
    }
    private void CheckPlayerIsFall()
    {
        if (transform.position.y < _playerFallBound)
        {
            TakeDamage(health);
        }
    }
    public override void Die()
    {
        AudioManager.Instance.PlaySound("Player Death", transform.position);

        base.Die();
    }
}