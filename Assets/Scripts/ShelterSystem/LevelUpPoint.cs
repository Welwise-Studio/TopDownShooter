using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShelterSystem
{
    public class LevelUpPoint : MonoBehaviour
    {
        [SerializeField]
        private int _levelUpPrice = 100;
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
            _priceHolder.text = _levelUpPrice.ToString();
        }

        private void Update()
        {
            if (_wallet.Enough(_levelUpPrice))
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
            if (other.TryGetComponent<Player>(out var player) && !_inCouldDown && _wallet.TrySpend(_levelUpPrice))
            {
                _shelter.Upgrade();
                _inCouldDown = true;
                _couldDownProgress.fillAmount = 0;
                _couldDownTimer = _couldDownTime;
            }
        }
    }
}
