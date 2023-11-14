using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShopPresenter : MonoBehaviour
{
    public bool IsShow { get; private set; }

    [SerializeField] private float _hidePosition;
    [SerializeField] private float _showPosition;
    [SerializeField] private float _duration;
    private bool _isMoving;
    private RectTransform _transform;

    private void Awake()
    {
        _transform = GetComponent<RectTransform>();
    }

    public void Show()
    {
        if (_isMoving)
            return;

        StartCoroutine(MoveRoutine(_hidePosition, _showPosition));
        IsShow = true;
    }

    public void Hide()
    {
        if (_isMoving)
            return;

        StartCoroutine(MoveRoutine(_showPosition, _hidePosition));
        IsShow = false;
    }

    private IEnumerator MoveRoutine(float from, float to)
    {
        _isMoving = true;
        var destination = new Vector3(to, _transform.anchoredPosition.y);
        var start = new Vector3(from, _transform.anchoredPosition.y);
        for (float t = 0; t <= _duration; t+=Time.deltaTime)
        {
            _transform.anchoredPosition = Vector2.Lerp(start, destination, Mathf.SmoothStep(0,1,t/_duration));
            yield return new WaitForEndOfFrame();
        }
        _transform.anchoredPosition = destination;
        _isMoving = false;
    }
}
