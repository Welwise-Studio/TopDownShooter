using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Color _trailColor;
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _bulletLifetime = 5f;
    [SerializeField] private float _damage = 1f;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private float _skinWidth = 0.1f;
    private void Start()
    {
        BulletLifeTime();

        Collider[] initialCollision = Physics.OverlapSphere(transform.position, 0.1f, _collisionMask);
        if (initialCollision.Length > 0)
        {
            OnHitObject(initialCollision[0], transform.position);
        }

        GetComponent<TrailRenderer>().materials[0].color = _trailColor;
    }
    private void Update()
    {
        BulletMove();
    }
    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
    private void BulletMove()
    {
        float moveDistance = _speed * Time.deltaTime;
        CheckCollison(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }
    private void BulletLifeTime()
    {
        Destroy(gameObject, _bulletLifetime);
    }
    private void CheckCollison(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, moveDistance + _skinWidth, _collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit.collider, hit.point);
        }
    }
    private void OnHitObject(Collider collider, Vector3 hitPoint)
    {
        IDamageble damagebleObject = collider.GetComponent<IDamageble>();
        damagebleObject?.TakeHit(_damage, hitPoint, transform.forward);
        Destroy(gameObject);
    }
}