using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField] private float _streakExpiryTime = 1f;
    public static int score { get; private set; }
    private float _lastEnemyKillTime;
    private int _streakCount;
    private void Start()
    {
        Enemy.OnDeathStatic += OnEnemyKilled;
        FindObjectOfType<Player>().OnDeath += OnPlayerDeath;
    }
    private void OnEnemyKilled()
    {
        if (Time.time < _lastEnemyKillTime + _streakExpiryTime)
        {
            _streakCount++;
        }
        else
        {
            _streakCount = 0;
        }

        _lastEnemyKillTime = Time.time;

        score += 5 + (int)Mathf.Pow(2, _streakCount);
    }
    private void OnPlayerDeath()
    {
        Enemy.OnDeathStatic -= OnEnemyKilled;
    }
}
