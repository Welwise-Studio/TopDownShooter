using UnityEngine;

namespace Architecture.MVP
{
    public abstract class PresenterFactory<TView, TPresenter, TModel> : MonoBehaviour
        where TView : View
        where TModel : class
        where TPresenter : Presenter<TView, TModel>
    {
        [SerializeField] protected TView _view;

        private TPresenter _presenter;
        protected TModel _model;

        protected abstract TPresenter Create();

        private void Start()
        {
            _presenter = Create();
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
        }
    }

    public abstract class PresenterFactory<TView, TPresenter> : MonoBehaviour
        where TView : View
        where TPresenter : Presenter<TView>
    {
        [SerializeField] protected TView _view;

        private TPresenter _presenter;

        protected abstract TPresenter Create();

        private void Start()
        {
            _presenter = Create();
        }

        private void OnDestroy()
        {
            _presenter.Dispose();
        }
    }
}
