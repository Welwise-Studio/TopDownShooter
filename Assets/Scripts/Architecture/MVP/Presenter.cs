using System;

namespace Architecture.MVP
{
    public abstract class Presenter<TView, TModel> : IDisposable where TView : View
    {
        protected readonly TView _view;
        protected readonly TModel _model;
        public Presenter(TView view, TModel model)
        {
            _view = view;
            _model = model;
            Init();
        }

        protected abstract void Init();

        public abstract void Dispose();
    }

    public abstract class Presenter<TView> : IDisposable where TView : View
    {
        protected readonly TView _view;
        public Presenter(TView view)
        {
            _view = view;
            Init();
        }

        protected abstract void Init();

        public abstract void Dispose();
    }
}
