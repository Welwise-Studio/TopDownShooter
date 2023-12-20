using Architecture.MVP;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Components.ModalWindow
{
    public sealed class ModalWindowBinder : Binder<ModalWindowView, ModalWindowPresenter, ModalWindowModel>
    {
        [SerializeField] private UnityAction _onOption1Click;
        [SerializeField] private UnityAction _onOption2Click;

        protected override void Bind()
        {
            _model = new ModalWindowModel(_onOption1Click, _onOption2Click);
            _presenter = new ModalWindowPresenter(_view, _model);
        }

        protected override void Unbind()
        {
            _presenter.Dispose();
        }
    }
}
