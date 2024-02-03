using ShelterSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class LootSpawner : MonoBehaviour
{
    [SerializeField] private float _dropChance = 100f;
    [SerializeField] private DropItem[] _dropItems;
    [SerializeField] private GameObject _dropEffect;
    [SerializeField][Tooltip("Seconds to destroy item")] private float _objectDestroyTimeout = 10f;
    private readonly List<GameObject> _spawnObjects = new List<GameObject>();
    private ISpawner _spawnerScript;
    private void Start()
    {
        var spawner = FindObjectOfType<Spawner>();
        var waves = FindObjectOfType<WavesController>();
        if (spawner != null)
            _spawnerScript = spawner;
        else if (waves != null)
            _spawnerScript = waves;

        _spawnerScript.OnNewWave += OnNewWave;
        Enemy.OnDeathStaticPosition += OnEnemyKilled;
    }

    private void OnDestroy()
    {
        if (_spawnerScript != null)
            _spawnerScript.OnNewWave -= OnNewWave;
    }
    private void OnEnemyKilled(Vector3 position)
    {
        if (Utility.DropChance(this._dropChance))
        {
            int rDropItemPrefab = UnityEngine.Random.Range(0, _dropItems.Length);

            if (Utility.DropChance(_dropItems[rDropItemPrefab]._dropChance))
            {
                SetItem(_dropItems[rDropItemPrefab], position);
            }
        }
    }
    public void SetItem(DropItem dropItem, Vector3 spawnPosition)
    {
        spawnPosition = new Vector3(spawnPosition.x, 0f, spawnPosition.z);

        GameObject item = new GameObject();
        _spawnObjects.Add(item);

        Instantiate(_dropEffect, spawnPosition, dropItem.transform.rotation, item.transform);
        Instantiate(dropItem, spawnPosition, dropItem.transform.rotation, item.transform);

        Destroy(item, _objectDestroyTimeout);
    }
    private void OnNewWave(int waveNumber)
    {
        if (_spawnObjects.Count != 0)
        {
            foreach (GameObject item in _spawnObjects)
            {
                if (item != null)
                {
                    Destroy(item);
                }
            }
        }
    }
}