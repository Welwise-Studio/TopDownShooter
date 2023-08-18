using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyPrefabs;
    public Wave[] waves;

    private LivingEntity _playerEntity;
    private Transform _playerT;

    private Wave _currentWave;
    private int _currentWaveNumber;

    private int _enemiesRemainingToSpawn;
    private int _enemiesRemeiningAlives;
    private float _nextSpawnTime;
    private bool _isCamping;

    private MapGenerator _map;

    private float _timeBetweenCampingChecks = 2f;
    private float _campThresholdDistance = 1.5f;
    private float _nextCampCheckTime;
    private Vector3 _campPositionOld;

    private bool _isDisabled;

    public event System.Action<int> OnNewWave;
    private void Start()
    {
        _playerEntity = FindObjectOfType<Player>();
        _playerT = _playerEntity.transform;
        _nextCampCheckTime = _timeBetweenCampingChecks + Time.time;
        _campPositionOld = _playerT.position;

        _playerEntity.OnDeath += OnPlayerDeath;

        _map = FindObjectOfType<MapGenerator>();
        NextWave();
    }
    private void Update()
    {
        if (!_isDisabled)
        {
            if (Time.time > _nextCampCheckTime)
            {
                _nextCampCheckTime = Time.time + _timeBetweenCampingChecks;

                _isCamping = (Vector3.Distance(_playerT.position, _campPositionOld) < _campThresholdDistance);
                _campPositionOld = _playerT.position;
            }

            if ((_enemiesRemainingToSpawn > 0 || _currentWave.infinite) && Time.time > _nextSpawnTime)
            {
                _enemiesRemainingToSpawn--;
                _nextSpawnTime = Time.time + _currentWave.timeBetweenSpawns;

                StartCoroutine("SpawnEnemy");
            }
        }
        #if UNITY_EDITOR
        if (Input.GetKeyUp(KeyCode.Return))
        {
            StopCoroutine("SpawnEnemy");
            foreach(Enemy enemy in FindObjectsOfType<Enemy>())
            {
                Destroy(enemy);
            }
            NextWave();
        }
        #endif
    }
    private IEnumerator SpawnEnemy()
    {
        float spawnDelay = 1f;
        float tileFlashSpeed = 4f;

        Transform spawnTile = _map.GetRandomOpenTile();

        if (_isCamping)
        {
            spawnTile = _map.GetTileFromPosition(_playerT.position);
        }

        Material tileMat = spawnTile.GetComponent<Renderer>().material;
        Color initialColor = Color.white;
        Color flashColour = Color.red;
        float spawnTimer = 0;

        while (spawnTimer < spawnDelay)
        {
            tileMat.color = Color.Lerp(initialColor, flashColour, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1f));
            spawnTimer += Time.deltaTime;
            yield return null;
        }

        int rEnemyPrefab = Random.Range(0, _enemyPrefabs.Length - 1);
        Enemy spawnedEnemy = Instantiate(_enemyPrefabs[rEnemyPrefab], spawnTile.position + Vector3.up, Quaternion.identity);

        spawnedEnemy.OnDeath += OnEnemyDeath;
        spawnedEnemy.SetCharacteristics(_currentWave.moveSpeed, 
        _currentWave.hitsToKillPlayer, 
        _currentWave.enemyHealth, 
        _currentWave.skinColor);
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
        _playerT.position = _map.GetTileFromPosition(Vector3.zero).position + Vector3.up * 3f;
    }
    private void NextWave()
    {
        if (_currentWaveNumber > 0)
        {
            AudioManager.Instance.PlaySound2D("Level Complete");
        }

        _currentWaveNumber++;

        if (_currentWaveNumber - 1 < waves.Length)
        {
            _currentWave = waves[_currentWaveNumber - 1];

            _enemiesRemainingToSpawn = _currentWave.enemyCount;
            _enemiesRemeiningAlives = _enemiesRemainingToSpawn;

            OnNewWave?.Invoke(_currentWaveNumber);
        }

        ResetPlayerPosition();
    }

    [System.Serializable]
    public class Wave
    {
        public bool infinite;
        public int enemyCount;
        public float timeBetweenSpawns;

        public float moveSpeed;
        public int hitsToKillPlayer;
        public float enemyHealth;
        public Color skinColor;
    }
}
