using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private int _addValue;
    private Wallet _wallet;

    private void Awake()
    {
        _wallet = FindObjectOfType<Wallet>();
        Destroy(gameObject,5);
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _wallet.Add(_addValue);
            Destroy(gameObject);
        }
    }
}
