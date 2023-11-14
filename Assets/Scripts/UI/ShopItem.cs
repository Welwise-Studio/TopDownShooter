using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour, IPointerDownHandler
{
    public Action<ShopItem> OnClicked;
    [field: SerializeField] public int Price { get; private set; }
    [field: SerializeField] public Gun Gun { get; private set; }
    public bool IsLocked {  get; private set; }

    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _priceLabel;
    [SerializeField] private GameObject _checkMark;
    [SerializeField] private GameObject[] _hideInUnlock;
    [SerializeField] private GameObject[] _hideInLock;
    

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClicked?.Invoke(this);
    }

    private void Start()
    {
        UpdateStyle();
    }

    public void Shake()
    {

    }

    public void Light() => _checkMark.SetActive(true);
    public void Unlight() => _checkMark.SetActive(false);

    public void Unlock()
    {
        IsLocked = false;
        UpdateStyle();
    }

    public void UpdateStyle()
    {
        foreach (GameObject go in _hideInUnlock)
        {
            go.SetActive(IsLocked);
        }

        foreach (GameObject go in _hideInLock)
        {
            go.SetActive(!IsLocked);
        }
        _icon.sprite = Gun.Icon;
        _priceLabel.text = Price.ToString();
    }
}
