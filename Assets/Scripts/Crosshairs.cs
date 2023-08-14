using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshairs : MonoBehaviour
{
    [SerializeField] private float _crosshairRotateSpeed = 40f;
    [SerializeField] private SpriteRenderer _crosshair;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private Color _highlightColor;
    private Color _originalColor;
    private void Start()
    {
        Cursor.visible = false;
        _originalColor = _crosshair.color;
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * _crosshairRotateSpeed * Time.deltaTime);
    }
    public void DetectTarget(Ray ray)
    {
        if (Physics.Raycast(ray, 100, _targetMask))
        {
            _crosshair.color = _highlightColor;
        }
        else
        {
            _crosshair.color = _originalColor;
        }
    }
}
