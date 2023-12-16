using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using YG;

public class WavePresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _mainLabel;
    [SerializeField] private TextMeshProUGUI _subLabel;
    [SerializeField] private float _hidePosition;
    [SerializeField] private float _showPosition;
    [SerializeField] private float _movingDuration = .1f;
    [SerializeField] private float _showDuration = 2;
    [SerializeField] private TMP_FontAsset _ruFont;
    [SerializeField] private TMP_FontAsset _defaultFont;

    private bool _isShowing;
    private RectTransform _transform;
    private string _mainLabelText;
    private string _subLabelText;
    private bool _isDefine;


    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
        YandexGame.GetDataEvent += DefineText;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled)
            DefineText();
    }

    public void Show(string mainText, string subText)
    {
        if (_isShowing) return;
        if (!_isDefine)
            DefineText();

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

    private void DefineText()
    {
        _isDefine = true;
        if (YandexGame.EnvironmentData.language == "ru")
        {
            _mainLabelText = "Волна ";
            _subLabelText = "Врагов ";
            _mainLabel.font = _ruFont;
            _subLabel.font = _ruFont;
        }
        else
        {
            _mainLabelText = "Wave ";
            _subLabelText = "Enemies ";
            _mainLabel.font = _defaultFont;
            _subLabel.font = _defaultFont;
        }
    }
}
