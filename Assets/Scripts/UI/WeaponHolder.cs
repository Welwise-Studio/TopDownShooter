using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] private float _switchedHidePosition;
    [SerializeField] private float _switchedShowPosition;
    [SerializeField] private float _switchDuration = .4f;
    [SerializeField] private RectTransform _switchedRect;
    [SerializeField] private float _switchedShowDuration = .2f;
    [SerializeField] private Image _icon;
    [SerializeField] private GunController _gunController;

    private RectTransform _iconRect;

    [SerializeField] private Sprite test;
    private Vector2 _startSize;

    private void Awake()
    {
        _iconRect = _icon.gameObject.GetComponent<RectTransform>();
        _startSize = _iconRect.sizeDelta;
        _gunController.OnGunChanged += (_) => { Switch(_.Icon); };
    }


    public void Switch(Sprite icon)
    {
        StartCoroutine(IconSwapRoutine(icon));
        StartCoroutine(TextSwitchRoutine());
    }

    private IEnumerator TextSwitchRoutine()
    {
        yield return MoveRoutine(_switchedHidePosition, _switchedShowPosition, (_switchDuration - _switchedShowDuration) / 2);
        yield return new WaitForSeconds(_switchedShowDuration);
        yield return MoveRoutine(_switchedShowPosition, _switchedHidePosition, (_switchDuration - _switchedShowDuration) / 2);

    }

    private IEnumerator IconSwapRoutine(Sprite icon)
    {
        yield return SizeRoutine(1, 0, _switchDuration / 2);
        _icon.sprite = icon;
        yield return SizeRoutine(0, 1, _switchDuration / 2);
    }

    private IEnumerator SizeRoutine(float from, float to, float duration)
    {
        var start = _startSize * from;
        var destination = _startSize * to;
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            _iconRect.sizeDelta = Vector2.Lerp(start, destination, Mathf.SmoothStep(0, 1, t / duration));
            yield return new WaitForEndOfFrame();
        }
        _iconRect.sizeDelta = destination;
    }

    private IEnumerator MoveRoutine(float from, float to, float duration)
    {
        var destination = new Vector3(_switchedRect.anchoredPosition.x, to);
        var start = new Vector3(_switchedRect.anchoredPosition.x, from);
        for (float t = 0; t <= duration; t += Time.deltaTime)
        {
            _switchedRect.anchoredPosition = Vector2.Lerp(start, destination, Mathf.SmoothStep(0, 1, t / duration));
            yield return new WaitForEndOfFrame();
        }
        _switchedRect.anchoredPosition = destination;
    }
}
