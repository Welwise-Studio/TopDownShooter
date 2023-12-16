using Architecture.MVP;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Components
{
    public sealed class SliderWithTitleView : MonoBehaviour, IView
    { 
        [field: SerializeField]
        public Slider Slider { get; private set; }

        [SerializeField]
        private TMP_Text _title;

        public event Action OnShow;
        public event Action OnHide;

        public void Show()
        {
            this.gameObject.SetActive(true);
            OnShow?.Invoke();
        }

        public void Hide() 
        { 
            this.gameObject.SetActive(false); 
            OnHide?.Invoke();
        }

        public void SetInteractable(bool state) => Slider.interactable = state;
    }
}
