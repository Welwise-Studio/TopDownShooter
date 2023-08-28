using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] [Tooltip("Seconds")] private float _objectDestroyTimeout = 10f;
    [SerializeField] private DropItem _defaultCoin;
    [SerializeField] private DropItem[] _dropItemPrefabs;
    private int _health;
    private int _gun;
    private void OnEnable()
    {
        FindObjectOfType<Spawner>().OnNewWave += OnNewWave;
    }
    private void OnDisable()
    {
        FindObjectOfType<Spawner>().OnNewWave -= OnNewWave;
    }
    private void Start()
    {
        int rDropItemPrefab = Random.Range(0, _dropItemPrefabs.Length);

        if (Utility.DropLootChance(_dropItemPrefabs[rDropItemPrefab].dropChance))
        {
            SetItem(_dropItemPrefabs[rDropItemPrefab]);
        }
        else
        {
            SetItem(_defaultCoin);
        }
    }
    public void SetItem(DropItem model)
    {
        Instantiate(model.gameObject, this.transform.position, model.transform.rotation, this.transform);
        this._health = model.health;
        this._gun = model.gun;
        Destroy(gameObject, _objectDestroyTimeout);
    }
    private void OnNewWave(int waveNumber)
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
        {
            other.gameObject.GetComponent<GunController>().EquipGun(_gun);
            other.gameObject.GetComponent<Player>().AddHealth(_health);

            Destroy(gameObject);
        }
    }
}
