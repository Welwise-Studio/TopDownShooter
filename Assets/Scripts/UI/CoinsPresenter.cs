using TMPro;
using UnityEngine;

public class CoinsPresenter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Wallet _wallet;

    private void Awake()
    {
        _wallet.OnValuseChanged += UpdateCoins;
        UpdateCoins(_wallet.Value);
    }

    private void UpdateCoins(int value) => _text.text = value.ToString();
}
