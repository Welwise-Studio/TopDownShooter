using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DieModal : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private RectTransform _modalRect;
    [SerializeField] private TextMeshProUGUI _scoreMesh;
    [SerializeField] private Button _menuButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private float _movingDuration = .1f;
    [SerializeField] private float _hidePosition;
    [SerializeField] private float _showPosition;
    private bool _isShowing;

    private void Awake()
    {
        _menuButton.onClick.AddListener(() => SceneManager.LoadScene("Menu"));
        _restartButton.onClick.AddListener(()=> SceneManager.LoadScene("Game"));
    }
    public void Show()
    {
        if (_isShowing) return;
        _isShowing = true;
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        _scoreMesh.text = ScoreKeeper.score.ToString();
        StartCoroutine(MoveRoutine(_hidePosition, _showPosition));
    }

    public void Hide()
    {
        if (!_isShowing) return;
        _isShowing = false;
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = false;
        StartCoroutine(MoveRoutine(_showPosition, _hidePosition));
    }

    private IEnumerator MoveRoutine(float from, float to)
    {
        var destination = new Vector3(_modalRect.anchoredPosition.x, to);
        var start = new Vector3(_modalRect.anchoredPosition.x, from);
        for (float t = 0; t <= _movingDuration; t += Time.deltaTime)
        {
            _modalRect.anchoredPosition = Vector2.Lerp(start, destination, Mathf.SmoothStep(0, 1, t / _movingDuration));
            yield return new WaitForEndOfFrame();
        }
        _modalRect.anchoredPosition = destination;
    }

}
