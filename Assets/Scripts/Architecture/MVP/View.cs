using System;
using UnityEngine;

namespace Architecture.MVP
{
    public abstract class View : MonoBehaviour
    {
        public event Action OnShow;
        public event Action OnHide;

        public void Show()
        {
            gameObject.SetActive(true);
            OnShow?.Invoke();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            OnHide?.Invoke();
        }
        public virtual void SetInteractable(bool state) { }
    }
}
