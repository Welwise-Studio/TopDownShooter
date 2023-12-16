using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using YG;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreUi;
    [SerializeField] private Image _fadePlane;
    [SerializeField] private GameObject _gameOverUI;
    [SerializeField] private TextMeshProUGUI _gameOverScoreUi;
    [SerializeField] private HealthBarPresenter _healthBarPresenter;
    [SerializeField] private WavePresenter _wavePresenter;

    private Player _playerScript;
    private Spawner _spawnerScript;

    private void Awake()
    {
        _spawnerScript = FindObjectOfType<Spawner>();
        _spawnerScript.OnNewWave += OnNewWave;
    }
    void Start()
    {
        _playerScript = FindObjectOfType<Player>();
        _playerScript.OnDeath += OnGameOver;
    }
    private void Update()
    {
        _scoreUi.SetText(ScoreKeeper.score.ToString("D6"));
    }
    private void OnNewWave(int waveNumber)
    {
        string[] numbers = new string[5];
        string enemyCountString;
        if (YandexGame.EnvironmentData.language == "ru")
        {
            numbers[0] = "Один";
            numbers[1] = "Два";
            numbers[2] = "Три";
            numbers[3] = "Четыре";
            numbers[4] = "Пять";
            enemyCountString = (_spawnerScript.waves[waveNumber - 1].infinite) ? "Бесконечность" : $"{_spawnerScript.waves[waveNumber - 1].enemyCount}";
        }
        else
        {

            numbers[0] = "One";
            numbers[1] = "Two";
            numbers[2] = "Three";
            numbers[3] = "Four";
            numbers[4] = "Five";
            enemyCountString = (_spawnerScript.waves[waveNumber - 1].infinite) ? "Infinite" : $"{_spawnerScript.waves[waveNumber - 1].enemyCount}";
        }


        _wavePresenter.Show(numbers[waveNumber - 1], enemyCountString);
    }
    private void OnGameOver()
    {
        Cursor.visible = true;

        StartCoroutine(Fade(Color.clear, new Color(0, 0, 0, 0.95f), 1f));

        _gameOverScoreUi.text = _scoreUi.text;
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
