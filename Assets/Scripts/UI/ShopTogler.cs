using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopTogler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler
{
    [HideInInspector] public bool IsMobile;
    [SerializeField] private ShopPresenter _shopPresenter;
    [SerializeField] private bool _isShow;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!IsMobile) return;

        if (_shopPresenter.IsShow)
            _shopPresenter.Hide();
        else
            _shopPresenter.Show();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isShow || IsMobile) return;

        _shopPresenter.Show();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_isShow || IsMobile) return;

        _shopPresenter.Hide();
    }
}
