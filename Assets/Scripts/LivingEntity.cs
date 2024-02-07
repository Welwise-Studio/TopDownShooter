using HealthSystem;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageble
{

    [SerializeField]
    private ItemDropController _itemDrop;
    public float startingHealth;

    public bool IsDropItems = true;

    [field: SerializeField]
    public float health { get; protected set; }
    public bool dead;
    public event System.Action OnDeath;
    protected virtual void Awake()
    {
        AddHealth(startingHealth);
    }
    protected virtual void Start()
    {
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
        if (IsDropItems)
            _itemDrop?.Drop();
        dead = true;

        OnDeath?.Invoke();

        Destroy(gameObject);

    }

    protected void CallOnDeath() => OnDeath?.Invoke();
}