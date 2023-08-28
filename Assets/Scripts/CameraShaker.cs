using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [field: SerializeField] public bool isEnable { get; private set; }
    [SerializeField] private float _strength;
    [SerializeField] private float _duration;

    private Vector3 _initialCameraPosition;
    private float _remainingShakeTime;

    private void Awake()
    {
        _initialCameraPosition = transform.localPosition;
        enabled = false;
    }

    public void Shake()
    {
        _remainingShakeTime = _duration;
        enabled = true;
    }

    private void Update()
    {
        if (_remainingShakeTime <= 0)
        {
            transform.localPosition = _initialCameraPosition;
            enabled = false;
        }

        transform.Translate(Random.insideUnitCircle * _strength);

        _remainingShakeTime -= Time.deltaTime;
    }
}