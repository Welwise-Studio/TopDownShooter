using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShelterSystem
{
    public class HealPoint : MonoBehaviour
    {
        [SerializeField]
        private int _healPrice = 100;
        [SerializeField]
        [Range(0, 100)]
        private float _healPercent;
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
        private TMP_Text _healthText;
        [SerializeField]
        private Wallet _wallet;
        [SerializeField]
        private LivingEntity _target;
        [SerializeField]
        private Shelter _shelter;

        private float _couldDownTimer;
        private bool _inCouldDown;

        private void OnEnable()
        {
            _shelter.OnLevelUp += UpdateHealthText;
        }

        private void OnDisable()
        {
            _shelter.OnLevelUp -= UpdateHealthText;
        }

        private void Start()
        {
            _priceHolder.text = _healPrice.ToString();
        }

        private void Update()
        {
            if (_wallet.Enough(_healPrice))
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

        private void UpdateHealthText(int lvl) => _healthText.text = (_target.startingHealth * (_healPercent / 100)).ToString();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out var player) && !_inCouldDown && _wallet.TrySpend(_healPrice))
            {
                _target.AddHealth(_target.startingHealth * (_healPercent/100));
                _inCouldDown = true;
                _couldDownProgress.fillAmount = 0;
                _couldDownTimer = _couldDownTime;
            }
        }
    }
}
