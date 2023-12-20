using UnityEngine;

namespace Architecture.MVP
{
    public abstract class Binder<TView, TPresenter, TModel> : MonoBehaviour
        where TView : View
        where TModel : class
        where TPresenter : Presenter<TView, TModel>
    {
        [SerializeField] protected TView _view;

        protected TPresenter _presenter;
        protected TModel _model;

        protected abstract void Bind();
        protected abstract void Unbind();

        private void Start()
        {
            Bind();
        }

        private void OnDestroy()
        {
            Unbind();
        }
    }

    public abstract class Binder<TView, TPresenter> : MonoBehaviour
        where TView : View
        where TPresenter : Presenter<TView>
    {
        [SerializeField] protected TView _view;

        protected TPresenter _presenter;

        protected abstract void Bind();
        protected abstract void Unbind();

        private void Start()
        {
            Bind();
        }

        private void OnDestroy()
        {
            Unbind();
        }
    }
}
