using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPresenter : MonoBehaviour
{
    [SerializeField] private LivingEntity _target;
    [SerializeField] private RectTransform _healthImage;
    [SerializeField] private RectTransform _canvas;

    private void Update()
    {
        if (_target == null)
            return;

        _healthImage.sizeDelta = new Vector2(_canvas.rect.width * Mathf.Clamp01(_target.health / _target.startingHealth), _healthImage.sizeDelta.y);
    }
}
