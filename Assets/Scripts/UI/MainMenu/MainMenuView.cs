using Architecture.MVP;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public sealed class MainMenuView : MonoBehaviour, IView
    {

        [field: SerializeField]
        public Button SettingsButton {  get; private set; }

        [field: SerializeField]
        public Button PlayButton { get; private set; }

        [SerializeField]
        private Image _logoImage;

        public void SetLogo(Sprite logo) => _logoImage.sprite = logo;

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
    }
}
