using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WavePresenter : MonoBehaviour
{
    [SerializeField] private string _mainLabelText = "Wave ";
    [SerializeField] private string _subLabelText = "Enemies ";
    [SerializeField] private TextMeshProUGUI _mainLabel;
    [SerializeField] private TextMeshProUGUI _subLabel;
    [SerializeField] private float _hidePosition;
    [SerializeField] private float _showPosition;
    [SerializeField] private float _movingDuration = .1f;
    [SerializeField] private float _showDuration = 2;

    private bool _isShowing;
    private RectTransform _transform;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    public void Show(string mainText, string subText)
    {
        if (_isShowing) return;

        _mainLabel.text = _mainLabelText+mainText;
        _subLabel.text = _subLabelText+subText;
        StartCoroutine(ShowRoutine());
    }

    private IEnumerator ShowRoutine()
    {
        _isShowing = true;
        yield return MoveRoutine(_hidePosition, _showPosition);
        yield return new WaitForSeconds(_showDuration);
        yield return MoveRoutine(_showPosition, _hidePosition);
        _isShowing = false;
    }

    private IEnumerator MoveRoutine(float from, float to)
    {
        var destination = new Vector3(_transform.anchoredPosition.x, to);
        var start = new Vector3(_transform.anchoredPosition.x, from);
        for (float t = 0; t <= _movingDuration; t += Time.deltaTime)
        {
            _transform.anchoredPosition = Vector2.Lerp(start, destination, Mathf.SmoothStep(0, 1, t / _movingDuration));
            yield return new WaitForEndOfFrame();
        }
        _transform.anchoredPosition = destination;
    }
}
