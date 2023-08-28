using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _targetFollowing;
    [SerializeField] private Vector3 _cameraOffset;
    [SerializeField] private float _speed = 1f;
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (_targetFollowing != null)
        {
            Vector3 nextPosition = Vector3.Lerp(transform.position, _targetFollowing.position + _cameraOffset, Time.deltaTime * _speed);

            transform.position = nextPosition;
        }
    }
}
