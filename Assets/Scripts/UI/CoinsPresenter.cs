using System.Collections;
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
        StartCoroutine(FixAwakeCoins());
    }

    private void UpdateCoins(int value) => _text.text = value.ToString();

    private IEnumerator FixAwakeCoins()
    {
        yield return new WaitForSeconds(1.5f);
        _text.text = _wallet.Value.ToString();
    }
}
