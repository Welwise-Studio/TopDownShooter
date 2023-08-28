using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crosshairs : MonoBehaviour
{
    [SerializeField] private bool _enableRotateAnimation;
    [SerializeField] private float _rotateSpeed = 40f;
    [SerializeField] private SpriteRenderer _crosshairHighlight;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private Color _highlightColor;
    private Color _originalColor;

    [Header("Crosshair Ammo Text")]
    [SerializeField] private GunController _gunControllerScript;
    [SerializeField] private TextMeshProUGUI _crosshairAmmoCountText;
    private void Start()
    {
        Cursor.visible = false;
        _originalColor = _crosshairHighlight.color;
    }
    void Update()
    {
        RotateAnimation(_rotateSpeed);

        CheckAmmo();
    }
    private void RotateAnimation(float rotateSpeed)
    {
        if (_enableRotateAnimation)
        {
            transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
        }
    }
    public void DetectTarget(Ray ray)
    {
        if (Physics.Raycast(ray, 100, _targetMask))
        {
            _crosshairHighlight.color = _highlightColor;
        }
        else
        {
            _crosshairHighlight.color = _originalColor;
        }
    }
    private void CheckAmmo()
    {
        if (_gunControllerScript._equippedGun._projectilesRemainingInMag == 0)
        {
            _crosshairAmmoCountText.SetText("R");
        }
        else
        {
            _crosshairAmmoCountText.SetText(_gunControllerScript._equippedGun._projectilesRemainingInMag.ToString());
        }
    }
}
