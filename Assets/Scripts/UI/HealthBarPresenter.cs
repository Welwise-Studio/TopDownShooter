using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarPresenter : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private RectTransform _healthImage;
    [SerializeField] private RectTransform _canvas;

    private void Update()
    {
        if (_player == null)
            return;

        _healthImage.sizeDelta = new Vector2(_canvas.rect.width * Mathf.Clamp01(_player.health / _player.startingHealth), _healthImage.sizeDelta.y);
    }
}
