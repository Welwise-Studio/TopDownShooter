using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Crosshairs _crosshairs;
    [SerializeField] private Animator _playerAnimator;
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

        CheckPlayerIsFall(-10);
    }
    public void OnNewWave(int waveNumber)
    {
        health = startingHealth;
        _gunControllerScript.EquipGun(waveNumber - 1);
    }
    private void MovementInput()
    {
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * _moveSpeed;
        //_playerAnimator.SetFloat("isRun_f", _moveSpeed);
        _playerControllerScript.Move(moveVelocity);
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
    private void CheckPlayerIsFall(float yBound)
    {
        if (transform.position.y < yBound)
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