using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShelterSystem
{
    public class LevelUpPoint : MonoBehaviour
    {
        [SerializeField]
        private int[] _levelUpPrices = new int[] {100, 150, 200, 300, 400, 500, 600, 1000, 1500, 2000};
        private int _currentPrice;
        [SerializeField]
        private float _couldDownTime;
        [SerializeField]
        private Color _enoughColor;
        [SerializeField]
        private Color _notEnoughColor;

        [SerializeField]
        private Image _couldDownProgress;
        [SerializeField]
        private TMP_Text _priceHolder;
        [SerializeField]
        private Wallet _wallet;
        [SerializeField]
        private Shelter _shelter;

        private float _couldDownTimer;
        private bool _inCouldDown;


        private void Start()
        {
            if (_shelter.Level > _levelUpPrices.Length)
                _currentPrice = _levelUpPrices[0];
            else
                _currentPrice = _levelUpPrices[_shelter.Level-1];

            _priceHolder.text = _currentPrice.ToString();
        }

        private void Update()
        {
            if (_wallet.Enough(_currentPrice))
                _couldDownProgress.color = _enoughColor;
            else
                _couldDownProgress.color = _notEnoughColor;

            if (_couldDownTimer <= 0)
                _inCouldDown = false;

            if (_inCouldDown)
            {
                _couldDownProgress.fillAmount = 1 - (_couldDownTimer / _couldDownTime);
                _couldDownTimer -= Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out var player) && !_inCouldDown && _wallet.TrySpend(_currentPrice))
            {
                _shelter.Upgrade();
                _inCouldDown = true;
                _couldDownProgress.fillAmount = 0;
                _couldDownTimer = _couldDownTime;
                _currentPrice = _levelUpPrices[_shelter.Level - 1];
                _priceHolder.text = _currentPrice.ToString();
            }
        }
    }
}
