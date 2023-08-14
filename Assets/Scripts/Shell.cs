using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Shell : MonoBehaviour
{
    private Rigidbody _objectRb;
    [Header("Force")]
    [SerializeField] private float _forceMin;
    [SerializeField] private float _forceMax;

    [Header("Time")]
    [SerializeField] private float _lifeTime = 4f;
    [SerializeField] private float _fadeTime = 2f;
    private void Awake()
    {
        _objectRb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        float force = Random.Range(_forceMin, _forceMax);
        _objectRb.AddForce(transform.right * force);
        _objectRb.AddTorque(Random.insideUnitSphere * force);

        StartCoroutine(Fade());
    }
    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(_lifeTime);

        float percent = 0;
        float fadeSpeed = 1 / _fadeTime;
        Material mat = GetComponent<Renderer>().material;
        Color initialColor = mat.color;

        while (percent < 1)
        {
            percent += Time.deltaTime * fadeSpeed;
            mat.color = Color.Lerp(initialColor, Color.clear, percent);
            yield return null;
        }

        Destroy(gameObject);
    }
}
