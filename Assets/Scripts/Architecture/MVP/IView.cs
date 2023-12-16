using System;

namespace Architecture.MVP
{
    public interface IView
    {
        public void Show();
        public void Hide();
        public event Action OnShow;
        public event Action OnHide;
        public void SetInteractable(bool state) { }
    }
}
