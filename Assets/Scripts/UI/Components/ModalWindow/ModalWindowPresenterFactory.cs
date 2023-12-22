using Architecture.MVP;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Components.ModalWindow
{
    public sealed class ModalWindowPresenterFactory : PresenterFactory<ModalWindowView, ModalWindowPresenter, ModalWindowModel>
    {
        [SerializeField] private UnityAction _onOption1Click;
        [SerializeField] private UnityAction _onOption2Click;

        protected override ModalWindowPresenter Create()
        {
            _model = new ModalWindowModel(_onOption1Click, _onOption2Click);
            return new ModalWindowPresenter(_view, _model);
        }
    }
}
