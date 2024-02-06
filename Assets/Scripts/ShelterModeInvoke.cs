using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelterModeInvoke : MonoBehaviour
{
    [SerializeField]
    private Spawner _spawner;

    [SerializeField]
    private Teleport _teleport;


    [SerializeField]
    private Vector2Int _spawnRandomRange = new Vector2Int(4,6);

    [SerializeField]
    private float _distance = 5;

    private int _spawnIndex;
    private Player _player;


    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _teleport.gameObject.SetActive(false);
        _spawnIndex = Random.Range(_spawnRandomRange.x, _spawnRandomRange.y + 1);
    }

    private void OnEnable()
    {
        _spawner.OnNewWave += OnNewWave;
    }

    private void OnDisable()
    {
        _spawner.OnNewWave -= OnNewWave;
    }

    private void OnNewWave(int index)
    {
        if (index == _spawnIndex)
        {
            var playerPos = _player.transform.position;
            Vector3 spawnPos;
            var rand = Random.insideUnitCircle.normalized;
            rand = rand == Vector2.zero ? Vector2.one : rand;
            Vector3 randomPoint = new Vector3(rand.x, 0, rand.y) * _distance;
            var endPoint = playerPos + randomPoint;
            if (Physics.Linecast(playerPos, endPoint, out var hit, LayerMask.NameToLayer("Player")))
            {
                spawnPos = hit.point;
            }
            else
            {
                spawnPos = endPoint;
            }
            _teleport.transform.position = spawnPos;
            _teleport.gameObject.SetActive(true);
        }
    }

}
