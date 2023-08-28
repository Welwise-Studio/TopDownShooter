using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private bool _enableRotateAnimation;
    [SerializeField] private float _rotateSpeed = 40f;
    void Update()
    {
        RotateAnimation(_rotateSpeed);
    }
    private void RotateAnimation(float rotateSpeed)
    {
        if (_enableRotateAnimation)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
    }
}
