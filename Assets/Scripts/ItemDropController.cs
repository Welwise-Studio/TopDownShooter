using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropController : MonoBehaviour
{
    [SerializeField]
    private float _coinDropChance = 0.5f;
    [SerializeField]
    private float _healDropChance = 0.1f;

    public GameObject _coin;
    public GameObject heal;

    public void Drop()
    {
        float _randomValue = Random.value;
        if (_randomValue < _coinDropChance)
        {
            Instantiate(_coin,transform.position, Quaternion.identity );
        }
        else if (_randomValue < _coinDropChance+ _healDropChance)
        {
            Instantiate(heal, transform.position, Quaternion.identity);
        }
    }
}
