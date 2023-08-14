using UnityEngine;

public interface IDamageble
{
    public void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    { }
    public void TakeDamage(float damage)
    { }
}
