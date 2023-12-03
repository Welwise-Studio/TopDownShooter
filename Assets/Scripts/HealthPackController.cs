using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackController : MonoBehaviour
{
    [SerializeField] private int _addHealth;
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _player.AddHealth(_addHealth);
            Destroy(gameObject);
        }
    }
}
