using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private bool _isActive = false;

    [Header("Links")]
    [SerializeField] private GameObject[] _roadPrefabs;
    [SerializeField] private GameObject[] _startRoadTiles;
    [SerializeField] private Transform _roadParentTransform;
    private Queue<GameObject> _activeTiles = new Queue<GameObject>();

    [Header("Spawner Settings")]
    [SerializeField] private Vector3 _roadVelocity;
    [SerializeField] private float _safeZoneRadius;

    [Header("Gizmos Settings")]
    [SerializeField] private float _gizmosSphereRadius;

    private Vector3 _startPosition;
    private Vector3 _spawnPointPostion;
    private float _removePointXPostion;
    private GameObject _lastRoadTile;




    private void Start()
    {
        _startPosition = _roadParentTransform.position;
        _spawnPointPostion = _startPosition + Vector3.right * _safeZoneRadius;
        _removePointXPostion = _startPosition.x - _safeZoneRadius;

        foreach (GameObject tile in _startRoadTiles) _activeTiles.Enqueue(tile);
        _lastRoadTile = _activeTiles.Peek();
    }


    private void Update()
    {
        if (_isActive)
        {
            _roadParentTransform.position += _roadVelocity * Time.deltaTime;
            if (_lastRoadTile.transform.position.x <= _removePointXPostion)
            {
                AddTile();
                DeleteLastTile();

                _lastRoadTile = _activeTiles.Peek();
            }
        }
    }


    private void AddTile()
    {
        GameObject tile = Instantiate(_roadPrefabs[Random.Range(0, _roadPrefabs.Length)],_spawnPointPostion, Quaternion.identity, _roadParentTransform);
        _activeTiles.Enqueue(tile);
    }

    private void DeleteLastTile()
    {
        Destroy(_activeTiles.Dequeue());
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.right * _safeZoneRadius, _gizmosSphereRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position - Vector3.right * _safeZoneRadius, _gizmosSphereRadius);

    }
}
