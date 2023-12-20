using UnityEngine.Events;

namespace UI.Components.ModalWindow
{
    public sealed class ModalWindowModel
    {
        public readonly UnityAction OnOption1Click;
        public readonly UnityAction OnOption2Click;

        public ModalWindowModel(UnityAction onOption1Click, UnityAction onOption2Click) 
        {
            OnOption1Click = onOption1Click;
            OnOption2Click = onOption2Click;
        }
    }
}
