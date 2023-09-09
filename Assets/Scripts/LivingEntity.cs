using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageble
{
    public float startingHealth;
    public float health { get; protected set; }
    protected bool dead;
    public event System.Action OnDeath;
    protected virtual void Start()
    {
        AddHealth(startingHealth);
    }
    public virtual void TakeHit(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        TakeDamage(damage);
    }
    public virtual void TakeDamage(float damage)
    {
        health -= damage;

        if (GetComponent<Player>() != null)
        {
            if (Camera.main.GetComponent<CameraShaker>().isEnable)
            {
                Camera.main.GetComponent<CameraShaker>().Shake();
            }
        }

        if (health <= 0 && !dead)
        {
            Die();
        }
    }
    public void AddHealth(float addHealth)
    {
        if ((addHealth + health) >=  startingHealth)
        {
            health = startingHealth;
        }
        else
        {
            health += addHealth;
        }
    }
    [ContextMenu("Self Destruct")]
    public virtual void Die()
    {
        dead = true;

        OnDeath?.Invoke();

        Destroy(gameObject);
    }
}