using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{
    public bool ControllLook = true;
    public bool MobileControll = false;
    public GunController GunControllerScript {  get; private set; }
    public PlayerController Controller { get; private set; }
    [field: SerializeField] public Crosshairs Crosshairs {  get; private set; }
    [SerializeField] private DieModal _dieModal;
    [SerializeField] private Joystick _variableJoystickMove;
    [SerializeField] private Animator _playerAnimator;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _playerFallBound = -10.0f;
    private Camera _cameraMain;

    private void Awake()
    {
        Controller = GetComponent<PlayerController>();
        GunControllerScript = GetComponent<GunController>();
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
        GunControllerScript.EquipGun(0);
    }
    private void CheckAmmoForReload()
    {
        if (GunControllerScript._equippedGun._projectilesRemainingInMag == 0)
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
        Vector3 moveInput = MobileControll ? new Vector3(_variableJoystickMove.Horizontal, 0, _variableJoystickMove.Vertical) : new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * _moveSpeed;
        Controller.Move(moveVelocity);

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
        if (!ControllLook)
            return;

        Ray ray = _cameraMain.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * GunControllerScript.GunHeight);

        if (groundPlane.Raycast(ray, out float rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);

            Controller.LookAt(point);
            Crosshairs.transform.position = point;
            Crosshairs.DetectTarget(ray);

            if ((new Vector2(point.x, point.z) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 1f)
            {
                GunControllerScript.Aim(point);
            }
        }
    }
    private void WeaponInput()
    {
        if (MobileControll)
            return;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            GunControllerScript.OnTriggerHold();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GunControllerScript.OnTriggerRelease();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            GunControllerScript.Reload();
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
        _dieModal.Show();
        Debug.Log("Die");
        base.Die();
    }
}