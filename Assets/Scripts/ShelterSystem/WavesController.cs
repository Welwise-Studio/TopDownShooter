using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace ShelterSystem
{
    public class WavesController : MonoBehaviour, ISpawner
    {
        public event System.Action<int> OnNewWave;
        public event System.Action OnEndSpawn;
        public bool IsLastWave => _currentWaveNumber >= (waves.Length);

        [SerializeField]
        private LivingEntity[] _targetsPriority;

        [Header("Enemy Spawn:")]
        [SerializeField] private Transform[] _spawnPoints;
        [Space(10)]
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private GameObject[] _defaultEnemyModels;
        [field: SerializeField] public Wave[] waves { get; private set; }

        private Wave _currentWave;
        private int _currentWaveNumber;
        private LivingEntity _currentTarget;

        private int _enemiesRemainingToSpawn;
        private int _enemiesRemeiningAlives;
        private float _nextSpawnTime;
        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();

        [SerializeField][Tooltip("Disable enemy spawn")] public bool IsDisabled;
        [SerializeField] private float _timeToStart = 7;
        private void OnDisable()
        {
            foreach (var item in _targetsPriority)
            {
                item.OnDeath -= OnTargetDeath;
            }
            CombinedSDK.OnCombinedSDKInitilizedEvent -= GetData;
        }

        private void OnEnable()
        {
            CombinedSDK.OnCombinedSDKInitilizedEvent += GetData;
        }

        private void Start()
        {
            IsDisabled = true;
            foreach (var item in _targetsPriority)
            {
                item.OnDeath += OnTargetDeath;
            }
            _currentTarget = _targetsPriority[0];

            if (CombinedSDK.IsInitilized)
                GetData();

            Invoke(nameof(EnableSpawner), _timeToStart);
        }

        private void EnableSpawner() => IsDisabled = false; 

        private void GetData()
        {
            _currentWaveNumber = CombinedSDK.AllSavesCombinedSDK.LastWaveIndex - 1;
            NextWave();
        }

        private void Update()
        {
            if (!IsDisabled)
            {                
                if ((_enemiesRemainingToSpawn > 0 || _currentWave.infinite) && Time.time > _nextSpawnTime)
                {
                    _enemiesRemainingToSpawn--;
                    _nextSpawnTime = Time.time + _currentWave.timeBetweenSpawns;

                    StartCoroutine(SpawnEnemy());
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
            var spawnedEnemy = Instantiate(_enemyPrefab, _spawnPoints[Random.Range(0, _spawnPoints.Length)].position, Quaternion.identity);
            _enemies.Add(spawnedEnemy);
            spawnedEnemy.Poof();

            spawnedEnemy.OnDeath += OnEnemyDeath;

            int rEnemyPrefab = Random.Range(0, _currentWave.enemyModels.Length == 0 || _currentWave.enemyModels == null ? _defaultEnemyModels.Length - 1 : _currentWave.enemyModels.Length - 1);

            spawnedEnemy.SetCharacteristics(_currentWave.moveSpeed,
            _currentWave.hitsToKillPlayer,
            _currentWave.enemyHealth,
            _currentWave.bloodColor,
            _currentWave.enemyModels.Length == 0 || _currentWave.enemyModels == null ? _defaultEnemyModels[rEnemyPrefab] : _currentWave.enemyModels[rEnemyPrefab]);
            spawnedEnemy.SetTarget(_currentTarget);
            spawnedEnemy.IsDropItems = false;
            //spawnedEnemy.UseTargetDeath = false;
            yield return null;
        }
        private void OnTargetDeath()
        {
            Debug.Log($"OnTargetDeath() | {_enemies.Count}");
            foreach (var entity in _targetsPriority)
            {
                if (!entity.dead) 
                {
                    _currentTarget = entity;
                    foreach (var item in _enemies)
                    {
                        Debug.Log($"SET {item.gameObject.name} target {_currentTarget.gameObject.name}");
                        item.SetTarget(_currentTarget);
                    }
                    return;
                }
            }

            IsDisabled = true;
        }
        private void OnEnemyDeath()
        {
            _enemiesRemeiningAlives--;

            if (_enemiesRemeiningAlives <= 0)
            {
                NextWave();
            }
        }
        private void NextWave()
        {
            if (_currentWaveNumber >= waves.Length)
                OnEndSpawn?.Invoke();
            if (_currentWaveNumber > 0)
            {
                AudioManager.Instance.PlaySound2D("Level Complete");
            }

            _currentWaveNumber++;

            if (_currentWaveNumber - 1 < waves.Length)
            {
                _enemies.Clear();
                _currentWave = waves[_currentWaveNumber - 1];

                _enemiesRemainingToSpawn = _currentWave.enemyCount;
                _enemiesRemeiningAlives = _enemiesRemainingToSpawn;

                OnNewWave?.Invoke(_currentWaveNumber);
            }
        }
    }
}
