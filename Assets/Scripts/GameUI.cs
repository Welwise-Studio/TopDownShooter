using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image _fadePlane;
    [SerializeField] private GameObject _gameOverUI;

    [SerializeField] private RectTransform _newWaveBanner;
    [SerializeField] private TextMeshProUGUI _newWaveTitle;
    [SerializeField] private TextMeshProUGUI _newWaveEnemyCount;
    [SerializeField] private TextMeshProUGUI _scoreUi;
    [SerializeField] private TextMeshProUGUI _gameOverScoreUi;

    [SerializeField] private RectTransform _healthBar;

    private Player _playerScript;
    private Spawner _spawner;

    private void Awake()
    {
        _spawner = FindObjectOfType<Spawner>();
        _spawner.OnNewWave += OnNewWave;
    }
    void Start()
    {
        _playerScript = FindObjectOfType<Player>();
        _playerScript.OnDeath += OnGameOver;
    }
    private void Update()
    {
        _scoreUi.text = ScoreKeeper.score.ToString("D6");

        float healthPercent = 0;
        if (_playerScript != null)
        {
            healthPercent = _playerScript.health / _playerScript.startingHealth;
        }
        _healthBar.localScale = new Vector3(healthPercent, 1, 1);
    }
    private void OnNewWave(int waveNumber)
    {
        string[] numbers = { "One", "Two", "Three", "Four", "Five" };
        _newWaveTitle.SetText($"- Wave {numbers[waveNumber - 1]} -");
        string enemyCountString = (_spawner.waves[waveNumber - 1].infinite) ? "Infinite" : $"{_spawner.waves[waveNumber - 1].enemyCount}";
        _newWaveEnemyCount.SetText($"Enemies: {enemyCountString}");

        StopCoroutine("AnimateNewWaveBanner");
        StartCoroutine("AnimateNewWaveBanner");
    }
    private void OnGameOver()
    {
        Cursor.visible = true;

        StartCoroutine(Fade(Color.clear, new Color(0, 0, 0, 0.95f), 1f));

        _gameOverScoreUi.text = _scoreUi.text;
        _scoreUi.gameObject.SetActive(false);
        _healthBar.transform.parent.gameObject.SetActive(false);
        _gameOverUI.SetActive(true);
    }
    private IEnumerator AnimateNewWaveBanner()
    {
        float delayTime = 1.5f;
        float speed = 3f;
        float animatePercent = 0;
        int dir = 1;

        float endDelayTime = Time.time + 1 / speed + delayTime;

        while (animatePercent >= 0)
        {
            animatePercent += Time.deltaTime * speed * dir;

            if (animatePercent >= 1)
            {
                animatePercent = 1;

                if (Time.time > endDelayTime)
                {
                    dir = -1;
                }
            }

            _newWaveBanner.anchoredPosition = Vector3.up * Mathf.Lerp(-170, 45, animatePercent);
            yield return null;
        }
    }
    private IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            _fadePlane.color = Color.Lerp(from, to, percent);

            yield return null;
        }
    }

    //UI Input
    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
