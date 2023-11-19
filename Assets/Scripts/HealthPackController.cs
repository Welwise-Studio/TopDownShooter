using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackController : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        Destroy(gameObject, 5);
    }
    private void OnCollisionEnter (Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player.AddHealth(50);
            Destroy(gameObject);
        }

        //if (TryGetComponent<Player>(out var Player))
        //{
        //    Player.AddHealth(50);
        //    Destroy(gameObject);
        //}
    }
}
