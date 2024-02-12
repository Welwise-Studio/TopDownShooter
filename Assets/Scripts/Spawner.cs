using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Spawner : MonoBehaviour, ISpawner
{
    public event System.Action<int> OnNewWave;

    [Header("Enemy Spawn:")]
    [Space(10)]
    [SerializeField] private float _spawnDistance = 5;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private GameObject[] _defaultEnemyModels;
    [field: SerializeField] public Wave[] waves { get; private set; }

    private LivingEntity _playerEntity;
    private Transform _playerTransform;

    private Wave _currentWave;
    public int MaxWaveNumber => waves.Length;
    public int CurrentWaveNumber;

    private int _enemiesRemainingToSpawn;
    private int _enemiesRemeiningAlives;
    private float _nextSpawnTime;
    private bool _isCamping;
    private MapGenerator _map;
    [SerializeField][Tooltip("Disable enemy spawn")] private bool _isDisabled;
    [SerializeField][Tooltip("Seconds to spawn enemy under player")] private float _timeBetweenCampingChecks = 2f;
    [SerializeField] private float _campThresholdDistance = 1.5f;
    private float _nextCampCheckTime;
    private Vector3 _campPositionOld;
    private void OnDisable()
    {
        _playerEntity.OnDeath -= OnPlayerDeath;
        CombinedSDK.OnCombinedSDKInitilizedEvent -= GetData;
    }
    private void OnEnable()
    {
        CombinedSDK.OnCombinedSDKInitilizedEvent += GetData;
    }
    private void Start()
    {
        _isDisabled = true;
        _playerEntity = FindObjectOfType<Player>();
        _playerTransform = _playerEntity.transform;
        _nextCampCheckTime = _timeBetweenCampingChecks + Time.time;
        _campPositionOld = _playerTransform.position;

        _playerEntity.OnDeath += OnPlayerDeath;

        _map = FindObjectOfType<MapGenerator>();
        if (CombinedSDK.IsInitilized)
            GetData();
        //NextWave();
    }
    private void GetData()
    {
        CurrentWaveNumber = CombinedSDK.AllSavesCombinedSDK.LastWaveIndex;
        _isDisabled = false;
        NextWave();
    }
    private void Update()
    {
        if (!_isDisabled)
        {
            if (Time.time > _nextCampCheckTime)
            {
                _nextCampCheckTime = Time.time + _timeBetweenCampingChecks;

                _isCamping = Vector3.Distance(_playerTransform.position, _campPositionOld) < _campThresholdDistance;
                _campPositionOld = _playerTransform.position;
            }

            if ((_enemiesRemainingToSpawn > 0 || _currentWave.infinite) && Time.time > _nextSpawnTime)
            {
                _enemiesRemainingToSpawn--;
                _nextSpawnTime = Time.time + _currentWave.timeBetweenSpawns;

                StartCoroutine("SpawnEnemy");
            }
        }
#if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.Return)) //Next wave to press ENTER.
        {
            StopCoroutine("SpawnEnemy");
            foreach (Enemy enemy in FindObjectsOfType<Enemy>())
            {
                Destroy(enemy);
            }
            NextWave();
        }
#endif
    }

    private IEnumerator SpawnEnemy()
    {

        Transform spawnTile = _map.GetRandomOpenTile();

        if (_isCamping)
        {
            spawnTile = _map.GetTileFromPosition(_playerTransform.position);
        }

        var playerPos = _playerEntity.transform.position;
        Vector3 spawnPos;
        var rand = Random.insideUnitCircle.normalized;
        rand = rand == Vector2.zero ? Vector2.one : rand;
        Vector3 randomPoint = new Vector3(rand.x, 0 , rand.y) * _spawnDistance;
        var endPoint = playerPos + randomPoint;
        if (Physics.Linecast(playerPos, endPoint, out var hit, LayerMask.NameToLayer("Player")))
        {
            spawnPos = hit.point;
        }
        else
        {
            spawnPos = endPoint;
        }
        var spawnedEnemy = Instantiate(_enemyPrefab, spawnPos, Quaternion.identity);

        spawnedEnemy.Poof();

        spawnedEnemy.OnDeath += OnEnemyDeath;

        int rEnemyPrefab = Random.Range(0, _currentWave.enemyModels.Length == 0 || _currentWave.enemyModels == null ? _defaultEnemyModels.Length - 1 : _currentWave.enemyModels.Length - 1);

        spawnedEnemy.SetCharacteristics(_currentWave.moveSpeed,
        _currentWave.hitsToKillPlayer,
        _currentWave.enemyHealth,
        _currentWave.bloodColor,
        _currentWave.enemyModels.Length == 0 || _currentWave.enemyModels == null ? _defaultEnemyModels[rEnemyPrefab] : _currentWave.enemyModels[rEnemyPrefab]);
        yield return null;
    }
    private void OnPlayerDeath()
    {
        _isDisabled = true;
    }
    private void OnEnemyDeath()
    {
        _enemiesRemeiningAlives--;

        if (_enemiesRemeiningAlives <= 0)
        {
            NextWave();
        }
    }
    private void ResetPlayerPosition()
    {
        _playerTransform.position = _map.GetTileFromPosition(Vector3.zero).position + Vector3.up * 1f;
    }
    private void NextWave()
    {
        if (CurrentWaveNumber > 0)
        {
            AudioManager.Instance.PlaySound2D("Level Complete");
        }

        CurrentWaveNumber++;

        if (CurrentWaveNumber - 1 < waves.Length)
        {
            _currentWave = waves[CurrentWaveNumber - 1];

            _enemiesRemainingToSpawn = _currentWave.enemyCount;
            _enemiesRemeiningAlives = _enemiesRemainingToSpawn;

            OnNewWave?.Invoke(CurrentWaveNumber);
        }

        //ResetPlayerPosition();
    }
}

[System.Serializable]
public class Wave
{
    public GameObject[] enemyModels;
    public bool infinite;
    [Min(0)] public int enemyCount;
    [Min(0.1f)] public float timeBetweenSpawns;
    [Min(0)] public float moveSpeed;
    [Min(1)][Tooltip("Hits / Player Health")] public int hitsToKillPlayer;
    [Min(1)] public float enemyHealth;
    public Color bloodColor = Color.green;
}