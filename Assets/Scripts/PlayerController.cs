using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody _objectRb;
    private Vector3 _velocity;
    private void Start()
    {
        _objectRb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        _objectRb.MovePosition(_objectRb.position + _velocity * Time.fixedDeltaTime);
    }
    public void Move(Vector3 velocity)
    {
        _velocity = velocity;
    }
    public void LookAt(Vector3 lookPoint)
    {
        //Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        //transform.LookAt(heightCorrectedPoint);
        Vector3 heightCorrectedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        Quaternion targetRotation = Quaternion.LookRotation(heightCorrectedPoint - transform.position);
        _objectRb.MoveRotation(targetRotation);
    }
}
