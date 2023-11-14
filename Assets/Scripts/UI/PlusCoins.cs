using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlusCoins : MonoBehaviour, IPointerDownHandler
{
    [Range(0, 1)]
    [SerializeField] private float _rotationStrength = 0.15f;
    [SerializeField] private float _positionStrength = 2f;
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _cdTime;
    [SerializeField] private PlusModal _modal;
    private float _cdTimer;
    private float _elapsed;
    private float _currentMagnitude = 1f;


    private Vector3 _startPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        _modal.Show();
    }

    private void Awake()
    {
        _startPosition = transform.localPosition;
    }

    private void Update()
    {
        if (_cdTimer != 0)
        {
            _cdTimer += Time.deltaTime;
            if (_cdTimer >= _cdTime)
                _cdTimer = 0;
            else
                return;
        }

        if (_elapsed < _duration)
        {
            float x = (Random.value - 0.5f) * _currentMagnitude * _positionStrength;
            float y = (Random.value - 0.5f) * _currentMagnitude * _positionStrength;

            float lerpAmount = _currentMagnitude * _rotationStrength;
            Vector3 lookAtVector = Vector3.Lerp(Vector3.forward, Random.insideUnitCircle, lerpAmount);

            transform.localPosition = _startPosition + new Vector3(x, y, 0);
            transform.eulerAngles = new Vector3(0, 0, Quaternion.LookRotation(lookAtVector).eulerAngles.x);

            _elapsed += Time.deltaTime;
            _currentMagnitude = (1 - (_elapsed / _duration)) * (1 - (_elapsed / _duration));

        }
        else
        {
            transform.localPosition = _startPosition;
            transform.localRotation = Quaternion.identity;
            _elapsed = 0;
            _cdTimer = Time.deltaTime;
        }
    }
}